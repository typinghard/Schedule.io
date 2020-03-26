using Agenda.Domain.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Infra.Data.Configs
{
    public static class ScheduleIoConfigurationHelper
    {
        public static DataBaseConfig DataBaseConfig { get; private set; }
        public static void SetDataBaseConfig(DataBaseConfig dataBaseConfig)
        {
            if (dataBaseConfig == null)
                throw new ScheduleIoException(new List<string> { "Configurações do banco de dados não informadas" });
            if (string.IsNullOrEmpty(dataBaseConfig.ConnectionString))
                throw new ScheduleIoException(new List<string> { "ConnectionString não informada" });

            DataBaseConfig = dataBaseConfig;
        }
    }
}
