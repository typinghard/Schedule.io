using Schedule.io.Core.Core.Data.Configurations;
using Schedule.io.Core.Core.Data.Configurations.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.Data.SqlServerDB.Configs
{
    public class SqlServerDBConfig : IDataBaseConfig
    {
        public SqlServerDBConfig(string connectionsString, string schemaName)
        {
            ConnectionsString = connectionsString;
            SchemaName = schemaName;
        }

        public string ConnectionsString { get; private set; }
        public string SchemaName { get; private set; }

        public EDataBaseType GetDataBaseType()
        {
            return EDataBaseType.SQLSERVERDB;
        }
    }
}
