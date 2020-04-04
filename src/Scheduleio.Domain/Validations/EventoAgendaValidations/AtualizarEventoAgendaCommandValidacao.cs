using Schedule.io.Core.Commands.EventoAgenda;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Validations.EventoAgendaValidations
{
    public class AtualizarEventoAgendaCommandValidacao : EventoAgendaValidacao<AtualizarEventoAgendaCommand>
    {
        public AtualizarEventoAgendaCommandValidacao()
        {
            ValidateId();
            ValidaTitulo();
            ValidaDescricao();
            ValidaDataInicialEvento();
            ValidaTipoEvento();
        }
    }
}
