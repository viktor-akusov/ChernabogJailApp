using ChernabogJailApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ChernabogJailApp.Pages.Administration
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ChernabogJailAppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IndexModel
            (
            UserManager<ChernabogJailAppUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [BindProperty]
        public IList<UserRolesViewModel> model { get; set; } = new List<UserRolesViewModel>();

        [BindProperty]
        public string userRole { get; set; }

        [BindProperty]
        public string userId { get; set; }

        [BindProperty]
        public string actionType { get; set; }

        public class UserRolesViewModel
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public IEnumerable<string> Roles { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {

            var users = await _userManager.Users.ToListAsync();

            foreach (ChernabogJailAppUser user in users)
            {
                UserRolesViewModel urv = new UserRolesViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = await _userManager.GetRolesAsync(user)
                };

                model.Add(urv);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ChernabogJailAppUser user = await _userManager.FindByIdAsync(userId); ;
            switch (actionType)
            {
                case "addRole":
                    bool userRoleExists = await _roleManager.RoleExistsAsync(userRole);
                    IdentityRole role;
                    if (!userRoleExists)
                    {
                        role = new IdentityRole();
                        role.Name = userRole;
                        await _roleManager.CreateAsync(role);
                    }
                    await _userManager.AddToRoleAsync(user, userRole);
                    break;
                case "delRole":
                    await _userManager.RemoveFromRoleAsync(user, userRole);
                    break;
                case "delUser":
                    await _userManager.DeleteAsync(user);
                    break;
                default:
                    break;
            }
            return RedirectToPage("./Index");
        }
    }
}
