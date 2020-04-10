using System;

namespace Schedule.io.Core.CommandHandlers
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