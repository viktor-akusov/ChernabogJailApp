using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChernabogJailApp.Data;
using ChernabogJailApp.Models;
using static ChernabogJailApp.RuleBook.CommonEnums;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ChernabogJailApp.Pages.RuleBook.SpecialPowers
{
    [Authorize(Roles = "Admin, Editor")]
    public class EditModel : PageModel
    {
        private readonly ChernabogJailApp.Data.ChernabogJailAppContext _context;
        public Type TypeOfEnumClass;
        public Type TypeOfEnumLevel;
        public Type TypeOfEnumSphere;
        public Type TypeOfEnumTime;
        public EditModel(ChernabogJailApp.Data.ChernabogJailAppContext context)
        {
            _context = context;
            TypeOfEnumClass = typeof(EnumClass);
            TypeOfEnumLevel = typeof(EnumLevel);
            TypeOfEnumSphere = typeof(EnumSphere);
            TypeOfEnumTime = typeof(EnumTime);
        }
        [BindProperty]
        public SpecialPower SpecialPower { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SpecialPower == null)
            {
                return NotFound();
            }

            var specialpower =  await _context.SpecialPower.FirstOrDefaultAsync(m => m.Id == id);
            if (specialpower == null)
            {
                return NotFound();
            }
            SpecialPower = specialpower;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SpecialPower).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialPowerExists(SpecialPower.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SpecialPowerExists(int id)
        {
          return (_context.SpecialPower?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
