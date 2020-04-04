using Schedule.io.Core.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Commands.Local
{
   public class LocalCommand : Command
    {
        public string Id { get; protected set; }
        public string IdentificadorExterno { get; protected set; }
        public string NomeLocal { get; protected set; }
        public string Descricao { get; protected set; }
        public bool ReservaLocal { get; protected set; }
        public int LotacaoMaxima { get; protected set; }
    }
}
