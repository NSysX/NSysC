using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.ServicesExtensions
{
    public static class ServiciosDeExtencion
    {
        public static void AgregaSerilog(this IServiceCollection services, IHostEnvironment hostEnvironment)
        {
            // Registramos Serilog 
            var fechaString = DateTime.Now.ToShortDateString().Replace("/", "-");
            services.AddLogging(ConstructorDeLoggin =>
            {
                // 1 - Crear la Configuracion
                var ConfiguracionLogger = new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Information() // Reeplazamos la primer linea del AppsettingJson para el log
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // AppSettingJson.Microsoft = Warning
                    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                    .WriteTo.Console(  // este es para el sink de console ,con el sink de console y se puede configurar cada sink
                      outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] -> {Message} - {Properties} - {NewLine}"
                     // horas:min:seg   3 carac mayusculas
                     )
                    .WriteTo.File($".\\Logs\\{fechaString}_Logs.txt",
                       outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] -> {Message} - {Properties} - {NewLine}"
                    )
                    .Enrich.WithProperty("Ambiente_Ejecucion", hostEnvironment.EnvironmentName)
                    .Enrich.WithProperty("NombreApp", hostEnvironment.ApplicationName)
                    .Enrich.WithMachineName()
                    .Enrich.WithEnvironmentUserName()
                    .Enrich.WithAssemblyName()
                    .Enrich.WithAssemblyVersion();

                // 2 - Creal el Logger
                var logger = ConfiguracionLogger.CreateLogger();

                // 3 - Inyectar el Servicios como singleton
                ConstructorDeLoggin.Services.AddSingleton<ILoggerFactory>(
                    // le decimos que use el serilog logger factory viene de serilog extensions
                    // de parametros le decimos que logger tiene que usar (arriba) y no haga el dispose
                    provider => new SerilogLoggerFactory(logger, dispose: false));
            });
        }
    }


    public static class ServicesExtension
    {
        public static IEnumerable<TSource> FromHierarchy<TSource>(this TSource source, Func<TSource, TSource> nextItem, Func<TSource, bool> canContinue)
        {
            for (var current = source; canContinue(current); current = nextItem(current))
            {
                yield return current;
            }
        }

        public static IEnumerable<TSource> FromHierarchy<TSource>(this TSource source, Func<TSource, TSource> nextItem) where TSource : class
        {
            return FromHierarchy(source, nextItem, s => s != null);
        }

        public static string ObtenTodosLosMsjs(this Exception exception)
        {
            var messages = exception.FromHierarchy(ex => ex.InnerException).Select(ex => ex.Message);
            return string.Join(Environment.NewLine, messages);
        }
    }
}
