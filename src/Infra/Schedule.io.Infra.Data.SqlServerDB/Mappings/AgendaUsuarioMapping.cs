using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.io.Models.ValueObjects;

namespace Schedule.io.Infra.Data.SqlServerDB.Mappings
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

            builder.OwnsOne(c => c.Permissoes, cm =>
            {

            });

            builder.ToTable("AgendaUsuario");
        }
    }
}
