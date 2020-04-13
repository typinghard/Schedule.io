using MongoDB.Bson.Serialization;

namespace Schedule.io.Infra.MongoDB
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
