using Schedule.io.Core.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Core.Data.Configurations
{
    public class DataBaseConfigurationHelper
    {
        public static IDataBaseConfig DataBaseConfig { get; private set; }
        public static void SetDataBaseConfig(IDataBaseConfig dataBaseConfig)
        {
            if (dataBaseConfig == null)
                throw new ScheduleIoException(new List<string> { "Configurações do banco de dados não informadas" });

            DataBaseConfig = dataBaseConfig;
        }
    }
}
