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
    public class EventoAgendaRepository : Repository<Evento>, IEventoAgendaRepository
    {
        private readonly string convite_split = "convite_split";
        private readonly string permissoesConvite_split = "permissoesConvite_split";
        public EventoAgendaRepository(AgendaContext context) : base(context)
        {

        }

        public override Evento Obter(string eventoId)
        {
            var query = $@"SELECT e.*,
	                              Id as {convite_split}, c.*
                           FROM Evento e
                           LEFT JOIN Convite c on e.Id = c.EventoId
                           WHERE e.Id = '{eventoId}'";

            return DapperEvento(query, convite_split).FirstOrDefault();
        }

        public override void Adicionar(Evento obj)
        {
            base.Adicionar(obj);
            Db.Convite.AddRange(obj.Convites);
        }

        public override void Atualizar(Evento obj)
        {
            base.Atualizar(obj);
            Db.Convite.UpdateRange(obj.Convites);
        }

        public override void Excluir(Evento obj)
        {
            Db.Convite.RemoveRange(obj.Convites);
            base.Excluir(obj);
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
                    con.Query<Evento, Convite, Evento>(
                        query,
                        (evento, convite) =>
                        {
                            if (!eventos.Any(e => e.Id == evento.Id))
                                eventos.Add(evento);

                            eventos.Last().AdicionarConvite(convite);
                            return evento;
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

            return eventos.ToList();
        }
    }
}
