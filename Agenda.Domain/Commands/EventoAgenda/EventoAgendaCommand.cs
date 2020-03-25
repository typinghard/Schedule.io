using Agenda.Domain.Core.Messages;
using Agenda.Domain.Enums;
using Agenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class EventoAgendaCommand : Command
    {
        public string Id { get; protected set; }
        public string AgendaId { get; protected set; }
        public string IdentificadorExterno { get; protected set; }
        public string Titulo { get; protected set; }
        public string Descricao { get; protected set; }
        public IList<string> Pessoas { get; protected set; }
        public string Local { get; protected set; }
        public DateTime DataInicio { get; protected set; }
        public DateTime? DataFinal { get; protected set; }
        public DateTime DataLimiteConfirmacao { get; protected set; }
        public int QuantidadeMinimaDeUsuarios { get; protected set; }
        public bool OcupaUsuario { get; protected set; }
        public bool EventoPublico { get; protected set; }
        public TipoEvento TipoEvento { get; protected set; }
        public EnumFrequencia EnumFrequencia { get; protected set; }
    }
}
