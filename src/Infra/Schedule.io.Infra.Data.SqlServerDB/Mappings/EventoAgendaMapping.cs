using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.Data.SqlServerDB.Mappings
{
    public class EventoAgendaMapping : IEntityTypeConfiguration<EventoAgenda>
    {
        public void Configure(EntityTypeBuilder<EventoAgenda> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CriadoAs)
                .IsRequired();

            builder.Property(c => c.AtualizadoAs)
                .IsRequired();

            builder.Property(c => c.Inativo)
                .IsRequired();

            builder.Property(c => c.AgendaId)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.UsuarioId)
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


            builder.OwnsOne(c => c.Tipo, cm =>
            {
                cm.Property(c => c.Nome)
                    .HasColumnName("Nome")
                    .HasColumnType("varchar(120)");

                cm.Property(c => c.Descricao)
                    .HasColumnName("Descricao")
                    .HasColumnType("varchar(500)");
            });


            builder.Property(c => c.Frequencia)
                .HasColumnType("int");

            builder.ToTable("EventoAgenda");
        }
    }
}
