using Microsoft.Extensions.DependencyInjection;
using Raven.DependencyInjection;
using Schedule.io.Core.Data.Configurations;
using Schedule.io.Core.Data.EventSourcing;
using Schedule.io.Interfaces.Services;
using Schedule.io.Infra.RavenDB.EventSourcing;
using System;
using System.Collections.Generic;
using System.Text;
using Schedule.io.Interfaces.Repositories;
using Raven.Client.Documents;
using Schedule.io.Models.AggregatesRoots;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Schedule.io.Infra.RavenDB.Indexes;

namespace Schedule.io.Infra.RavenDB.Configs
{
    public static class RavenDBApplicationBuilderExtensions
    {
        private static IDocumentStore EnsureExists(this IDocumentStore store)
        {
            try
            {
                using (var dbSession = store.OpenSession())
                {
                    dbSession.Query<Agenda>().Take(0).ToList();
                }
            }
            catch (Raven.Client.Exceptions.Database.DatabaseDoesNotExistException)
            {
                store.Maintenance.Server.Send(new Raven.Client.ServerWide.Operations.CreateDatabaseOperation(new Raven.Client.ServerWide.DatabaseRecord
                {
                    DatabaseName = store.Database
                }));
            }
            return store;
        }

        public static IApplicationBuilder UseScheduleioRavenDb(this IApplicationBuilder app)
        {
            var docStore = app.ApplicationServices.GetService<IDocumentStore>();
            docStore
                .EnsureExists()
                .CreateIndexes();

            return app;
        }
        public static void AddScheduleIoRavenDb(this IServiceCollection services, RavenDBConfig ravenDBConfig)
        {
            DataBaseConfigurationHelper.SetDataBaseConfig(ravenDBConfig);

            services
                .AddRavenDbDocStore(options =>
                    options.Settings = new RavenSettings()
                    {
                        Urls = ravenDBConfig.Urls,
                        CertFilePath = ravenDBConfig.CertificateFilePath,
                        DatabaseName = ravenDBConfig.DataBase,
                        CertPassword = ravenDBConfig.CertificatePassword
                    })
                .AddRavenDbSession();

            services.AddScoped<IEventSourcingRepository, EventSourcingRepository>();

            services.AddScoped<IAgendaRepository, AgendaRepository>();
            services.AddScoped<ITipoEventoRepository, TipoEventoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEventoAgendaRepository, EventoAgendaRepository>();
            services.AddScoped<ILocalRepository, LocalRepository>();
            services.AddScoped<IUnitOfWork, UoW.UnitOfWork>();
        }
    }
}

