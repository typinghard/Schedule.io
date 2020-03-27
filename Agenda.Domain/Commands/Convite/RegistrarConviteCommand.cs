﻿using Agenda.Domain.Enums;
using Agenda.Domain.Models;
using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class RegistrarConviteCommand : ConviteCommand
    {
        public RegistrarConviteCommand(string eventoId, string usuarioId, EnumStatusConviteEvento status, PermissoesConvite permissoes)
        {
            EventoId = eventoId;
            UsuarioId = usuarioId;
            Status = status;
            Permissoes = permissoes;
        }


        public override bool EhValido()
        {
            ValidationResult = new RegistrarConviteCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
