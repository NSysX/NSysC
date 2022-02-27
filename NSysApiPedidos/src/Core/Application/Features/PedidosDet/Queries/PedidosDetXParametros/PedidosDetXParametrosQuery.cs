using Application.DTOs;
using Application.Interfaces;
using Application.Specifications.PedidosDet;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PedidosDet.Queries.PedidosDetXParametros
{
    public class PedidosDetXParametrosQuery : IRequest<RespuestaPaginada<List<PedidoDetDTO>>>
    {
        public int NumeroDePagina { get; set; }
        public int RegistrosXPagina { get; set; }
        public int IdCliente { get; set; }
        public string Estatus { get; set; }
    }

    public class PedidosDetXParametros_Manejador : IRequestHandler<PedidosDetXParametrosQuery, RespuestaPaginada<List<PedidoDetDTO>>>
    {
        private readonly IRepositoryAsync<PedidoDet> _repository;
        private readonly IMapper _mapper;

        public PedidosDetXParametros_Manejador(IRepositoryAsync<PedidoDet> repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<RespuestaPaginada<List<PedidoDetDTO>>> Handle(PedidosDetXParametrosQuery request, CancellationToken cancellationToken)
        {
            var entities = await this._repository.ListAsync(new PedidosDetXParametrosSpec(request.NumeroDePagina,
                                                            request.RegistrosXPagina, request.IdCliente , request.Estatus),                              cancellationToken);
            
            var dtos = this._mapper.Map<List<PedidoDetDTO>>(entities);

            return new RespuestaPaginada<List<PedidoDetDTO>>(dtos, request.NumeroDePagina, request.RegistrosXPagina);
        }
    }
}
