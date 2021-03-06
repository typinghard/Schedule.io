﻿using MongoDB.Bson.Serialization;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Infra.MongoDB.Configs
{
    public class ScheduleioMap : MongoMap<Evento>
    {
        public ScheduleioMap()
        {
            if (!IsClassMapRegistered())
            {
                BsonClassMap.RegisterClassMap<Evento>(cm =>
                {
                    cm.AutoMap();
                });
            }
        }
    }
}
