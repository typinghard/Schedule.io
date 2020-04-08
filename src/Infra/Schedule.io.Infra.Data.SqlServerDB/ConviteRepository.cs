
using Schedule.io.Core.Interfaces;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.Data.SqlServerDB
{
    public class ConviteRepository : Repository<Convite>, IConviteRepository
    {

        public ConviteRepository(AgendaContext context) : base(context)
        {
        }

        public IList<Convite> ObterConvitesPorEventoId(string eventoId)
        {
            throw new NotImplementedException();
        }
    }
}
