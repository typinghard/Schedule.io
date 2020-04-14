using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Infra.Data.SqlServerDB.Mappings
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

            builder.Property(c => c.Titulo)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Publico)
                .HasColumnType("bit");


            builder.ToTable("Agenda");
        }
    }
}
