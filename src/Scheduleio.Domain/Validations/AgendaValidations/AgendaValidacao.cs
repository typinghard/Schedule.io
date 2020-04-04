using FluentValidation;
using Schedule.io.Core.Commands.AgendaCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Validations.AgendaValidations
{
    public abstract class AgendaValidacao<T> : AbstractValidator<T> where T : AgendaCommand
    {

    }
}
