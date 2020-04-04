using Schedule.io.Core.Commands;
using Schedule.io.Core.Commands.EventoAgenda;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Validations.EventoAgendaValidations
{
   public class RemoverEventoAgendaCommandValidacao : EventoAgendaValidacao<RemoverEventoAgendaCommand>
    {
        public RemoverEventoAgendaCommandValidacao()
        {
            ValidateId();
        }
    }
}
