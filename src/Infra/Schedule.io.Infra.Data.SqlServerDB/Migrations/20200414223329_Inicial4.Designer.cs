﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Schedule.io.Infra.Data.SqlServerDB;

namespace Schedule.io.Infra.Data.SqlServerDB.Migrations
{
    [DbContext(typeof(AgendaContext))]
    [Migration("20200414223329_Inicial4")]
    partial class Inicial4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Schedule.io.Core.Data.EventSourcing.StoredEvent", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AggregatedId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dados")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataOcorrencia")
                        .HasColumnType("datetime2");

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StoredEvents");
                });

            modelBuilder.Entity("Schedule.io.Models.AggregatesRoots.Agenda", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("AtualizadoAs")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CriadoAs")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(500)");

                    b.Property<bool>("Publico")
                        .HasColumnType("bit");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("UsuarioIdCriador")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Agenda");
                });

            modelBuilder.Entity("Schedule.io.Models.AggregatesRoots.Evento", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AgendaId")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("AtualizadoAs")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CriadoAs")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataFinal")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DataLimiteConfirmacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(500)");

                    b.Property<int>("Frequencia")
                        .HasColumnType("int");

                    b.Property<string>("IdTipoEvento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentificadorExterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocalId")
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("OcupaUsuario")
                        .HasColumnType("bit");

                    b.Property<bool>("Publico")
                        .HasColumnType("bit");

                    b.Property<int>("QuantidadeMinimaDeUsuarios")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("UsuarioIdCriador")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Evento");
                });

            modelBuilder.Entity("Schedule.io.Models.AggregatesRoots.Local", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("AtualizadoAs")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CriadoAs")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("IdentificadorExterno")
                        .HasColumnType("varchar(200)");

                    b.Property<int>("LotacaoMaxima")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("Reserva")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Local");
                });

            modelBuilder.Entity("Schedule.io.Models.AggregatesRoots.TipoEvento", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("AtualizadoAs")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CriadoAs")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(120)");

                    b.HasKey("Id");

                    b.ToTable("TipoEvento");
                });

            modelBuilder.Entity("Schedule.io.Models.AggregatesRoots.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("AtualizadoAs")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CriadoAs")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Schedule.io.Models.ValueObjects.AgendaUsuario", b =>
                {
                    b.Property<string>("AgendaId")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("varchar(200)");

                    b.HasKey("AgendaId", "UsuarioId");

                    b.ToTable("AgendaUsuario");
                });

            modelBuilder.Entity("Schedule.io.Models.ValueObjects.Convite", b =>
                {
                    b.Property<string>("EmailConvidado")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("EventoId")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int?>("PermissoesConviteTempId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("varchar(200)");

                    b.HasIndex("PermissoesConviteTempId");

                    b.ToTable("Convite");
                });

            modelBuilder.Entity("Schedule.io.Models.ValueObjects.PermissoesConvite", b =>
                {
                    b.Property<int>("ConviteTempId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ConvidaUsuario")
                        .HasColumnName("ConvidaUsuario")
                        .HasColumnType("bit");

                    b.Property<bool>("ModificaEvento")
                        .HasColumnName("ModificaEvento")
                        .HasColumnType("bit");

                    b.Property<bool>("VeListaDeConvidados")
                        .HasColumnName("VeListaDeConvidados")
                        .HasColumnType("bit");

                    b.HasKey("ConviteTempId");

                    b.ToTable("PermissoesConvite");
                });

            modelBuilder.Entity("Schedule.io.Models.ValueObjects.Convite", b =>
                {
                    b.HasOne("Schedule.io.Models.ValueObjects.PermissoesConvite", "Permissoes")
                        .WithMany()
                        .HasForeignKey("PermissoesConviteTempId");
                });
#pragma warning restore 612, 618
        }
    }
}
