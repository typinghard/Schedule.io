using Schedule.io.Core.Data;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using System;
using System.Collections.Generic;

namespace Schedule.io.Interfaces.Repositories
{
    public interface IEventoRepository : IRepository<Evento>
    {
        IList<Evento> Listar(string agendaId, string usuarioId);
        IList<Evento> Listar(string agendaId);
        IList<Evento> Listar(string agendaId, DateTime dataInicio, DateTime dataFinal);
    }
}
