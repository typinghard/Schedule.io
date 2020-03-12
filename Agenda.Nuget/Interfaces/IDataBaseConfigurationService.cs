using ScheduleIo.Nuget.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Interfaces
{
    public interface IDataBaseConfigurationService
    {
        void SetConfig(DataBaseConfig dataBaseConfig);
        DataBaseConfig GetConfig();
    }
}
