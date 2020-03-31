using Agenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Interfaces
{
    public interface IAgendaUsuarioRepository : IRepository<AgendaUsuario>
    {
        AgendaUsuario ObterPorAgendaIdEUsuarioId(string agendaId, string usuarioId);
    }
}
