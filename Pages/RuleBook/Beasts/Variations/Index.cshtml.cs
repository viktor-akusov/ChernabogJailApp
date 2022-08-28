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
    public class IndexModel : PageModel
    {
        private readonly ChernabogJailApp.Data.ChernabogJailAppContext _context;

        public IndexModel(ChernabogJailApp.Data.ChernabogJailAppContext context)
        {
            _context = context;
        }

        public IList<BeastVariation> BeastVariation { get;set; } = default!;
        public Beast Beast { get; set; }

        public async Task OnGetAsync(int id)
        {
            Beast = _context.Beast.Where(b => b.Id == id).FirstOrDefault();
            if (_context.BeastVariation != null)
            {
                BeastVariation = await _context.BeastVariation.Where(v => v.BeastId == id).ToListAsync();
            }
        }
    }
}
