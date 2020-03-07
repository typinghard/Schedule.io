﻿using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class RemoverEventoUsuarioCommand : EventoUsuarioCommand
    {
        public RemoverEventoUsuarioCommand(Guid id)
        {
            this.Id = id;
            this.AggregateId = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverEventoUsuarioCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
