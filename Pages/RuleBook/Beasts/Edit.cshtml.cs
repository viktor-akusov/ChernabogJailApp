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
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ChernabogJailApp.Pages.RuleBook.Beasts
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
        public Beast Beast { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Beast == null)
            {
                return NotFound();
            }

            var beast =  await _context.Beast.FirstOrDefaultAsync(m => m.Id == id);
            if (beast == null)
            {
                return NotFound();
            }
            Beast = beast;
           ViewData["BeastCategoryId"] = new SelectList(_context.BeastCategory, "Id", "Name");
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

            _context.Attach(Beast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeastExists(Beast.Id))
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

        private bool BeastExists(int id)
        {
          return (_context.Beast?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
