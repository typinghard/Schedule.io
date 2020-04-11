using Dapper;
using Microsoft.EntityFrameworkCore;
using Schedule.io.Core.Interfaces;
using Schedule.io.Core.Models;
using Schedule.io.Infra.Data.SqlServerDB.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Schedule.io.Infra.Data.SqlServerDB
{
    public class EventoAgendaRepository : Repository<EventoAgenda>, IEventoAgendaRepository
    {
        public EventoAgendaRepository(AgendaContext context) : base(context)
        {

        }

        public IList<EventoAgenda> ObterEventosDaAgenda(string agendaId)
        {

            var tipo_split = "tipo_split";
            var query = @$"
                                 SELECT *,
                                        Id as {tipo_split}, Nome, Descricao
                                 FROM {_table}  
                                 WHERE
                                 AgendaId = '{agendaId}'
                                 and {_inativoFalse}
            ";

            return DapperEventoAgenda(query, tipo_split);
        }

        public IList<EventoAgenda> ObterEventosPorPeriodo(string agendaId, DateTime dataInicio, DateTime dataFinal)
        {
            var tipo_split = "tipo_split";
            var query = @$"
                                 SELECT *,
                                        Id as {tipo_split}, Nome, Descricao
                                 FROM {_table}  
                                 WHERE
                                 AgendaId = '{agendaId}'
                                 and DataInicio between '{dataInicio.FormataDataSql(true)}'and '{dataFinal.FormataDataSql()}'
                                 and {_inativoFalse}
            ";

            return DapperEventoAgenda(query, tipo_split);

        }

        public IList<EventoAgenda> ObterTodosEventosDoUsuario(string agendaId, string usuarioId)
        {
            var tipo_split = "tipo_split";
            var query = @$"
                                 SELECT *,
                                        Id as {tipo_split}, Nome, Descricao
                                 FROM {_table}  
                                 WHERE
                                 AgendaId = '{agendaId}'
                                 and UsuarioId = '{usuarioId}'
                                 and {_inativoFalse}
            ";


            return DapperEventoAgenda(query, tipo_split);

        }

        private IList<EventoAgenda> DapperEventoAgenda(string query, string split)
        {
            var eventos = new List<EventoAgenda>();
            using (var con = new SqlConnection(_connectionString))
            {
                try
                {
                    con.Open();
                    con.Query<EventoAgenda, TipoEvento, EventoAgenda>(
                        query,
                        (eventoAgenda, tipoEvento) =>
                        {
                            eventos.Add(eventoAgenda);
                            eventos.Last().AtribuirTipo(tipoEvento);
                            return eventoAgenda;
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
