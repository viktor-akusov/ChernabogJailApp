using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChernabogJailApp.Data;
using ChernabogJailApp.Models;

namespace ChernabogJailApp.Pages.RuleBook.SpecialPowers
{
    public class DetailsModel : PageModel
    {
        private readonly ChernabogJailApp.Data.ChernabogJailAppContext _context;

        public DetailsModel(ChernabogJailApp.Data.ChernabogJailAppContext context)
        {
            _context = context;
        }

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
    }
}
