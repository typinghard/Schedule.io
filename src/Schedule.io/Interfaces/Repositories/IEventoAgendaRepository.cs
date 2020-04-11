using Schedule.io.Core.Core.Data;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;

namespace Schedule.io.Interfaces.Repositories
{
    public interface IEventoAgendaRepository : IRepository<Evento>
    {
        IList<Evento> ObterTodosEventosDoUsuario(string agendaId, string usuarioId);
        IList<Evento> ObterEventosDaAgenda(string agendaId);
        IList<Evento> ObterEventosPorPeriodo(string agendaId, DateTime dataInicio, DateTime dataFinal);
    }
}
