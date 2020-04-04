﻿using Schedule.io.Core.Validations.LocalValidations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Commands.LocalCommands
{
    public class RegistrarLocalCommand : LocalCommand
    {
        public RegistrarLocalCommand(string id, string identificadorExterno, string nomeLocal, string descricao, bool reservaLocal, int lotacaomaxima)
        {
            this.Id = id;
            this.IdentificadorExterno = identificadorExterno;
            this.Nome = nomeLocal;
            this.Descricao = descricao;
            this.Reserva = reservaLocal;
            this.LotacaoMaxima = lotacaomaxima;
        }

        public override bool EhValido()
        {
            ValidationResult = new RegistrarLocalCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}