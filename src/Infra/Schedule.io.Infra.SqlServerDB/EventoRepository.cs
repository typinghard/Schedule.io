using Dapper;
using Schedule.io.Extensions;
using Schedule.io.Infra.SqlServerDB.Extensions;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Schedule.io.Infra.SqlServerDB
{
    public class EventoRepository : Repository<Evento>, IEventoRepository
    {
        private readonly string convite_split = "convite_split";

        public EventoRepository(AgendaContext context) : base(context)
        { }

        public override Evento Obter(string eventoId)
        {
            var query = $@"SELECT e.*,
	                              Id as {convite_split}, c.*
                           FROM {TabelaEvento} e
                           LEFT JOIN {TabelaConvite} c on e.Id = c.EventoId
                           WHERE e.Id = '{eventoId}'";

            return DapperEvento(query, convite_split).FirstOrDefault();
        }

        private void SincronizarConvites(Evento evento)
        {
            var convitesNoBanco = Db.Convites.Where(x => x.EventoId == evento.Id).ToList();
            Db.Convites.AddRange(evento.Convites.Where(x => !convitesNoBanco.Any(y => y.EventoId == x.EventoId &&
                                                                                      y.UsuarioId == x.UsuarioId)));
            Db.Convites.UpdateRange(evento.Convites.Where(x => convitesNoBanco.Any(y => y.EventoId == x.EventoId &&
                                                                                        y.UsuarioId == x.UsuarioId)));
            Db.Convites.RemoveRange(convitesNoBanco.Where(x => !evento.Convites.Any(y => y.EventoId == x.EventoId &&
                                                                                         y.UsuarioId == x.UsuarioId)));
        }

        public override void Adicionar(Evento obj)
        {
            base.Adicionar(obj);
            Db.Convites.AddRange(obj.Convites);
        }

        public override void Atualizar(Evento obj)
        {
            base.Atualizar(obj);
            SincronizarConvites(obj);
        }

        public override void Excluir(Evento obj)
        {
            Db.Convites.RemoveRange(obj.Convites);
            base.Excluir(obj);
        }

        public IList<Evento> Listar(string agendaId)
        {
            var query = $@"SELECT e.*,
	                              Id as {convite_split}, c.*
                           FROM {TabelaEvento} e
                           LEFT JOIN {TabelaConvite} c on e.Id = c.EventoId
                           WHERE
                           e.AgendaId = '{agendaId}'
                                 
            ";

            return DapperEvento(query, convite_split);
        }

        public IList<Evento> Listar(string agendaId, DateTime dataInicio, DateTime dataFinal)
        {
            var query = $@"SELECT e.*,
	                              Id as {convite_split}, c.*
                           FROM {TabelaEvento} e
                           LEFT JOIN {TabelaConvite} c on e.Id = c.EventoId
                           WHERE
                           e.AgendaId = '{agendaId}'
                           and e.DataInicio between '{dataInicio.FormataDataSql(true)}'and '{dataFinal.FormataDataSql()}'
                                 
            ";

            return DapperEvento(query, convite_split);

        }

        public IList<Evento> Listar(string agendaId, string usuarioId)
        {
            var query = $@"SELECT e.*,
	                              Id as {convite_split}, c.*
                           FROM {TabelaEvento} e
                           LEFT JOIN {TabelaConvite} c on e.Id = c.EventoId
                           WHERE
                           e.AgendaId = '{agendaId}'
                           and e.UsuarioIdCriador = '{usuarioId}'
                                 
            ";


            return DapperEvento(query, convite_split);

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

        public IList<Evento> Listar(string agendaId, List<DateTime> datasInicio, List<DateTime?> datasFinal)
        {
            var whereDinamico = PredicadoExtensions.Iniciar<Evento>();
            foreach (var dataInicio in datasInicio)
                whereDinamico = whereDinamico.And(x => x.DataInicio.Ticks >= dataInicio.Ticks);

            if (datasFinal.Any())
                foreach (var dataFinal in datasFinal)
                    whereDinamico = whereDinamico.And(x => x.DataFinal == null || x.DataFinal.Value.Ticks <= dataFinal.Value.Ticks);

            whereDinamico = whereDinamico.And(x => x.AgendaId == agendaId);

            return DbSet
                   .Where(whereDinamico)
                   .ToList();
        }
    }
}
