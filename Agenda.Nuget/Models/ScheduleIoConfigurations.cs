using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Models
{
    public class ScheduleIoConfigurations
    {
        public ScheduleIoConfigurations(DataBaseConfig dataBaseConfig)
        {
            DataBaseConfig = dataBaseConfig;
        }
        public DataBaseConfig DataBaseConfig { get; private set; }
    }
}
