using Agenda.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Validations
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
