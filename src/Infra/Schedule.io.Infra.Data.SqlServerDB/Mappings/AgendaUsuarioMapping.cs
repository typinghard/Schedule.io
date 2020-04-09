using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Infra.Data.SqlServerDB.Mappings
{
    public class AgendaUsuarioMapping : IEntityTypeConfiguration<AgendaUsuario>
    {
        public void Configure(EntityTypeBuilder<AgendaUsuario> builder)
        {
            builder.HasKey(x => x.Id);

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

            builder.OwnsOne(c => c.Permissoes, cm =>
            {

            });

            builder.ToTable("AgendaUsuario");
        }
    }
}
