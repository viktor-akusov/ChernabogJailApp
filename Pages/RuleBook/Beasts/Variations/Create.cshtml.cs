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

namespace ChernabogJailApp.Pages.RuleBook.Beasts.Variations
{
    [Authorize(Roles = "Admin, Editor")]
    public class CreateModel : PageModel
    {
        private readonly ChernabogJailApp.Data.ChernabogJailAppContext _context;

        public CreateModel(ChernabogJailApp.Data.ChernabogJailAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            Beast = _context.Beast.Where(b => b.Id == id).FirstOrDefault();
            return Page();
        }

        [BindProperty]
        public BeastVariation BeastVariation { get; set; } = default!;
        public Beast Beast { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            _context.BeastVariation.Add(BeastVariation);
            await _context.SaveChangesAsync();
            return Redirect(Url.Page("/RuleBook/Beasts/Variations/Index", new { id = BeastVariation.BeastId }));
        }
    }
}
