using MongoDB.Bson.Serialization;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.Data.MongoDB
{
    public class AgendaMap : MongoMap<EventoAgenda>
    {
        public AgendaMap()
        {
            if (!IsClassMapRegistered())
            {
                BsonClassMap.RegisterClassMap<EventoAgenda>(cm =>
                {
                    cm.AutoMap();
                });
            }
        }
    }
}
