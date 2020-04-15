using MongoDB.Bson.Serialization;

namespace Schedule.io.Infra.MongoDB.Configs
{
    public abstract class MongoMap<TEntity> where TEntity : class
    {
        protected bool IsClassMapRegistered()
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(TEntity)))
                return true;
            return false;
        }
    }
}
