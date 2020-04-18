using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.io.Core.Data.Configurations;
using Schedule.io.Infra.SqlServerDB.Configs;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Infra.SqlServerDB.Mappings
{
    public class LocalMapping : IEntityTypeConfiguration<Local>
    {
        public void Configure(EntityTypeBuilder<Local> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.CriadoAs)
                .IsRequired();

            builder.Property(c => c.AtualizadoAs)
                .IsRequired();

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.IdentificadorExterno)
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Reserva)
                .HasColumnType("bit");

            builder.Property(c => c.LotacaoMaxima)
                .HasColumnType("int");

            builder.ToTable("Local", ((SqlServerDBConfig)DataBaseConfigurationHelper.DataBaseConfig).SchemaName);
        }
    }
}
