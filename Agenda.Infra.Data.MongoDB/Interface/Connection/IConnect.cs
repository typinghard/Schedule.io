using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Infra.Data.MongoDB.Interface.Connection
{
    public interface IConnect
    {
        IMongoCollection<T> Collection<T>(string CollectionName);
    }
}
