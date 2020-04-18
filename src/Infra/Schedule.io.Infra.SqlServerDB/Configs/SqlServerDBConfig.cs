using Schedule.io.Core.Data.Configurations;
using Schedule.io.Core.Data.Configurations.Enums;

namespace Schedule.io.Infra.SqlServerDB.Configs
{
    public class SqlServerDBConfig : IDataBaseConfig
    {
        public SqlServerDBConfig(string connectionsString, string schemaName = "dbo")
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
