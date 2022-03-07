using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class UsuariosDeAplicacion : IdentityUser
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
