using Agenda.Domain.Models;
using System;
using System.Collections.Generic;

namespace Agenda.Domain.Interfaces
{
    public interface IEventoAgendaRepository : IRepository<EventoAgenda>
    {
        IList<EventoAgenda> ObterTodosEventosDoUsuario(string agendaId, string usuarioId);

        IList<EventoAgenda> ObterEventosDaAgenda(string agendaId);

        IList<EventoAgenda> ObterEventosPorPeriodo(string agendaId, DateTime dataInicio, DateTime dataFinal);

    }
}
