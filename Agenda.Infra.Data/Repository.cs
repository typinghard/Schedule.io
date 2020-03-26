using Agenda.Core.Data;
using Agenda.Domain.Core.DomainObjects;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agenda.Infra.Data
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        protected readonly AgendaContext Db;
        protected readonly IMongoCollection<TEntity> _collection;

        public Repository(AgendaContext context)
        {
            Db = context;

            if (!Db.Session.IsInTransaction)
                Db.Session.StartTransaction();

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

        public TEntity ObterPorId(Guid id)
        {
            return _collection.Find(t => t.Id == id && t.Inativo == false).FirstOrDefault();
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

        public virtual void ForcarDelecao(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
