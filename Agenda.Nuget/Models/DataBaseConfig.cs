using ScheduleIo.Nuget.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Models
{
    public class DataBaseConfig
    {
        public DataBaseConfig(EDataBaseType dataBaseType, string connectionString)
        {
            DataBaseType = dataBaseType;
            ConnectionString = connectionString;
        }
        public EDataBaseType DataBaseType { get; private set; }
        public string ConnectionString { get; private set; }
    }

    
}
