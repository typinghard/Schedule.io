using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Schedule.io.Core.Core.Data.Configurations;
using Schedule.io.Infra.RavenDB.Configs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Infra.RavenDB
{
    public class AgendaContext : IDisposable
    {
        private readonly DocumentStore documentStore;
        private readonly IDocumentSession _session;
        public AgendaContext()
        {
            var ravenDbConfig = (RavenDBConfig)DataBaseConfigurationHelper.DataBaseConfig;
            documentStore = new DocumentStore
            {
                Urls = ravenDbConfig.Urls,
                Database = ravenDbConfig.DataBase,
                //Certificate = ravenDbConfig.Certificate
            };
            documentStore.Initialize();
            _session = documentStore.OpenSession();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public IDocumentSession Session { get { return _session; } }
        public int SalvarAlteracoes()
        {
            try
            {
                _session.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

    }
}
