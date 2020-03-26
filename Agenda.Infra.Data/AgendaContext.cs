using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using Agenda.Infra.Data.Configs;
using Agenda.Core.Data.EventSourcing;

namespace Agenda.Infra.Data
{
    public class AgendaContext : IDisposable
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        private readonly IClientSessionHandle _session;
        public AgendaContext()
        {
            _mongoClient = new MongoClient(ScheduleIoConfigurationHelper.DataBaseConfig.ConnectionString);
            _session = _mongoClient.StartSession();
            _database = _mongoClient.GetDatabase(ScheduleIoConfigurationHelper.DataBaseConfig.DatabaseName);
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
                if (!_session.IsInTransaction)
                {
                    _session.StartTransaction();
                }

                _session.CommitTransaction();
                return 1;
            }
            catch (Exception ex)
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

        internal IMongoCollection<Agenda.Domain.Models.Agenda> Agenda { get { return _database.GetCollection<Agenda.Domain.Models.Agenda>(typeof(Agenda.Domain.Models.Agenda).Name.ToLower()); } }
        internal IMongoCollection<Usuario> Usuario { get { return _database.GetCollection<Usuario>(typeof(Usuario).Name.ToLower()); } }
        internal IMongoCollection<AgendaUsuario> AgendaUsuario { get { return _database.GetCollection<AgendaUsuario>(typeof(AgendaUsuario).Name.ToLower()); } }
        internal IMongoCollection<EventoAgenda> EventoAgenda { get { return _database.GetCollection<EventoAgenda>(typeof(EventoAgenda).Name.ToLower()); } }
        internal IMongoCollection<Convite> Convite { get { return _database.GetCollection<Convite>(typeof(Convite).Name.ToLower()); } }
        internal IMongoCollection<Local> Local { get { return _database.GetCollection<Local>(typeof(Local).Name.ToLower()); } }

        public IMongoCollection<StoredEvent> StoredEvents { get { return _database.GetCollection<StoredEvent>(typeof(StoredEvent).Name.ToLower()); } }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
