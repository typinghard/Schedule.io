using Agenda.Domain.Models;
using Agenda.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Commands
{
    public class RegistrarEventoUsuarioCommand : EventoUsuarioCommand
    {
        public RegistrarEventoUsuarioCommand(Guid usuarioId, bool confirmacao, Permissao permissao)
        {
            this.UsuarioId = usuarioId;
            this.Confirmacao = confirmacao;
            this.Permissao = permissao;
        }


        public override bool EhValido()
        {
            ValidationResult = new RegistrarEventoUsuarioCommandValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
