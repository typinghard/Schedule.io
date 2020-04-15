using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.io.Core.Data.Configurations;
using Schedule.io.Infra.SqlServerDB.Configs;
using Schedule.io.Models.ValueObjects;

namespace Schedule.io.Infra.SqlServerDB.Mappings
{
    public class AgendaUsuarioMapping : IEntityTypeConfiguration<AgendaUsuario>
    {
        public void Configure(EntityTypeBuilder<AgendaUsuario> builder)
        {
            builder.Property(c => c.AgendaId)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.UsuarioId)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.HasKey(c => new { c.AgendaId, c.UsuarioId });

            builder.ToTable("AgendaUsuario", ((SqlServerDBConfig)DataBaseConfigurationHelper.DataBaseConfig).SchemaName);
        }
    }
}
