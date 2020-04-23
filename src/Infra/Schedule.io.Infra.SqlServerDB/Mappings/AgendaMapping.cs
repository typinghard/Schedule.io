using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.io.Core.Data.Configurations;
using Schedule.io.Infra.SqlServerDB.Configs;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Infra.SqlServerDB.Mappings
{
    public class AgendaMapping : IEntityTypeConfiguration<Agenda>
    {
        public void Configure(EntityTypeBuilder<Agenda> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.CriadoAs)
                .IsRequired();

            builder.Property(c => c.AtualizadoAs)
                .IsRequired();

            builder.Property(c => c.UsuarioIdCriador)
                .IsRequired();

            builder.Property(c => c.Titulo)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Publico)
                .HasColumnType("bit");

            builder.Ignore(c => c.Usuarios);
            builder.Ignore(c => c.Eventos);

            builder.ToTable("Agenda", ((SqlServerDBConfig)DataBaseConfigurationHelper.DataBaseConfig).SchemaName);
        }
    }
}
