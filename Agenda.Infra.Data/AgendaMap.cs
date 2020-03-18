using Agenda.Domain.Models;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Infra.Data
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
