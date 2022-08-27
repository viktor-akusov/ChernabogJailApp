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

namespace ChernabogJailApp.Pages.RuleBook.Beasts.Categories
{
    [Authorize(Roles = "Admin, Editor")]
    public class DetailsModel : PageModel
    {
        private readonly ChernabogJailApp.Data.ChernabogJailAppContext _context;

        public DetailsModel(ChernabogJailApp.Data.ChernabogJailAppContext context)
        {
            _context = context;
        }

      public BeastCategory BeastCategory { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BeastCategory == null)
            {
                return NotFound();
            }

            var beastcategory = await _context.BeastCategory.FirstOrDefaultAsync(m => m.Id == id);
            if (beastcategory == null)
            {
                return NotFound();
            }
            else 
            {
                BeastCategory = beastcategory;
            }
            return Page();
        }
    }
}
