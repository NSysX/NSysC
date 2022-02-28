using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contextos;
using Persistence.Repository;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static void AgregaServiciosDePersistencia(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NSysPDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("defaultConnection"), b => b.MigrationsAssembly(typeof(NSysPDbContext).Assembly.FullName)));

            services.AddTransient(typeof(IRepositoryAsync<>), typeof(MiRepositorioAsync<>));
        }
    }
}
