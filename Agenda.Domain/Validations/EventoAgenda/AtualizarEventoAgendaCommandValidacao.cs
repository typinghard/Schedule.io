using Agenda.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Validations
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
