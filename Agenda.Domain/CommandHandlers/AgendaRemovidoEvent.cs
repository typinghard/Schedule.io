using System;

namespace Agenda.Domain.CommandHandlers
{
    internal class AgendaRemovidoEvent
    {
        private Guid id;

        public AgendaRemovidoEvent(Guid id)
        {
            this.id = id;
        }
    }
}