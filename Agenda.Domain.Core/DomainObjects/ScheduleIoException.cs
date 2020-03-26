using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.Core.DomainObjects
{
    public class ScheduleIoException : Exception
    {
        public List<string> ScheduleIoMessages;
        public ScheduleIoException(List<string> messages) : base(string.Join(",", messages))
        {
            ScheduleIoMessages = messages;
        }
    }
}
