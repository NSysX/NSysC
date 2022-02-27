using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PedidosDet.Commands.ActualizarPedidosCommand
{
    public class ActualizarPedidoDetCommand : IRequest<Respuesta<int>>
    {
        public int Id { get; set; }
        public string Estatus { get; set; }
        public int IdCliente { get; set; }
        public int IdEmpleado { get; set; }
        public double Cantidad { get; set; }
        public int IdProdMaestro { get; set; }
        public int IdMarca { get; set; }
        public bool EsCadaUno { get; set; }
        public string Notas { get; set; }
    }

    public class ActualizarPedidoDet_Manejador : IRequestHandler<ActualizarPedidoDetCommand, Respuesta<int>>
    {
        private readonly IRepositoryAsync<PedidoDet> _repositoryAsync;

        public ActualizarPedidoDet_Manejador(IRepositoryAsync<PedidoDet> repositoryAsync)
        {
            this._repositoryAsync = repositoryAsync;
        }

        public async Task<Respuesta<int>> Handle(ActualizarPedidoDetCommand request, CancellationToken cancellationToken)
        {
            // si existe el registro a actualizar
            var entity = await this._repositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException($"No existe el registro con el Id = {request.Id}");

            entity.Estatus = request.Estatus;
            entity.IdCliente = request.IdCliente;
            entity.IdEmpleado = request.IdEmpleado;
            entity.Cantidad = request.Cantidad;
            entity.IdProdMaestro = request.IdProdMaestro;
            entity.IdMarca = request.IdMarca;
            entity.EsCadaUno = request.EsCadaUno;
            entity.Notas = request.Notas;

            await this._repositoryAsync.UpdateAsync(entity, cancellationToken);

            return new Respuesta<int>(entity.Id);
        }
    }
}
