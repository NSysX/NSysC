using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Services;

namespace Shared
{
    public static class ServiceExtension
    {
        public static void AgregaServiciosDeShared(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IFechaHoraServicio, FechaHoraServicio>();
        }
    }
}
