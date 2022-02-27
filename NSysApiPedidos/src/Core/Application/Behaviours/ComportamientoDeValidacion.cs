using Application.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Behaviours
{

    // entra un TReponse y sale un TRequest
    public class ComportamientoDeValidacion<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        // vamos a manejar este validador como global
        // IValidator es de fluent validation
        private readonly IEnumerable<IValidator<TRequest>> _validadores;

        public ComportamientoDeValidacion(IEnumerable<IValidator<TRequest>> validadores)
        {
            this._validadores = validadores;
        }

        // implementa por fuerza el metodo
        public async Task<TResponse> Handle(TRequest peticion, 
                                            CancellationToken cancellationToken, 
                                            RequestHandlerDelegate<TResponse> next)
        {
            // si ha alguno validador implementado
            if (this._validadores.Any())
            {
                ValidationContext<TRequest> contextoDeValidacion = new FluentValidation.ValidationContext<TRequest>(peticion);
                ValidationResult[] resultadosDeValidacion = await Task.WhenAll(this._validadores.Select
                                                               (v => v.ValidateAsync(contextoDeValidacion, cancellationToken)));
                List<ValidationFailure> fallosDeValidacion = resultadosDeValidacion.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (fallosDeValidacion.Count != 0) // si encuentra un registro en la lista de fallos
                {
                    // por ejemplo errores de longitud o falta de llenado de una propiedad etc..
                    // hay dos tipos de constructores uno acepta una lista y otro no
                    throw new ExcepcionDeValidador(fallosDeValidacion);
                }

            }

            return await next();
        }
    }
}
