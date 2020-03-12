using ScheduleIo.Nuget.Interfaces;
using ScheduleIo.Nuget.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Services
{
    
    public class DataBaseConfigurationService : IDataBaseConfigurationService
    {
        public DataBaseConfig GetConfig()
        {
            return ScheduleIoConfigurationHelper.DataBaseConfig;
        }

        public void SetConfig(DataBaseConfig dataBaseConfig)
        {
            ScheduleIoConfigurationHelper.SetDataBaseConfig(dataBaseConfig);
        }
    }
}
