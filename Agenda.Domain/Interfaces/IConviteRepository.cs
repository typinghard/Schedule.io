using Agenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Interfaces
{
    public interface IConviteRepository : IRepository<Convite>
    {
        IList<Convite> ObterConvitesPorEventoId(string eventoId);
    }
}
