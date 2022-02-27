using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PedidosDet.Commands.EliminarPedidosCommand
{
    public class EliminarPedidoDetCommand : IRequest<Respuesta<int>>
    {
        public int Id { get; set; }
    }

    public class EliminarPedidoDet_Manejador : IRequestHandler<EliminarPedidoDetCommand, Respuesta<int>>
    {
        private readonly IRepositoryAsync<PedidoDet> _repositoryAsync;

        public EliminarPedidoDet_Manejador(IRepositoryAsync<PedidoDet> repositoryAsync)
        {
            this._repositoryAsync = repositoryAsync;
        }

        public async Task<Respuesta<int>> Handle(EliminarPedidoDetCommand request, CancellationToken cancellationToken)
        {
            // verificamos que exista

            var entity = await this._repositoryAsync.GetByIdAsync(request.Id);
            if (entity == null) 
                throw new KeyNotFoundException($"No se encontro el registro con el Id = { request.Id }");

            await this._repositoryAsync.DeleteAsync(entity);

            return new Respuesta<int>(entity.Id);
        }
    }
}
