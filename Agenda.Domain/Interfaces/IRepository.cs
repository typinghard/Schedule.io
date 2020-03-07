using System;
using System.Collections.Generic;

namespace Agenda.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Adicionar(TEntity obj);
        TEntity ObterPorId(Guid id);
        IList<TEntity> ObterTodosAtivos();
        void Atualizar(TEntity obj);
        void Remover(TEntity obj);
        void ForcarDelecao(Guid id);
        int SalvarAlteracoes();
    }
}
