using Agenda.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Validations
{
   public class RemoverEventoAgendaCommandValidacao : EventoAgendaValidacao<RemoverEventoAgendaCommand>
    {
        public RemoverEventoAgendaCommandValidacao()
        {
            ValidateId();
        }
    }
}
