using MongoDB.Driver;
using Schedule.io.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.ValueObjects;

namespace Schedule.io.Infra.MongoDB
{
    public class AgendaRepository : Repository<Agenda>, IAgendaRepository
    {
        public AgendaRepository(AgendaContext context) : base(context)
        {
        }

        public void Gravar(AgendaUsuario obj)
        {
            Db.AgendaUsuario.InsertOne(Db.Session, obj);
            SalvarAlteracoes();
        }

        public IList<Agenda> ListarAgendasPorUsuarioId(string usuarioId)
        {
            return Db.Agenda
                .Find(c => c.UsuarioIdCriador == usuarioId)
                .ToList();
        }

        public Agenda ObterAgendaPorUsuarioId(string agendaId, string usuarioId)
        {
            var agendaUsuario = Db.AgendaUsuario.Find(c => c.AgendaId == agendaId && c.UsuarioId == usuarioId).FirstOrDefault();

            if (agendaUsuario == null)
                return null;

            return Db.Agenda.Find(x => x.Id == agendaUsuario.AgendaId).FirstOrDefault();
        }
    }
}
