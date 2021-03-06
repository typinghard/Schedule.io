﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.io.Core.Data.Configurations;
using Schedule.io.Infra.SqlServerDB.Configs;
using Schedule.io.Models.ValueObjects;

namespace Schedule.io.Infra.SqlServerDB.Mappings
{
    public class ConviteMapping : IEntityTypeConfiguration<Convite>
    {
        public void Configure(EntityTypeBuilder<Convite> builder)
        {
            builder.Property(c => c.EventoId)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.UsuarioId)
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Status)
                .IsRequired()
                .HasColumnType("int");

            builder.OwnsOne(c => c.Permissoes, per =>
            {
                per.Property(c => c.ModificaEvento)
                    .HasColumnName("ModificaEvento")
                    .IsRequired()
                    .HasColumnType("bit");

                per.Property(c => c.ConvidaUsuario)
                    .HasColumnName("ConvidaUsuario")
                    .IsRequired()
                    .HasColumnType("bit");

                per.Property(c => c.VeListaDeConvidados)
                    .HasColumnName("VeListaDeConvidados")
                    .IsRequired()
                    .HasColumnType("bit");
            });

            builder.HasKey(c => new { c.EventoId, c.UsuarioId });
            builder.ToTable("Convite", ((SqlServerDBConfig)DataBaseConfigurationHelper.DataBaseConfig).SchemaName);
        }
    }
}
