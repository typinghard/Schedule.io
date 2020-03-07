using Agenda.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Validations
{
    public abstract class AgendaValidacao<T> : AbstractValidator<T> where T : AgendaCommand
    {

    }
}
