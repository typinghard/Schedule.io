﻿using Schedule.io.Core.DomainObjects;
using System.Collections.Generic;

namespace Schedule.io.Core.Data.Configurations
{
    public class DataBaseConfigurationHelper
    {
        public static IDataBaseConfig DataBaseConfig { get; private set; }
        public static void SetDataBaseConfig(IDataBaseConfig dataBaseConfig)
        {
            DataBaseConfig = dataBaseConfig ?? throw new ScheduleIoException(new List<string> { "Configurações do banco de dados não informadas" });
        }
    }
}
