using System;
using System.Collections.Generic;
using System.Linq;

namespace Agenda.Core.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Adicionar(TEntity obj);
        TEntity ObterPorId(string id);
        IList<TEntity> ObterTodosAtivos();
        void Atualizar(TEntity obj);
        void ForcarDelecao(string id);
        void Remover(TEntity obj);
        int SalvarAlteracoes();
    }
}
