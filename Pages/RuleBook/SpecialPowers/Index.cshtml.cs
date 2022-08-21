using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChernabogJailApp.Data;
using ChernabogJailApp.Models;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Runtime.ConstrainedExecution;
using static ChernabogJailApp.RuleBook.CommonEnums;
using System.Diagnostics;
using Newtonsoft.Json;

namespace ChernabogJailApp.Pages.RuleBook.SpecialPowers
{
    public class IndexModel : PageModel
    {
        private readonly ChernabogJailApp.Data.ChernabogJailAppContext _context;
        [BindProperty]
        public string? SearchString { get; set; }
        [BindProperty]
        public Dictionary<string, string> CharClasses { get; set; }
        [BindProperty]
        public Dictionary<string, string> Spheres { get; set; }
        [BindProperty]
        public Dictionary<string, string> Levels { get; set; }
        private readonly int pageSize = 8;

        public IndexModel(ChernabogJailApp.Data.ChernabogJailAppContext context)
        {
            _context = context;
        }

        public List<SpecialPower> SpecialPower { get;set; } = default!;

        public void OnGet(string? searchString, Dictionary<string, string> charClasses, Dictionary<string, string> spheres, Dictionary<string, string> levels)
        {
            SearchString = searchString ?? "";
            CharClasses = charClasses;
            Spheres = spheres;
            Levels = levels;
        }

        public async Task<PartialViewResult> OnGetSpecialPowersPartialAsync(string? searchString, int? pageIndex, string charClasses, string spheres, string levels)
        {
            var res = Partial("_Items", this);
            CharClasses = JsonConvert.DeserializeObject<Dictionary<string, string>>(charClasses);
            Spheres = JsonConvert.DeserializeObject<Dictionary<string, string>>(spheres);
            Levels = JsonConvert.DeserializeObject<Dictionary<string, string>>(levels);
            if (_context.SpecialPower != null)
            {
                IQueryable<SpecialPower> specialPowersIQ = from s in _context.SpecialPower
                                                           select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    specialPowersIQ = specialPowersIQ.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())
                                           || s.Description.ToUpper().Contains(searchString.ToUpper()));
                }
                var specialPowers = await specialPowersIQ.ToListAsync();
                var selectedCharClasses = CharClasses.Where(c => c.Value == "true").Select(c => c.Key.Trim('"')).ToList<string>();
                if (selectedCharClasses.Count > 0)
                {
                    specialPowers = specialPowers.Where(s => selectedCharClasses.Contains(s.Class.ToString())).ToList();
                }
                var selectedSpheres = Spheres.Where(c => c.Value == "true").Select(c => c.Key.Trim('"')).ToList<string>();
                if (selectedSpheres.Count > 0)
                {
                    specialPowers = specialPowers.Where(s => selectedSpheres.Contains(s.Sphere.ToString())).ToList();
                }
                var selectedLevels = Levels.Where(c => c.Value == "true").Select(c => c.Key.Trim('"')).ToList<string>();
                if (selectedLevels.Count > 0)
                {
                    specialPowers = specialPowers.Where(s => selectedLevels.Contains(s.Level.ToString())).ToList();
                }
                SpecialPower = specialPowers.OrderBy(s => s.Name).Skip(pageIndex is not null ? pageSize * ((int)pageIndex - 1) : 0).Take(pageSize).ToList();
            }
            if (SpecialPower.Count == 0)
            {
                var result = Partial("_Items", this);
                result.StatusCode = 404;
                result.ViewName = "_Items";
                return result;
            }
            return res;
        }

    }
}
