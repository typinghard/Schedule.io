using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Core.DomainObjects
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {

        }
    }
}
