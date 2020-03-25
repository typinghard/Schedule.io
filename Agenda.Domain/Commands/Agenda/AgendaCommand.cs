﻿using Agenda.Domain.Core.Messages;
using System;

namespace Agenda.Domain.Commands
{
    public class AgendaCommand : Command
    {
        public string Id { get; protected set; }
        public string Titulo { get; protected set; }

        public string Descricao { get; protected set; }

        public bool Publico { get; protected set; }
    }
}
