using Schedule.io.Core.Commands.LocalCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Validations.LocalValidations
{
    public class RemoverLocalCommandValidacao : LocalValidacao<RemoverLocalCommand>
    {
        public RemoverLocalCommandValidacao()
        {
            ValidateId();
        }
    }
}
