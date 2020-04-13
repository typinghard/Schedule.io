using Raven.Client.Documents.Session;
using Schedule.io.Core.Data;
using Schedule.io.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schedule.io.Infra.RavenDB
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        protected readonly IDocumentSession _session;

        public Repository(IDocumentSession session)
        {
            _session = session;
        }
        public void Adicionar(TEntity obj)
        {
            obj.DefinirDataCriacao();
            _session.Store(obj);
        }

        public void Atualizar(TEntity obj)
        {
            obj.DefinirDataAtualizacao();
            _session.Store(obj);
        }

        public TEntity Obter(string id)
        {
            return _session
                 .Query<TEntity>()
                 .Where(x => x.Id == id)
                 .FirstOrDefault();
        }

        public IList<TEntity> Listar()
        {
            return _session
                 .Query<TEntity>()
                 .ToList();
        }

        public void Remover(TEntity obj)
        {
            _session.Delete(obj);
        }

        protected IDocumentSession Sessao { get { return _session; } }
        public int SalvarAlteracoes()
        {
            try
            {
                _session.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
