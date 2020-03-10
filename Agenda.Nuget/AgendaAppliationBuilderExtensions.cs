using Agenda.Infra.CrossCutting.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NugetAgenda.Nuget
{
    public static class AgendaAppliationBuilderExtensions
    {
        public static IApplicationBuilder UseAgenda(this IApplicationBuilder app)
        {
            return app;
        }

        public static IServiceCollection AddAgenda(this IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
            return services;
        }


    }
}
