using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.io.Core.Data.Configurations;
using Schedule.io.Infra.SqlServerDB.Configs;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Infra.SqlServerDB.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.CriadoAs)
                .IsRequired();

            builder.Property(c => c.AtualizadoAs)
                .IsRequired();

            builder.Property(c => c.Email)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.ToTable("Usuario", ((SqlServerDBConfig)DataBaseConfigurationHelper.DataBaseConfig).SchemaName);
        }
    }
}
