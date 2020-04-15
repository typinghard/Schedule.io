using Microsoft.EntityFrameworkCore;
using Schedule.io.Core.Data.Configurations;
using Schedule.io.Core.Data.EventSourcing;
using Schedule.io.Infra.SqlServerDB.Extensions;
using Schedule.io.Infra.SqlServerDB.Configs;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using System;

namespace Schedule.io.Infra.SqlServerDB
{
    public class AgendaContext : DbContext, IDisposable
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)
        {
            SeedAgendaContext.CriarTabelas(this);
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

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(((SqlServerDBConfig)DataBaseConfigurationHelper.DataBaseConfig).ConnectionsString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}