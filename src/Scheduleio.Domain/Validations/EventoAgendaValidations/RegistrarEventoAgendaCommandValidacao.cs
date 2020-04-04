using Schedule.io.Core.Commands;
using Schedule.io.Core.Commands.EventoAgenda;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Validations.EventoAgendaValidations
{
    public class RegistrarEventoAgendaCommandValidacao : EventoAgendaValidacao<RegistrarEventoAgendaCommand>
    {
        public RegistrarEventoAgendaCommandValidacao()
        {
            ValidaTitulo();
            //ValidaDescricao();
            ValidaDataInicialEvento();
            ValidaTipoEvento();
            //ValidateQuantidadeDeUsuariosReferenteALotacaoMaximadoLocal();
        }
    }
}
