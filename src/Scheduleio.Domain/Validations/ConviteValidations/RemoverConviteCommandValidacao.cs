using Schedule.io.Core.Commands.Convite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Validations.ConviteValidations
{
    public class RemoverConviteCommandValidacao : ConviteValidacao<RemoverConviteCommand>
    {
        public RemoverConviteCommandValidacao()
        {
            ValidateId();
        }
    }
}
