using Agenda.Core.Data;
using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Interfaces;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schedule.io.Infra.RavenDB
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        protected readonly IDocumentSession _session;
        private readonly string DocumentName;

        public Repository(IDocumentSession session)
        {
            DocumentName = typeof(TEntity).Name.ToLower();
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void ForcarDelecao(string id)
        {
            throw new NotImplementedException();
            //_session.Delete(id.ToString());
        }

        public TEntity ObterPorId(string id)
        {
            return _session.Advanced.RawQuery<TEntity>(
            "from " + DocumentName + " " +
            "where Id == $Id")
                .AddParameter("Id", id.ToString())
                .FirstOrDefault();
        }

        public IList<TEntity> ObterTodosAtivos()
        {
            return _session.Advanced.RawQuery<TEntity>(
            "from " + DocumentName + " " +
            "where Inativo == false").ToList();
        }

        public void Remover(TEntity obj)
        {
            obj.Inativar();
            _session.Store(obj);
        }

        protected IDocumentSession Sessao { get { return _session; } }
        public int SalvarAlteracoes()
        {
            try
            {
                _session.SaveChanges();
                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
    }
}
