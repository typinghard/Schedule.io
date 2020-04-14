using Schedule.io.Core.Data;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Interfaces.Repositories
{
    public interface IAgendaRepository : IRepository<Agenda>
    {
        Agenda ObterAgendaPorUsuarioId(string agendaId, string usuarioId);

        IList<Agenda> ListarAgendasPorUsuarioId(string usuarioId);

        bool VerificaSeAgendaUsuarioExiste(AgendaUsuario agendaUsuario);
    }
}
