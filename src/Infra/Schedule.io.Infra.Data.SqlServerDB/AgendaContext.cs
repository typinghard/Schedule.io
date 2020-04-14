using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Schedule.io.Core.Data.Configurations;
using Schedule.io.Core.Data.EventSourcing;
using Schedule.io.Infra.Data.SqlServerDB.Configs;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using System;
using System.Data.Common;
using System.Linq;

namespace Schedule.io.Infra.Data.SqlServerDB
{
    public class AgendaContext : DbContext, IDisposable
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)
        {
            Database.OpenConnection();
        }

        public int SalvarAlteracoes()
        {
            using (var transaction = Database.BeginTransaction())
            {
                try
                {
                    SaveChanges();
                    transaction.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return 0;
                }
            }

        }

        public DbSet<Agenda> Agenda { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<AgendaUsuario> AgendaUsuario { get; set; }
        public DbSet<TipoEvento> TipoEvento { get; set; }
        public DbSet<Evento> Evento { get; set; }
        public DbSet<Convite> Convite { get; set; }
        public DbSet<Local> Local { get; set; }
        public DbSet<StoredEvent> StoredEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AgendaContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(((SqlServerDBConfig)DataBaseConfigurationHelper.DataBaseConfig).ConnectionsString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}