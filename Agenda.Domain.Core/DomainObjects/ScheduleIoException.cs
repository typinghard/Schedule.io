using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Core.DomainObjects
{
    public class ScheduleIoException : Exception
    {
        public ScheduleIoException(string message) : base(message)
        {

        }
    }
}
