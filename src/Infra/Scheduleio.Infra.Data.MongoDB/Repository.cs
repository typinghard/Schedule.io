using MongoDB.Driver;
using Schedule.io.Core.Core.Data;
using Schedule.io.Core.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schedule.io.Infra.Data.MongoDB
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        protected readonly AgendaContext Db;
        protected readonly IMongoCollection<TEntity> _collection;

        public Repository(AgendaContext context)
        {
            Db = context;
            _collection = Db.Set<TEntity>();
        }

        public void Adicionar(TEntity obj)
        {
            obj.DefinirDataCriacao();
            _collection.InsertOne(Db.Session, obj);
        }

        public IList<TEntity> ObterTodosAtivos()
        {
            return _collection.Find(t => t.Inativo == false).ToList();
        }

        public TEntity ObterPorId(string id)
        {
            return _collection.Find(t => t.Id == id).FirstOrDefault();
        }


        public void Remover(TEntity obj)
        {
            obj.Inativar();
            _collection.ReplaceOne(Db.Session, c => c.Id == obj.Id, obj);
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

        public virtual void ForcarDelecao(string id)
        {
            throw new NotImplementedException();
        }
    }
}
