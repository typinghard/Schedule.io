using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Schedule.io.Core.Data;
using Schedule.io.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schedule.io.Infra.Data.SqlServerDB
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        protected readonly AgendaContext Db;
        protected readonly DbSet<TEntity> DbSet;
        protected readonly IConfiguration _configuration;
        protected string _connectionString;
        protected string _table;
        protected string _inativoFalse;

        protected Repository(AgendaContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
            _connectionString = Db.Database.GetDbConnection().ConnectionString;
            _table = typeof(TEntity).ToString().Split(".").Last();
        }

        public virtual void Adicionar(TEntity obj)
        {
            obj.DefinirDataCriacao();
            DbSet.Add(obj);
        }

        public virtual IList<TEntity> Listar()
        {
            return ObterLista($"SELECT * FROM {_table} ").ToList();
        }

        public virtual void Atualizar(TEntity obj)
        {
            obj.DefinirDataAtualizacao();
            DbSet.Update(obj);
        }

        public int SalvarAlteracoes()
        {
            return Db.SaveChanges();
        }

        public virtual IList<TEntity> ObterLista(string query)
        {
            var list = new List<TEntity>();

            using (var con = new SqlConnection(_connectionString))
            {
                try
                {
                    con.Open();
                    list = con.Query<TEntity>(query).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return list;
            }
        }

        public virtual TEntity Obter(string query)
        {
            return ObterLista(query).FirstOrDefault();
        }

        public virtual void Excluir(TEntity obj)
        {
            DbSet.Remove(obj);
        }

        public void Dispose()
        {
            Db?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Inativar(TEntity obj)
        {
            throw new NotImplementedException();
        }
    }
}
