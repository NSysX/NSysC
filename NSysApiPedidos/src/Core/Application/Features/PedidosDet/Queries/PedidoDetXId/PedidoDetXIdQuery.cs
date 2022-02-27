using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PedidosDet.Queries.PedidoDetXId
{
    public class PedidoDetXIdQuery : IRequest<Respuesta<PedidoDetDTO>>
    {
        public int Id { get; set; }
    }

    public class PedidoDetXId_Manejador : IRequestHandler<PedidoDetXIdQuery, Respuesta<PedidoDetDTO>>
    {
        private readonly IRepositoryAsync<PedidoDet> _repositoryAsync;
        private readonly IMapper _mapper;

        public PedidoDetXId_Manejador(IRepositoryAsync<PedidoDet> repositoryAsync, IMapper mapper)
        {
            this._repositoryAsync = repositoryAsync;
            this._mapper = mapper;
        }

        public async Task<Respuesta<PedidoDetDTO>> Handle(PedidoDetXIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await this._repositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            
            if (entity == null)
                throw new KeyNotFoundException($"No se Encontro el Registro con Id = { request.Id }");

            var dto = this._mapper.Map<PedidoDetDTO>(entity);
            return new Respuesta<PedidoDetDTO>(dto);
        }
    }

    public class PedidoXIdValidator : AbstractValidator<PedidoDetXIdQuery>
    {
        public PedidoXIdValidator()
        {
            RuleFor(p => p.Id)
                .NotNull().WithMessage("{PropertyName} : No debe ser Null")
                .GreaterThan(0).WithMessage("{PropertyName} : Debe ser mayor a 0");
        }
    }
}
