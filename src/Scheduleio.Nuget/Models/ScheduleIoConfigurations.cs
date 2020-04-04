using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Schedule.io.Models
{
    public class ScheduleIoConfigurations
    {
        public ScheduleIoConfigurations(bool useEventSourcing = false)
        {
            UseEventSourcing = useEventSourcing;
        }
        public bool UseEventSourcing { get; }
    }
}
