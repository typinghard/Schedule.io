using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Schedule.io.Core.Core.Data;
using Schedule.io.Core.Core.Data.Configurations;
using Schedule.io.Core.Core.DomainObjects;
using Schedule.io.Infra.Data.SqlServerDB.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.io.Infra.Data.SqlServerDB
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
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

            _inativoFalse = $"Inativo = {0}";
        }

        public void Adicionar(TEntity obj)
        {
            obj.DefinirDataCriacao();
            DbSet.Add(obj);
        }

        public IList<TEntity> ObterTodosAtivos()
        {
            return ObterLista($"SELECT * FROM {_table} WHERE Inativo = {0}").ToList();
        }

        public TEntity ObterPorId(string id)
        {
            return Obter($"SELECT * FROM {_table} WHERE Id = '{id}' and Inativo = {0}");
        }
        public void Remover(TEntity obj)
        {
            obj.Inativar();
            DbSet.Update(obj);
        }

        public void Atualizar(TEntity obj)
        {
            obj.DefinirDataAtualizacao();
            DbSet.Update(obj);
        }

        public int SalvarAlteracoes()
        {
            return Db.SaveChanges();
        }

        public IList<TEntity> ObterLista(string query)
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

        public TEntity Obter(string query)
        {
            return ObterLista(query).FirstOrDefault();
        }

        public void Dispose()
        {
            Db?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void ForcarDelecao(string id)
        {
            throw new NotImplementedException();
        }
    }
}
