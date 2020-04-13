using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Core.Data.EventSourcing;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Core.Data.Configurations;
using Schedule.io.Infra.MongoDB.Configs;
using Schedule.io.Models.ValueObjects;

namespace Schedule.io.Infra.MongoDB
{
    public class AgendaContext : IDisposable
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        private readonly IClientSessionHandle _session;
        public AgendaContext()
        {
            var mongoDbConfig = (MongoDBConfig)DataBaseConfigurationHelper.DataBaseConfig;
            _mongoClient = new MongoClient(mongoDbConfig.ConnectionString);
            _session = _mongoClient.StartSession();
            _database = _mongoClient.GetDatabase(mongoDbConfig.DatabaseName);
            Map();
        }

        protected void Map()
        {
            new AgendaMap();
        }

        public int SalvarAlteracoes()
        {
            try
            {
                _session.CommitTransaction();
                return 1;
            }
            catch
            {
                _session.AbortTransaction();
                return 0;
            }
        }

        public IClientSessionHandle Session { get { return _session; } }

        public IMongoCollection<T> Set<T>() where T : Entity
        {
            return _database.GetCollection<T>(typeof(T).Name.ToLower(),
                new MongoCollectionSettings()
                {
                    GuidRepresentation = GuidRepresentation.CSharpLegacy
                });
        }

        internal IMongoCollection<Agenda> Agenda { get { return _database.GetCollection<Agenda>(typeof(Agenda).Name.ToLower()); } }
        internal IMongoCollection<Usuario> Usuario { get { return _database.GetCollection<Usuario>(typeof(Usuario).Name.ToLower()); } }
        internal IMongoCollection<AgendaUsuario> AgendaUsuario { get { return _database.GetCollection<AgendaUsuario>(typeof(AgendaUsuario).Name.ToLower()); } }
        internal IMongoCollection<Evento> Evento { get { return _database.GetCollection<Evento>(typeof(Evento).Name.ToLower()); } }
        internal IMongoCollection<Convite> Convite { get { return _database.GetCollection<Convite>(typeof(Convite).Name.ToLower()); } }
        internal IMongoCollection<Local> Local { get { return _database.GetCollection<Local>(typeof(Local).Name.ToLower()); } }

        public IMongoCollection<StoredEvent> StoredEvents { get { return _database.GetCollection<StoredEvent>(typeof(StoredEvent).Name.ToLower()); } }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
