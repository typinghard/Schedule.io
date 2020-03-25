﻿using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
   public  class RemoverAgendaUsuarioCommand : AgendaUsuarioCommand
    {
        public RemoverAgendaUsuarioCommand(string id)
        {
            this.Id = id;
            this.AggregateId = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverAgendaUsuarioCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
