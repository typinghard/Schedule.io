using Agenda.Infra.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Infra.Data.Configs
{
    public class DataBaseConfig
    {
        public DataBaseConfig(EDataBaseType dataBaseType, string connectionString, string databaseName)
        {
            DataBaseType = dataBaseType;
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }
        public EDataBaseType DataBaseType { get; private set; }
        public string ConnectionString { get; private set; }
        public string DatabaseName { get; private set; }
    }
}
