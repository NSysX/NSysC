using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PedidosDet.Commands.ActualizarPedidosCommand
{
    public class ActualizarPedidoDetValidator : AbstractValidator<PedidoDet>
    {
        public ActualizarPedidoDetValidator()
        {
            RuleFor(p => p.Id)
               .NotNull().WithMessage("'{PropetyName}' : No debe ser nulo")
               .GreaterThan(0).WithMessage("'{PropertyName}' : Debe ser mayor a 0");

            RuleFor(p => p.Estatus)
               .NotNull().WithMessage("'{PropertyName}' : No debe ser Nulo")
               .NotEmpty().WithMessage("'{PropertyName}' : No debe estar vacion");

            RuleFor(p => p.IdCliente)
                .NotNull().WithMessage("'{PropertyName}' : No debe ser Nulo")
                .GreaterThan(0).WithMessage("'{PropertyName}' : Solo numeros mayores a 0");

            RuleFor(p => p.IdEmpleado)
                .NotNull().WithMessage("'{PropertyName}' : No debe ser Nulo")
                .GreaterThan(0).WithMessage("'{PropertyName}' : Solo numeros Mayores a 0");

            RuleFor(p => p.Cantidad)
                .NotNull().WithMessage("'{PropertyName}' : No debe ser Nulo")
                .GreaterThan(0).WithMessage("'{PropertyName}' : Solo numeros mayores a 0");

            RuleFor(p => p.IdProdMaestro)
                .NotNull().WithMessage("'{PropertyName}' : No debe ser Nulo")
                .GreaterThan(0).WithMessage("'{PropertyName}' : Solo Numeros Mayores a 0");

            RuleFor(p => p.IdMarca)
                .NotNull().WithMessage("'{PropertyName}' : No debe ser Nulo")
                .GreaterThan(0).WithMessage("'{PropertyName}' : Solo numeros mayores a 0");

            RuleFor(p => p.EsCadaUno)
                .NotNull().WithMessage("'{PropertyName}' : No debe ser Nulo");

            RuleFor(p => p.Notas)
                .NotNull().WithMessage("'{PropertyName}' : No debe ser Nulo");
        }
    }
}
