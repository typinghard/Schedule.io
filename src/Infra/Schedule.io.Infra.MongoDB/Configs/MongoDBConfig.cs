using Schedule.io.Core.Data.Configurations;
using Schedule.io.Core.Data.Configurations.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.MongoDB.Configs
{
    public class MongoDBConfig : IDataBaseConfig
    {
        public MongoDBConfig(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }
        public string ConnectionString { get; private set; }
        public string DatabaseName { get; private set; }

        public EDataBaseType GetDataBaseType()
        {
            return EDataBaseType.MONGODB;
        }
    }
}
