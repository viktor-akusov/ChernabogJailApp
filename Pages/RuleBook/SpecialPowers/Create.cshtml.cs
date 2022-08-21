using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChernabogJailApp.Data;
using ChernabogJailApp.Models;
using static ChernabogJailApp.RuleBook.CommonEnums;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ChernabogJailApp.Pages.RuleBook.SpecialPowers
{
    [Authorize(Roles = "Admin, Editor")]
    public class CreateModel : PageModel
    {
        private readonly ChernabogJailApp.Data.ChernabogJailAppContext _context;
        public Type TypeOfEnumClass;
        public Type TypeOfEnumLevel;
        public Type TypeOfEnumSphere;
        public Type TypeOfEnumTime;

        public CreateModel(ChernabogJailApp.Data.ChernabogJailAppContext context)
        {
            _context = context;
            TypeOfEnumClass = typeof(EnumClass);
            TypeOfEnumLevel = typeof(EnumLevel);
            TypeOfEnumSphere = typeof(EnumSphere);
            TypeOfEnumTime = typeof(EnumTime);
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SpecialPower SpecialPower { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.SpecialPower == null || SpecialPower == null)
            {
                return Page();
            }

            _context.SpecialPower.Add(SpecialPower);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
