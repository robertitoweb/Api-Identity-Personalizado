using DemoIdentity.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DemoIdentity.Pages.Account
{
    [Authorize]
    public class RolesModel : PageModel
    {
        private readonly RoleManager<MyRol> _roleManager;
        private readonly UserManager<MyUser> _userManager;

        [BindProperty]
        public RolesDTO Rol { get; set; }

        public RolesModel(RoleManager<MyRol> rolesModel,UserManager<MyUser> userManager)
        {
            _roleManager = rolesModel;
            _userManager = userManager;
        }
    public async Task<IActionResult> OnGet()
        {
            var MyRoles = await _roleManager.Roles.ToListAsync();
            ViewData["roles"] = MyRoles;
            return Page();

        }
        public async Task<IActionResult> OnPostAsync()
        {
            var newRol = new MyRol();
            newRol.Name = Rol.Name;
            newRol.Fechalta = DateTime.Now;
            newRol.Seccion = Rol.Seccion;

            var res = await _roleManager.CreateAsync(newRol);

            //Assign user in rol
            var user = await _userManager.FindByEmailAsync("rguardado.rsoft@gmail.com");
            var rolassign = await _userManager.AddToRoleAsync(user, Rol.Name);

            return RedirectPermanent("/account/roles");
        }
    }
}
