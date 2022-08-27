using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChernabogJailApp.Data;
using ChernabogJailApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ChernabogJailApp.Pages.RuleBook.Beasts
{
    [Authorize(Roles = "Admin, Editor")]
    public class CreateModel : PageModel
    {
        private readonly ChernabogJailApp.Data.ChernabogJailAppContext _context;

        public CreateModel(ChernabogJailApp.Data.ChernabogJailAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BeastCategoryId"] = new SelectList(_context.BeastCategory, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Beast Beast { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Beast == null || Beast == null)
            {
                return Page();
            }

            _context.Beast.Add(Beast);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
