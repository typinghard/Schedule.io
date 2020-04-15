using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.io.Models.AggregatesRoots;

namespace Schedule.io.Infra.Data.SqlServerDB.Mappings
{
    public class EventoAgendaMapping : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CriadoAs)
                .IsRequired();

            builder.Property(c => c.AtualizadoAs)
                .IsRequired();


            builder.Property(c => c.AgendaId)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.UsuarioIdCriador)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Titulo)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(500)");

            builder.Property(c => c.LocalId)
                .HasColumnType("varchar(200)");

            builder.Property(c => c.DataInicio)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(c => c.DataFinal)
                .HasColumnType("datetime");

            builder.Property(c => c.DataLimiteConfirmacao)
                .HasColumnType("datetime");

            builder.Property(c => c.QuantidadeMinimaDeUsuarios)
                .HasColumnType("int");

            builder.Property(c => c.Publico)
                .HasColumnType("bit");

            builder.Property(c => c.Frequencia)
                .HasColumnType("int");


            builder.Ignore(c => c.Convites);

            builder.ToTable("Evento");
        }
    }
}
