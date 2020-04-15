using System;
using System.Collections.Generic;

namespace Schedule.io.Core.DomainObjects
{
    public class ScheduleIoException : Exception
    {
        public List<string> ScheduleIoMessages;
        public ScheduleIoException(List<string> messages) : base(string.Join(",", messages))
        {
            ScheduleIoMessages = messages;
        }

        public ScheduleIoException(string message) : base(message)
        {
        }
    }
}
