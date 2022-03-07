using FluentValidation;

namespace Application.Features.Autenticar.Commands.RegistrarUsuarioCommand
{
    public class RegistrarUsuarioValidator : AbstractValidator<RegistrarUsuarioCommand>
    {
        public RegistrarUsuarioValidator()
        {
            RuleFor(x => x.Nombre)
                .NotNull().WithMessage("'{PropertyName}' : No puede ser NULL")
                .NotEmpty().WithMessage("'{PropertyName}' : No puede estar vacio")
                .Length(2, 50).WithMessage("'{PropertyName}' : Debe tener entre {MinLength} y {MaxLength} Caracteres")
                .Matches(@"^[a-zA-ZáéíóúñÑ\s]*$").WithMessage("'{PropertyName}' : Contiene Caracteres Invalidos (Solo acepta letras mayusculas,Minusculas y espacios)");

            RuleFor(x => x.Apellido)
                .NotNull().WithMessage("'{PropertyName}' : No puede ser NULL")
                .NotEmpty().WithMessage("'{PropertyName}' : No puede estar vacio")
                .Length(2, 50).WithMessage("'{PropertyName}' : Debe tener entre {MinLength} y {MaxLength} Caracteres")
                .Matches(@"^[a-zA-ZáéíóúñÑ\s]*$").WithMessage("'{PropertyName}' : Contiene Caracteres Invalidos (Solo acepta letras mayusculas,Minusculas y espacios)");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("'{PropertyName}' : No debe estar vacia")
                .NotNull().WithMessage("'{PropertyName}' : No debe se Nulo")
                .Length(5, 50).WithMessage("'{PropertyName}' : Debe tener entre {MinLength} y {MaxLength} Caracteres longitud")
                .EmailAddress().WithMessage("Ingresar un Email Valido");

            RuleFor(x => x.UserName)
                .NotNull().WithMessage("'{PropertyName}' : No puede ser NULL")
                .NotEmpty().WithMessage("'{PropertyName}' : No puede estar vacio")
                .Length(8, 15).WithMessage("'{PropertyName}' : Debe tener entre {MinLength} y {MaxLength} Caracteres");
            //.Matches(@"^[a-zA-ZáéíóúñÑ\s]*$").WithMessage("'{PropertyName}' : Contiene Caracteres Invalidos )");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("'{PropertyName}' : No puede ser NULL")
                .NotEmpty().WithMessage("'{PropertyName}' : No puede estar vacio")
                .Length(8, 15).WithMessage("'{PropertyName}' : Debe tener entre {MinLength} y {MaxLength} Caracteres");

            RuleFor(x => x.ConfirmaPassword)
                .NotNull().WithMessage("'{PropertyName}' : No puede ser NULL")
                .NotEmpty().WithMessage("'{PropertyName}' : No puede estar vacio")
                .Length(8, 15).WithMessage("'{PropertyName}' : Debe tener entre {MinLength} y {MaxLength} Caracteres")
                .Equal(p => p.ConfirmaPassword).WithMessage("'{PropertyName}' : Debe ser Igual a Password");
        }
    }
}
