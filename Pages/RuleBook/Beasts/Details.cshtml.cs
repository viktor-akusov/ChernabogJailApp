using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChernabogJailApp.Data;
using ChernabogJailApp.Models;

namespace ChernabogJailApp.Pages.RuleBook.Beasts
{
    public class DetailsModel : PageModel
    {
        private readonly ChernabogJailApp.Data.ChernabogJailAppContext _context;

        public DetailsModel(ChernabogJailApp.Data.ChernabogJailAppContext context)
        {
            _context = context;
        }

        public Beast Beast { get; set; } = default!;
        public IList<BeastVariation> BeastVariation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Beast == null)
            {
                return NotFound();
            }

            var beast = await _context.Beast.Include(m => m.BeastCategory).FirstOrDefaultAsync(m => m.Id == id);

            if (beast == null)
            {
                return NotFound();
            }
            else 
            {
                Beast = beast;
                BeastVariation = await _context.BeastVariation.Where(v => v.BeastId == beast.Id).ToListAsync();
            }
            return Page();
        }
    }
}
