﻿using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class RegistrarLocalCommand : LocalCommand
    {
        public RegistrarLocalCommand(Guid id, string identificadorExterno, string nomeLocal, string descricao, bool reservaLocal, int lotacaomaxima)
        {
            this.Id = id;
            this.IdentificadorExterno = identificadorExterno;
            this.NomeLocal = NomeLocal;
            this.Descricao = descricao;
            this.ReservaLocal = reservaLocal;
            this.LotacaoMaxima = lotacaomaxima;
        }

        public override bool EhValido()
        {
            ValidationResult = new RegistrarLocalCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
