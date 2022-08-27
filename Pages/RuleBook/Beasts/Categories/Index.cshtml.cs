using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChernabogJailApp.Data;
using ChernabogJailApp.Models;
using Newtonsoft.Json;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ChernabogJailApp.Pages.RuleBook.Beasts.Categories
{
    [Authorize(Roles = "Admin, Editor")]
    public class IndexModel : PageModel
    {
        private readonly ChernabogJailApp.Data.ChernabogJailAppContext _context;
        [BindProperty]
        public string? SearchString { get; set; }
        private readonly int pageSize = 8;

        public IndexModel(ChernabogJailApp.Data.ChernabogJailAppContext context)
        {
            _context = context;
        }

        public IList<BeastCategory> BeastCategory { get;set; } = default!;

        public void OnGet(string? searchString)
        {
            SearchString = searchString ?? "";
        }

        public async Task<PartialViewResult> OnGetCategoriesPartialAsync(string? searchString, int? pageIndex)
        {
            var res = Partial("_Items", this);
            if (_context.BeastCategory != null)
            {
                IQueryable<BeastCategory> beastCategoriesIQ = from b in _context.BeastCategory
                                                           select b;
                if (!String.IsNullOrEmpty(searchString))
                {
                    beastCategoriesIQ = beastCategoriesIQ.Where(b => b.Name.ToUpper().Contains(searchString.ToUpper())
                                           || b.Description.ToUpper().Contains(searchString.ToUpper()));
                }
                BeastCategory = await beastCategoriesIQ.OrderBy(s => s.Name).Skip(pageIndex is not null ? pageSize * ((int)pageIndex - 1) : 0).Take(pageSize).ToListAsync();
            }
            if (BeastCategory.Count == 0)
            {
                var result = Partial("_Items", this);
                result.StatusCode = 404;
                result.ViewName = "_Items";
                return result;
            }
            return res;
        }

    }
}
