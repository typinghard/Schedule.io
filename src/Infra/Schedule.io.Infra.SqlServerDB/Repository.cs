using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Schedule.io.Core.Data;
using Schedule.io.Core.Data.Configurations;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Infra.SqlServerDB.Configs;
using Schedule.io.Infra.SqlServerDB.Statics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schedule.io.Infra.SqlServerDB
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        protected readonly AgendaContext Db;
        protected readonly DbSet<TEntity> DbSet;
        protected readonly IConfiguration _configuration;
        protected string _connectionString;
        private string _table;
        private readonly string _schemaName;
        protected string TabelaAgenda {  get { return string.Concat(_schemaName, ".", Tabelas.Agenda); } }
        protected string TabelaAgendaUsuario { get { return string.Concat(_schemaName, ".", Tabelas.AgendaUsuario); } }

        protected Repository(AgendaContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
            _connectionString = Db.Database.GetDbConnection().ConnectionString;
            _table = string.Concat(_schemaName, ".", typeof(TEntity).ToString().Split(".").Last());
            _schemaName = ((SqlServerDBConfig)DataBaseConfigurationHelper.DataBaseConfig).SchemaName;
        }

        public virtual void Adicionar(TEntity obj)
        {
            obj.DefinirDataCriacao();
            DbSet.Add(obj);
        }

        public virtual IList<TEntity> Listar()
        {
            return ObterLista($"SELECT * FROM { _table } ").ToList();
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

        public virtual TEntity Obter(string id)
        {
            return ObterLista($"SELECT * FROM { _table } WHERE Id = '{ id }'").FirstOrDefault();
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

        public bool Existe(string id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                try
                {
                    con.Open();
                    return con.Query<bool>($"SELECT COUNT(*) FROM { _table } WHERE Id = '{ id }'").FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
