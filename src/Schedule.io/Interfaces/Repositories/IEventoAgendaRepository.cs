using Schedule.io.Core.Data;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using System;
using System.Collections.Generic;

namespace Schedule.io.Interfaces.Repositories
{
    public interface IEventoAgendaRepository : IRepository<Evento>
    {
        IList<Evento> ListarTodosEventosDoUsuario(string agendaId, string usuarioId);
        IList<Evento> ListarEventosDaAgenda(string agendaId);
        IList<Evento> ListarEventosPorPeriodo(string agendaId, DateTime dataInicio, DateTime dataFinal);

        IList<Convite> ListarConvites(string eventoId);
    }
}
