using Agenda.Infra.Data.MongoDB.Interface.Connection;
using Microsoft.Extensions.Configuration;

namespace Agenda.Infra.Data.MongoDB.Connection
{
    public class Config : IConfig
    {
        public IConfiguration Configuration { get; }

        public const string DATABASE = "agenda";

        public Config(IConfiguration configuration)
        {
            Configuration = configuration;
            MongoConnectionString = Configuration["MongoDB:ConnectionString"];
            MongoDatabase = Configuration["MongoDB:Database"];
        }

        public Config(string connectionString, string database)
        {
            MongoConnectionString = connectionString;
            MongoDatabase = database;
        }
        public string MongoConnectionString { get; private set; }
        public string MongoDatabase { get; private set; }
    }
}
