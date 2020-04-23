using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Schedule.io.Core.Data.Configurations;
using Schedule.io.Core.Data.EventSourcing;
using Schedule.io.Infra.SqlServerDB.EventSourcing;
using Schedule.io.Infra.SqlServerDB.Extensions;
using Schedule.io.Interfaces.Repositories;

namespace Schedule.io.Infra.SqlServerDB.Configs
{
    public static class SqlServerDBApplicationBuilderExtensions
    {
        public static void UseScheduleIoSqlServerDb(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<AgendaContext>();
            context.CriarTabelas();
        }
        public static void AddScheduleioSqlServerDb(this IServiceCollection services, SqlServerDBConfig sqlServerDBConfig)
        {
            DataBaseConfigurationHelper.SetDataBaseConfig(sqlServerDBConfig);

            services.AddDbContext<AgendaContext>(options =>
                options.UseSqlServer(sqlServerDBConfig.ConnectionsString));

            services.AddScoped<AgendaContext>();
            services.AddScoped<IEventSourcingRepository, EventSourcingRepository>();
            services.AddScoped<IAgendaRepository, AgendaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ILocalRepository, LocalRepository>();
            services.AddScoped<ITipoEventoRepository, TipoEventoRepository>();
            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<IUnitOfWork, UoW.UnitOfWork>();
        }
    }
}
