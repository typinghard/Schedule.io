﻿using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class RegistrarUsuarioCommand : UsuarioCommand
    {
        public RegistrarUsuarioCommand(Guid id,string usuarioEmail)
        {
            this.Id = id;
            this.UsuarioEmail = usuarioEmail;
        }

        public override bool EhValido()
        {
            ValidationResult = new RegistrarUsuarioCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
