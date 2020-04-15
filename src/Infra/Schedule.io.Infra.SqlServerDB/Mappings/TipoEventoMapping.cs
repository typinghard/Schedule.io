using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.io.Core.Data.Configurations;
using Schedule.io.Infra.SqlServerDB.Configs;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Infra.SqlServerDB.Mappings
{
    public class TipoEventoMapping : IEntityTypeConfiguration<TipoEvento>
    {
        public void Configure(EntityTypeBuilder<TipoEvento> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.CriadoAs)
                .IsRequired();

            builder.Property(c => c.AtualizadoAs)
                .IsRequired();

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(120)");

            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(500)");

            builder.ToTable("TipoEvento", ((SqlServerDBConfig)DataBaseConfigurationHelper.DataBaseConfig).SchemaName);
        }
    }
}
