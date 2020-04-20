using Schedule.io.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace Schedule.io.Core.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity, IAggregateRoot
    {
        void Adicionar(TEntity obj);
        TEntity Obter(string id);
        bool Existe(string id);
        IList<TEntity> Listar();
        void Atualizar(TEntity obj);
        void Excluir(TEntity obj);
        int SalvarAlteracoes();
    }
}
