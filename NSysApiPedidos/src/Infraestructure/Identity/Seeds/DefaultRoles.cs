using Application.Enums;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Identity.Seeds
{
    public static class DefaultRoles
    {
       public static async Task SeedAsync(UserManager<UsuariosDeAplicacion> userManager, RoleManager<IdentityRole> roleManager)
       {
            // creamos el rol
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
       }
    }
}
