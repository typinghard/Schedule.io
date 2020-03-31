using Agenda.Domain.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
   public class LocalCommand : Command
    {
        public string Id { get; protected set; }
        public string IdentificadorExterno { get; protected set; }
        public string Nome { get; protected set; }
        public string Descricao { get; protected set; }
        public bool Reserva { get; protected set; }
        public int LotacaoMaxima { get; protected set; }
    }
}
