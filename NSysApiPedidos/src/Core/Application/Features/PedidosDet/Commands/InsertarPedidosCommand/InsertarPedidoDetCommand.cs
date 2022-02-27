using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PedidosDet.Commands.InsertarPedidosCommand
{
    public class InsertarPedidoDetCommand : IRequest<Respuesta<int>>
    {
        public int IdCliente { get; set; }
        public int IdEmpleado { get; set; }
        public double Cantidad { get; set; }
        public int IdProdMaestro { get; set; }
        public int IdMarca { get; set; }
        public bool EsCadaUno { get; set; }
        public string Notas { get; set; }
    }

    public class InsertarPedidoDet_Manejador : IRequestHandler<InsertarPedidoDetCommand, Respuesta<int>>
    {
        private readonly IRepositoryAsync<PedidoDet> _repositoryAsync;
        private readonly IMapper _mapper;

        public InsertarPedidoDet_Manejador(IRepositoryAsync<PedidoDet> repositoryAsync, IMapper mapper)
        {
            this._repositoryAsync = repositoryAsync;
            this._mapper = mapper;
        }

        public async Task<Respuesta<int>> Handle(InsertarPedidoDetCommand request, CancellationToken cancellationToken)
        {
            // Falta verificar que no exista que lo pidan mas de una vez
            // var entidad = 
            // si no existe

            var entity = this._mapper.Map<PedidoDet>(request);
            var data = await this._repositoryAsync.AddAsync(entity, cancellationToken);
            return new Respuesta<int>(data.Id);
        }
    }
}
