using Agenda.Domain.Models;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Infra.MongoDB
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
