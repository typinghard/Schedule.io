﻿using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class AtualizarLocalCommand : LocalCommand
    {
        public AtualizarLocalCommand(string id, string identificadorExterno, string nomeLocal, string descricao, bool reservaLocal, int lotacaomaxima)
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
            ValidationResult = new AtualizarLocalCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
