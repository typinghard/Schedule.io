using MongoDB.Bson.Serialization;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Infra.MongoDB
{
    public class AgendaMap : MongoMap<Evento>
    {
        public AgendaMap()
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
