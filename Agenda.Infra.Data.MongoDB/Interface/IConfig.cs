using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Infra.Data.MongoDB.Interface.Connection
{
    public interface IConfig
    {
        string MongoConnectionString { get; }
        string MongoDatabase { get; }
    }
}
