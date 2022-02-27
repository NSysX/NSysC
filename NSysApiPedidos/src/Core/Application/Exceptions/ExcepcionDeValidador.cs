using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Application.Exceptions
{
    public class ExcepcionDeValidador : Exception 
    {
        // Creamos una propiedad 
        public List<string> ListaErrores { get; }

        // constructor vacio y heredamos del constructor base y le mandamos el parametro
        public ExcepcionDeValidador() : base("Se ha producido uno o mas errores de Validacion")
        {
            ListaErrores = new List<string>();
        }

        // se crea otro constructor , le pasamor un ienumerable recolectando las validaciones
        // que nos arroja el FluentValidation
        // este contructor recibe un IEnumerable de tipo validationFailure
        public ExcepcionDeValidador(IEnumerable<ValidationFailure> fallosDeValidacion) : this()
        {
            // hacemos un foreach
            // para recorrer todos esos errores para gregarlos a ListaErrores
            foreach (ValidationFailure falloDeValidacion in fallosDeValidacion)
            {
                ListaErrores.Add(falloDeValidacion.ErrorMessage);
            }

        }
    }
}
