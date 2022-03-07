using Application.Enums;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<UsuariosDeAplicacion> userManager, RoleManager<IdentityRole> roleManager)
        {
            // admin usuario
            var defaultUser = new UsuariosDeAplicacion
            {
                UserName = "UserBasic",
                Email = "userbasic@mail.com",
                Nombre = "Eduardito",
                Apellido = "Navarrito",
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
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
            }

        }
    }
}
