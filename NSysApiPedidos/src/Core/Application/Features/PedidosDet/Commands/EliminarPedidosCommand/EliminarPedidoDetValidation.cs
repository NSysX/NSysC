using FluentValidation;

namespace Application.Features.PedidosDet.Commands.EliminarPedidosCommand
{
    public class EliminarPedidoDetValidation : AbstractValidator<EliminarPedidoDetCommand>
    {
        public EliminarPedidoDetValidation()
        {
            RuleFor(p => p.Id)
                .NotNull().WithMessage("'{PropetyName}' : No debe ser nulo")
                .GreaterThan(0).WithMessage("'{PropertyName}' : Debe ser mayor a 0");  
        }
    }
}
