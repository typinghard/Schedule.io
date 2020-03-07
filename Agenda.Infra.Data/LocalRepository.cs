using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Infra.Data
{
    public class LocalRepository : Repository<Local>, ILocalRepository
    {
        public LocalRepository(AgendaContext context) : base(context)
        {
        }
    }
}
