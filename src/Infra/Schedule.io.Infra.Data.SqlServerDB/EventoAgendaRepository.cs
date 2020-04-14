using Dapper;
using Schedule.io.Infra.Data.SqlServerDB.Extensions;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Schedule.io.Infra.Data.SqlServerDB
{
    public class EventoRepository : Repository<Evento>, IEventoAgendaRepository
    {
        public EventoRepository(AgendaContext context) : base(context)
        {

        }

        public void AdicionarConvite(Convite convite)
        {
            throw new NotImplementedException();
        }

        public void ExcluirConvite(Convite convite)
        {
            throw new NotImplementedException();
        }

        public IList<Convite> ListarConvites(string eventoId)
        {
            throw new NotImplementedException();
        }


        public IList<Evento> ListarEventosDaAgenda(string agendaId)
        {

            var tipo_split = "tipo_split";
            var query = @$"
                                 SELECT *,
                                        Id as {tipo_split}, Nome, Descricao
                                 FROM {_table}  
                                 WHERE
                                 AgendaId = '{agendaId}'
                                 
            ";

            return DapperEvento(query, tipo_split);
        }

        public IList<Evento> ListarEventosPorPeriodo(string agendaId, DateTime dataInicio, DateTime dataFinal)
        {
            var tipo_split = "tipo_split";
            var query = @$"
                                 SELECT *,
                                        Id as {tipo_split}, Nome, Descricao
                                 FROM {_table}  
                                 WHERE
                                 AgendaId = '{agendaId}'
                                 and DataInicio between '{dataInicio.FormataDataSql(true)}'and '{dataFinal.FormataDataSql()}'
                                 
            ";

            return DapperEvento(query, tipo_split);

        }

        public IList<Evento> ListarTodosEventosDoUsuario(string agendaId, string usuarioId)
        {
            var tipo_split = "tipo_split";
            var query = @$"
                                 SELECT *,
                                        Id as {tipo_split}, Nome, Descricao
                                 FROM {_table}  
                                 WHERE
                                 AgendaId = '{agendaId}'
                                 and UsuarioId = '{usuarioId}'
                                 
            ";


            return DapperEvento(query, tipo_split);

        }

        private IList<Evento> DapperEvento(string query, string split)
        {
            var eventos = new List<Evento>();
            using (var con = new SqlConnection(_connectionString))
            {
                try
                {
                    con.Open();
                    con.Query<Evento, TipoEvento, Evento>(
                        query,
                        (Evento, tipoEvento) =>
                        {
                            eventos.Add(Evento);
                            //eventos.Last().AtribuirTipo(tipoEvento);
                            return Evento;
                        },
                        splitOn: split);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }

            return eventos;
        }
    }
}
