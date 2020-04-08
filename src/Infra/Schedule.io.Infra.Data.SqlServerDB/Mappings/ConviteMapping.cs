using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.Data.SqlServerDB.Mappings
{
    public class ConviteMapping : IEntityTypeConfiguration<Convite>
    {
        public void Configure(EntityTypeBuilder<Convite> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.CriadoAs)
                .IsRequired();

            builder.Property(c => c.AtualizadoAs)
                .IsRequired();

            builder.Property(c => c.Inativo)
                .IsRequired();

            builder.Property(c => c.EventoId)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.UsuarioId)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.EmailConvidado)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Status)
                .IsRequired()
                .HasColumnType("int");

            builder.OwnsOne(c => c.Permissoes, cm =>
            {
                cm.Property(c => c.ModificaEvento)
                    .HasColumnName("ModificaEvento")
                    .HasColumnType("bit");

                cm.Property(c => c.ConvidaUsuario)
                    .HasColumnName("ConvidaUsuario")
                    .HasColumnType("bit");

                cm.Property(c => c.VeListaDeConvidados)
                    .HasColumnName("VeListaDeConvidados")
                    .HasColumnType("bit");
            });


            builder.ToTable("Convite");
        }
    }
}
