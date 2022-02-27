using Application.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.ServicesExtensions;

namespace WebApi.Middlewares
{
    public class ManejadorDeErroresMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private readonly ILogger<ManejadorDeErroresMiddleware> _logger;

        public ManejadorDeErroresMiddleware(RequestDelegate next, IHostEnvironment env, ILogger<ManejadorDeErroresMiddleware> logger)
        {
            this._next = next;
            this._env = env;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await this._next(httpContext);
            }
            catch (Exception error)
            {
                var respuesta = httpContext.Response;
                // la respuesta debe ser en Json
                respuesta.ContentType = "application/json";

                var respuestaModelo = new Respuesta<string>()
                {
                    // por que entrando aqui es que hubo un error
                    Succeeded = false,
                    Message = error?.Message,
                };

                // lo que en realidad queriamos hacer es el siguiente codigo
                switch (error)
                {
                    case Application.Exceptions.ExcepcionDeApi e:
                        // excepcion personalizada
                        respuestaModelo.Message = "Excepcion de API";
                        respuesta.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    case Application.Exceptions.ExcepcionDeValidador e:
                        // excepcion personalizada
                        respuesta.StatusCode = StatusCodes.Status400BadRequest;
                        respuestaModelo.Errores = e.ListaErrores;
                        break;
                    case KeyNotFoundException e:
                        // sirve para lanzar error 404 cuando no encontremos el registro
                        respuesta.StatusCode = StatusCodes.Status404NotFound;
                        break;

                    case DbUpdateException e:
                        respuesta.StatusCode = StatusCodes.Status400BadRequest;
                        respuestaModelo.Errores = new();
                        respuestaModelo.Message = String.Format(CultureInfo.CurrentCulture,e.Message);
                        respuestaModelo.Errores.Add(e.ObtenTodosLosMsjs()); // .InnerException.Message
                        break;
                    default:
                        respuesta.StatusCode = StatusCodes.Status500InternalServerError;    
                        break;
                }

                if (!this._env.IsDevelopment())
                {
                    respuestaModelo.Message = "Fallo la Operacion, Favor de intentarlo nuevamente";
                    respuestaModelo.Errores = null;
                }

                this._logger.LogError(error, error.ObtenTodosLosMsjs());

                string resultado = JsonSerializer.Serialize(respuestaModelo);

                // hacemos que escriba ese resultado
                await respuesta.WriteAsync(resultado);
            }
        }
    }
}
