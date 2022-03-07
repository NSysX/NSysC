using Application.DTOs.Users;
using Application.Features.Autenticar.Commands.AutenticarCommand;
using Application.Features.Autenticar.Commands.RegistrarUsuarioCommand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : BaseApiController
    {
        [HttpPost("Authenticate")]
        public async Task<IActionResult> AuthenticateAsync(PeticionDeAutenticacion peticion)
        {
            return Ok(await this.Mediator.Send(new AutenticarUsuarioCommand
            {
                Email = peticion.Email,
                Password = peticion.Password,
                IpAddress = GeneraDireccionIP()
            }));

            string GeneraDireccionIP()
            {
                // si tiene la llave en la cabecera X-Forwarded-For
                if (Request.Headers.ContainsKey("X-Forwarded-For"))
                    return Request.Headers["X-Forwarded-For"];
                else
                    return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }
          
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(PeticionDeRegistro peticion)
        {
            return Ok(await this.Mediator.Send(new RegistrarUsuarioCommand
            {
                Nombre = peticion.Nombre,
                Apellido = peticion.Apellido,
                Email = peticion.Email,
                UserName = peticion.UserName,
                Password = peticion.Password,
                ConfirmaPassword = peticion.ConfirmaPassword,
                Origen = Request.Headers["origin"]
            }));
        }

        
    }

  
}
