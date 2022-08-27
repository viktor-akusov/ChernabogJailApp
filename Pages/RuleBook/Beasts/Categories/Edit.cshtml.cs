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

namespace ChernabogJailApp.Pages.RuleBook.Beasts.Categories
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
        public BeastCategory BeastCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BeastCategory == null)
            {
                return NotFound();
            }

            var beastcategory =  await _context.BeastCategory.FirstOrDefaultAsync(m => m.Id == id);
            if (beastcategory == null)
            {
                return NotFound();
            }
            BeastCategory = beastcategory;
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

            _context.Attach(BeastCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeastCategoryExists(BeastCategory.Id))
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

        private bool BeastCategoryExists(int id)
        {
          return (_context.BeastCategory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
