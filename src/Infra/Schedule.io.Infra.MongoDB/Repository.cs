using MongoDB.Driver;
using Schedule.io.Core.Data;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Infra.MongoDB.Configs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schedule.io.Infra.MongoDB
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        protected readonly ScheduleioContext Db;
        protected readonly IMongoCollection<TEntity> _collection;

        public Repository(ScheduleioContext context)
        {
            Db = context;
            _collection = Db.Set<TEntity>();
        }

        public void Adicionar(TEntity obj)
        {
            obj.DefinirDataCriacao();
            _collection.InsertOne(Db.Session, obj);
        }

        public IList<TEntity> Listar()
        {
            return _collection.AsQueryable().ToList();
        }

        public TEntity Obter(string id)
        {
            return _collection.Find(t => t.Id == id).FirstOrDefault();
        }

        public void Atualizar(TEntity obj)
        {
            obj.DefinirDataAtualizacao();
            _collection.ReplaceOne(Db.Session, c => c.Id == obj.Id, obj);
        }

        public int SalvarAlteracoes()
        {
            return Db.SalvarAlteracoes();
        }
        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Excluir(TEntity obj)
        {
            _collection.DeleteOne(c => c.Id == obj.Id);
        }
    }
}
