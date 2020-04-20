using Schedule.io.Core.Data;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using System.Collections.Generic;

namespace Schedule.io.Interfaces.Repositories
{
    public interface IAgendaRepository : IRepository<Agenda>
    {
        Agenda Obter(string agendaId, string usuarioId);
        IList<Agenda> Listar(string usuarioId);
    }
}
