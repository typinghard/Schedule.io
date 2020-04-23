using MongoDB.Driver;
using System;
using System.Collections.Generic;
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

        public IList<Agenda> Listar(string usuarioId)
        {
            return Db.Agenda
                .Find(c => c.UsuarioIdCriador == usuarioId)
                .ToList();
        }

        public Agenda Obter(string agendaId, string usuarioId)
        {
            return Db.Agenda.Find(a => a.Usuarios.Any(u => u.UsuarioId == usuarioId) && a.Id == agendaId).FirstOrDefault();
        }
    }
}
