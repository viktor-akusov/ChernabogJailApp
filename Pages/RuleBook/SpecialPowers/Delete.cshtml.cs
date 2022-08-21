using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChernabogJailApp.Data;
using ChernabogJailApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ChernabogJailApp.Pages.RuleBook.SpecialPowers
{
    [Authorize(Roles = "Admin, Editor")]
    public class DeleteModel : PageModel
    {
        private readonly ChernabogJailApp.Data.ChernabogJailAppContext _context;

        public DeleteModel(ChernabogJailApp.Data.ChernabogJailAppContext context)
        {
            _context = context;
        }

        [BindProperty]
      public SpecialPower SpecialPower { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SpecialPower == null)
            {
                return NotFound();
            }

            var specialpower = await _context.SpecialPower.FirstOrDefaultAsync(m => m.Id == id);

            if (specialpower == null)
            {
                return NotFound();
            }
            else 
            {
                SpecialPower = specialpower;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.SpecialPower == null)
            {
                return NotFound();
            }
            var specialpower = await _context.SpecialPower.FindAsync(id);

            if (specialpower != null)
            {
                SpecialPower = specialpower;
                _context.SpecialPower.Remove(SpecialPower);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
