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

namespace ChernabogJailApp.Pages.RuleBook.Beasts
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
      public Beast Beast { get; set; } = default!;

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
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Beast == null)
            {
                return NotFound();
            }
            var beast = await _context.Beast.FindAsync(id);

            if (beast != null)
            {
                Beast = beast;
                _context.Beast.Remove(Beast);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
