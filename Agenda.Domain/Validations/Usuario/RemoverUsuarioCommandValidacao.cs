using Agenda.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Validations
{
   public class RemoverUsuarioCommandValidacao :UsuarioValidacao<RemoverUsuarioCommand>
    {
        public RemoverUsuarioCommandValidacao()
        {
            //validações
        }
    }
}
