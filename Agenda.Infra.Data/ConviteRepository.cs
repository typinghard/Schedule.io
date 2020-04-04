using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Infra.MongoDB
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
