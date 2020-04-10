using FluentValidation;
using Schedule.io.Core.Commands.AgendaUsuarioCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Validations.AgendaUsuarioValidations
{
   public abstract class AgendaUsuarioValidacao<T> : AbstractValidator<T> where T : AgendaUsuarioCommand
    {

    }
}
