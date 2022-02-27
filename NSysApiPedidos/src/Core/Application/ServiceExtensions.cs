using Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ServiceExtensions
    {
        public static void AgregaServiciosAplicacion(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            // para registrar el Comportamiento de Validacion
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ComportamientoDeValidacion<,>));
        }
    }
}
