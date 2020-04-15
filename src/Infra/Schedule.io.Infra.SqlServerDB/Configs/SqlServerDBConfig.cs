using Schedule.io.Core.Data.Configurations;
using Schedule.io.Core.Data.Configurations.Enums;

namespace Schedule.io.Infra.Data.SqlServerDB.Configs
{
    public class SqlServerDBConfig : IDataBaseConfig
    {
        public SqlServerDBConfig(string connectionsString)
        {
            ConnectionsString = connectionsString;
            //SchemaName = schemaName;
        }

        public string ConnectionsString { get; private set; }
        //public string SchemaName { get; private set; }

        public EDataBaseType GetDataBaseType()
        {
            return EDataBaseType.SQLSERVERDB;
        }
    }
}
