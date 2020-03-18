using Agenda.Infra.Data.Configs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Models
{
    public class ScheduleIoConfigurations
    {
        public DataBaseConfig DataBaseConfig { get; }
        public ScheduleIoConfigurations(DataBaseConfig dataBaseConfig)
        {
            DataBaseConfig = dataBaseConfig;
        }
    }
}
