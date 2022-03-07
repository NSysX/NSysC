using Application.DTOs.Users;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Autenticar.Commands.AutenticarCommand
{
    public class AutenticarUsuarioCommand : IRequest<Respuesta<RespuestaDeAutenticacion>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }
    }

    public class AutenticarUsuario_Handler : IRequestHandler<AutenticarUsuarioCommand, Respuesta<RespuestaDeAutenticacion>>
    {
        private readonly ICuentaServicio _cuentaServicio;

        public AutenticarUsuario_Handler(ICuentaServicio cuentaServicio)
        {
            this._cuentaServicio = cuentaServicio;
        }

        public async Task<Respuesta<RespuestaDeAutenticacion>> Handle(AutenticarUsuarioCommand request, CancellationToken cancellationToken)
        {
            return await this._cuentaServicio.AutenticarAsync(new PeticionDeAutenticacion
            {
                Email = request.Email,
                Password = request.Password,
            }, request.IpAddress);
        }
    }
}
