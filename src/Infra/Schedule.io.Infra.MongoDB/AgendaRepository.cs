using MongoDB.Driver;
using Schedule.io.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Interfaces.Repositories;
using Schedule.io.Models.ValueObjects;
using System.Linq;
using Schedule.io.Infra.MongoDB.Configs;

namespace Schedule.io.Infra.MongoDB
{
    public class AgendaRepository : Repository<Agenda>, IAgendaRepository
    {
        public AgendaRepository(ScheduleioContext context) : base(context)
        {
        }

        public IList<Agenda> ListarAgendasPorUsuarioId(string usuarioId)
        {
            return Db.Agenda
                .Find(c => c.UsuarioIdCriador == usuarioId)
                .ToList();
        }

        public Agenda ObterAgendaPorUsuarioId(string agendaId, string usuarioId)
        {
            return Db.Agenda.Find(a => a.AgendasUsuarios.Any(u => u.UsuarioId == usuarioId) && a.Id == agendaId).FirstOrDefault();
        }

        public bool VerificaSeAgendaExiste(string agendaId)
        {
            return Db.Agenda.CountDocuments(a => a.Id == agendaId) > 0;
        }

        public bool VerificaSeAgendaExiste(string agendaId)
        {
            throw new NotImplementedException();
        }

        public bool VerificaSeAgendaUsuarioExiste(AgendaUsuario agendaUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
