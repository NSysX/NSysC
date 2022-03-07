using Application.DTOs.Users;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Autenticar.Commands.RegistrarUsuarioCommand
{
    public class RegistrarUsuarioCommand : IRequest<Respuesta<string>>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmaPassword { get; set; }
        public string Origen { get; set; } // se agrego esta propiedad
    }

    public class RegistrarUsuario_Handler : IRequestHandler<RegistrarUsuarioCommand, Respuesta<string>>
    {
        private readonly ICuentaServicio _cuentaServicio;

        public RegistrarUsuario_Handler(ICuentaServicio cuentaServicio)
        {
            this._cuentaServicio = cuentaServicio;
        }

        public async Task<Respuesta<string>> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            return await this._cuentaServicio.RegistrarAsync(new PeticionDeRegistro
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = request.Email,
                UserName = request.UserName,
                Password = request.Password,
                ConfirmaPassword = request.ConfirmaPassword
            }, request.Origen);
        }
    }
}
