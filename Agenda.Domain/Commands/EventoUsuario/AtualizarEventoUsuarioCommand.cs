using Agenda.Domain.Models;
using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class AtualizarEventoUsuarioCommand : EventoUsuarioCommand
    {
        public AtualizarEventoUsuarioCommand(Guid id, Guid usuarioId, bool confirmacao, Permissao permissao)
        {
            this.Id = id;
            this.UsuarioId = usuarioId;
            this.Confirmacao = confirmacao;
            this.Permissao = permissao;
        }


        public override bool EhValido()
        {
            ValidationResult = new AtualizarEventoUsuarioCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
