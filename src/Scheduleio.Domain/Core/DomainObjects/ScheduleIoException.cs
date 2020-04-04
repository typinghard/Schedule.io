using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Core.DomainObjects
{
    public class ScheduleIoException : Exception
    {
        public List<string> ScheduleIoMessages;
        public ScheduleIoException(List<string> messages)
        {
            ScheduleIoMessages = messages;
        }

        public ScheduleIoException(string message) : base(message)
        {
        }
    }
}
