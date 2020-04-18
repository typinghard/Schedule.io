using Dapper;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Schedule.io.Infra.SqlServerDB
{
    public class AgendaRepository : Repository<Agenda>, IAgendaRepository
    {
        private readonly string agendaUsuario_split = "agendaUsuario_split";
        public AgendaRepository(AgendaContext context) : base(context)
        {

        }

        public override void Adicionar(Agenda obj)
        {
            base.Adicionar(obj);
            Db.AgendaUsuario.AddRange(obj.AgendasUsuarios);
        }

        public override void Atualizar(Agenda obj)
        {
            base.Atualizar(obj);
            Db.AgendaUsuario.UpdateRange(obj.AgendasUsuarios);
        }

        public override void Excluir(Agenda obj)
        {
            base.Excluir(obj);
            Db.AgendaUsuario.RemoveRange(obj.AgendasUsuarios);
        }

        public override Agenda Obter(string agendaId)
        {
            var query = $@"SELECT a.*,
                            Id as {agendaUsuario_split}, au.AgendaId, au.UsuarioId 
                           FROM { TabelaAgenda } a
                           INNER JOIN { TabelaAgendaUsuario } au on a.Id = au.AgendaId
                           WHERE a.Id = '{agendaId}'";

            return DapperAgenda(query, agendaUsuario_split).FirstOrDefault();
        }

        public IList<Agenda> Listar(string usuarioId)
        {
            var query = @$"
                             SELECT a.*,
                             Id as {agendaUsuario_split}, au.AgendaId, au.UsuarioId 
                             FROM { TabelaAgenda } a 
                             INNER JOIN { TabelaAgendaUsuario } au on a.Id = au.AgendaId
                             WHERE
                             au.UsuarioId = '{usuarioId}'
            ";

            return DapperAgenda(query, agendaUsuario_split);
        }

        public Agenda Obter(string agendaId, string usuarioId)
        {
            var query = @$"
                             SELECT a.*,
                             Id as {agendaUsuario_split}, AgendaId, UsuarioId 
                             FROM { TabelaAgenda } a 
                             INNER JOIN { TabelaAgendaUsuario } au on a.Id = au.AgendaId
                             WHERE AgendaId = '{agendaId}'
                             and au.UsuarioId = '{usuarioId}'
            ";

            return DapperAgenda(query, agendaUsuario_split).FirstOrDefault();
        }

        private IList<Agenda> DapperAgenda(string query, string split)
        {
            var agendas = new List<Agenda>();
            using (var con = new SqlConnection(_connectionString))
            {
                try
                {
                    con.Open();
                    con.Query<Agenda, AgendaUsuario, Agenda>(
                        query,
                        (agenda, agendaUsuario) =>
                        {
                            if (!agendas.Any(a => a.Id == agenda.Id))
                                agendas.Add(agenda);

                            agendas.Last().AdicionarAgendaDoUsuario(agendaUsuario);
                            return agenda;
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

            return agendas.ToList();
        }
    }
}
