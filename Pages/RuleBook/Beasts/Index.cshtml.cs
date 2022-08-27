using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChernabogJailApp.Data;
using ChernabogJailApp.Models;
using Newtonsoft.Json;
using ChernabogJailApp.Migrations;

namespace ChernabogJailApp.Pages.RuleBook.Beasts
{
    public class IndexModel : PageModel
    {
        private readonly ChernabogJailApp.Data.ChernabogJailAppContext _context;

        [BindProperty]
        public string? SearchString { get; set; }
        private readonly int pageSize = 8;
        public IndexModel(ChernabogJailApp.Data.ChernabogJailAppContext context)
        {
            _context = context;
        }

        public IList<Beast> Beast { get; set; } = default!;
        public IList<BeastCategory> BeastCategory { get; set; } = default!;
        [BindProperty]
        public Dictionary<int, string> BeastCategories { get; set; }

        public void OnGet(string? searchString, Dictionary<int, string> beastCategories)
        {
            BeastCategory = _context.BeastCategory.ToList();
            BeastCategories = beastCategories;
            SearchString = searchString ?? "";
        }

        public async Task<PartialViewResult> OnGetBeastsPartialAsync(string? searchString, int? pageIndex, string beastCategories)
        {
            BeastCategories = JsonConvert.DeserializeObject<Dictionary<int, string>>(beastCategories);
            var res = Partial("_Items", this);
            if (_context.BeastCategory != null)
            {
                IQueryable<Beast> beastsIQ = from b in _context.Beast
                                             select b;
                if (!String.IsNullOrEmpty(searchString))
                {
                    beastsIQ = beastsIQ.Where(b => b.Name.ToUpper().Contains(searchString.ToUpper())
                                           || b.Description.ToUpper().Contains(searchString.ToUpper()));
                }
                var categories = BeastCategories.Where(m => m.Value == "true").ToDictionary(i => i.Key, i => i.Value).Keys;
                if (categories.Count() > 0)
                {   
                    beastsIQ = from b in beastsIQ
                               where categories.Contains((int)b.BeastCategoryId)
                               select b;

                }
                Beast = await beastsIQ.Include(b => b.BeastCategory).OrderBy(s => s.Name).Skip(pageIndex is not null ? pageSize * ((int)pageIndex - 1) : 0).Take(pageSize).ToListAsync();
            }
            if (Beast.Count == 0)
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
