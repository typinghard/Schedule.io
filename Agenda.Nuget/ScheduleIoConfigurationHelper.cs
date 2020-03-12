using ScheduleIo.Nuget.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget
{
    public static class ScheduleIoConfigurationHelper
    {
        public static DataBaseConfig DataBaseConfig { get; private set; }
        public static void SetDataBaseConfig(DataBaseConfig dataBaseConfig)
        {
            DataBaseConfig = dataBaseConfig;
        }
    }
}
