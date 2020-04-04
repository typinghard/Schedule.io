using Schedule.io.Core.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Schedule.io.Core.Commands.Usuario;

namespace Schedule.io.Core.Validations.UsuarioValidations
{
    public abstract class UsuarioValidacao<T> : AbstractValidator<T> where T : UsuarioCommand
    {

    }
}
