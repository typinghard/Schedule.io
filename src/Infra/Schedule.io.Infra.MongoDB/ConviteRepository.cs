using MongoDB.Driver;
using Schedule.io.Core.Interfaces;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.MongoDB
{
    public class ConviteRepository : Repository<Convite>, IConviteRepository
    {
        public ConviteRepository(AgendaContext context) : base(context)
        {
        }
        public IList<Convite> ObterConvitesPorEventoId(string eventoId)
        {
            return Db.Convite
                     .Find(x => x.EventoId == eventoId
                           && !x.Inativo)
                     .ToList();
        }
    }
}
