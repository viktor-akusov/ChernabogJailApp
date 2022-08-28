using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChernabogJailApp.Data;
using ChernabogJailApp.Models;

namespace ChernabogJailApp.Pages.RuleBook.Beasts.Variations
{
    public class DeleteModel : PageModel
    {
        private readonly ChernabogJailApp.Data.ChernabogJailAppContext _context;

        public DeleteModel(ChernabogJailApp.Data.ChernabogJailAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BeastVariation BeastVariation { get; set; } = default!;
        public Beast Beast { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BeastVariation == null)
            {
                return NotFound();
            }

            var beastvariation = await _context.BeastVariation.FirstOrDefaultAsync(m => m.Id == id);

            if (beastvariation == null)
            {
                return NotFound();
            }
            else 
            {
                BeastVariation = beastvariation;
                Beast = _context.Beast.Where(b => b.Id == id).FirstOrDefault();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.BeastVariation == null)
            {
                return NotFound();
            }
            var beastvariation = await _context.BeastVariation.FindAsync(id);

            if (beastvariation != null)
            {
                BeastVariation = beastvariation;
                _context.BeastVariation.Remove(BeastVariation);
                await _context.SaveChangesAsync();
            }

            return Redirect(Url.Page("/RuleBook/Beasts/Variations/Index", new { id = beastvariation.BeastId }));
        }
    }
}
