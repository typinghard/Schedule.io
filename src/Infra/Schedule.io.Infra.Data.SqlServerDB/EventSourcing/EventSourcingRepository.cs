using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Schedule.io.Core.Core.Data.Configurations;
using Schedule.io.Core.Core.Data.EventSourcing;
using Schedule.io.Core.Core.Messages;
using Schedule.io.Infra.Data.SqlServerDB.Configs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Schedule.io.Infra.Data.SqlServerDB.EventSourcing
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        protected AgendaContext _agendaContext { get; private set; }
        protected DbSet<StoredEvent> _table { get; private set; }
        protected string _tableName { get; private set; }
        protected string _connectionString { get; private set; }

        public EventSourcingRepository(AgendaContext agendaContext)
        {
            _agendaContext = agendaContext;
            setCollectionName();
            setConnectAndCollection();
            _connectionString = _agendaContext.Database.GetDbConnection().ConnectionString;
        }

        #region internal
        internal void setCollectionName()
        {
            _tableName = typeof(StoredEvent).Name;
        }
        internal void setConnectAndCollection()
        {
            _table = _agendaContext.StoredEvents;
        }
        #endregion

        public IList<StoredEvent> ObterEventos(string aggregateId)
        {
            return ObterLista($"SELECT * FROM {_tableName} WHERE AggregatedId = {aggregateId} ").OrderBy(x => x.DataOcorrencia).ToList();
        }

        public void SalvarEvento<TEvent>(TEvent evento) where TEvent : Event
        {
            if (EventSourcingConfigurationHelper.Use)
                _table.Add(EventSourcingConfigurationHelper.FormatarEvento(evento));
        }


        public IList<StoredEvent> ObterLista(string query)
        {
            var list = new List<StoredEvent>();

            using (var con = new SqlConnection(_connectionString))
            {
                try
                {
                    con.Open();
                    list = con.Query<StoredEvent>(query).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return list;
            }
        }

    }
}
