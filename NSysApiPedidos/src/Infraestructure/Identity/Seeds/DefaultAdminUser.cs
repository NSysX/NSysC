using Application.Enums;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Seeds
{
    public static class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<UsuariosDeAplicacion> userManager, RoleManager<IdentityRole> roleManager)
        {
            // admin usuario
            var defaultUser = new UsuariosDeAplicacion
            {
                UserName = "UserAdmin",
                Email = "useradmin@mail.com",
                Nombre = "Eduardo",
                Apellido = "Navarro",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(r => r.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word1");
                    // luego le agregamos el rol
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    // tambien los permisos del basic
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
            }
        }
    }
}
