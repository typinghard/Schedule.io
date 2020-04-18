using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.io.Core.Data.Configurations;
using Schedule.io.Core.Data.EventSourcing;
using Schedule.io.Infra.SqlServerDB.Configs;

namespace Schedule.io.Infra.SqlServerDB.EventSourcing
{
    public class StoredEventMapping : IEntityTypeConfiguration<StoredEvent>
    {
        public void Configure(EntityTypeBuilder<StoredEvent> builder)
        {
            builder.Property(c => c.Id)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.AggregatedId)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Tipo)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.DataOcorrencia)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(c => c.Dados)
                .IsRequired();

            builder.HasKey(c => new { c.Id, c.AggregatedId });
            builder.ToTable("StoredEvents", ((SqlServerDBConfig)DataBaseConfigurationHelper.DataBaseConfig).SchemaName);
        }
    }
}
