using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Interfaces
{
    public interface IAgendaRepository : IRepository<Agenda.Domain.Models.Agenda>
    {
        Agenda.Domain.Models.Agenda ObterAgendaPorUsuarioId(string agendaId,string usuarioId);
    }
}
