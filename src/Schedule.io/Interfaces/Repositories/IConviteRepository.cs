using Schedule.io.Core.Core.Data;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Interfaces.Repositories
{
    public interface IConviteRepository : IRepository<Convite>
    {
        IList<Convite> ObterConvitesPorEventoId(string eventoId);
    }
}
