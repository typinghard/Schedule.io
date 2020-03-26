using System;

namespace Agenda.Domain.CommandHandlers
{
    internal class AgendaRemovidoEvent
    {
        private string id;

        public AgendaRemovidoEvent(string id)
        {
            this.id = id;
        }
    }
}