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
using ChernabogJailApp.Migrations;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ChernabogJailApp.Pages.RuleBook.Beasts.Variations
{
    [Authorize(Roles = "Admin, Editor")]
    public class EditModel : PageModel
    {
        private readonly ChernabogJailApp.Data.ChernabogJailAppContext _context;

        public EditModel(ChernabogJailApp.Data.ChernabogJailAppContext context)
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

            var beastvariation =  await _context.BeastVariation.FirstOrDefaultAsync(m => m.Id == id);
            if (beastvariation == null)
            {
                return NotFound();
            }
            BeastVariation = beastvariation;
            Beast = _context.Beast.Where(b => b.Id == beastvariation.BeastId).FirstOrDefault();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            _context.Attach(BeastVariation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeastVariationExists(BeastVariation.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Redirect(Url.Page("/RuleBook/Beasts/Variations/Index", new { id = BeastVariation.BeastId }));
        }

        private bool BeastVariationExists(int id)
        {
          return (_context.BeastVariation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
