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
    [Migration("20200415001412_Inicial")]
    partial class Inicial
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
                    b.Property<string>("EventoId")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("EventoId", "UsuarioId");

                    b.ToTable("Convite");
                });

            modelBuilder.Entity("Schedule.io.Models.ValueObjects.Convite", b =>
                {
                    b.OwnsOne("Schedule.io.Models.ValueObjects.PermissoesConvite", "Permissoes", b1 =>
                        {
                            b1.Property<string>("ConviteEventoId")
                                .HasColumnType("varchar(200)");

                            b1.Property<string>("ConviteUsuarioId")
                                .HasColumnType("varchar(200)");

                            b1.Property<bool>("ConvidaUsuario")
                                .HasColumnName("ConvidaUsuario")
                                .HasColumnType("bit");

                            b1.Property<bool>("ModificaEvento")
                                .HasColumnName("ModificaEvento")
                                .HasColumnType("bit");

                            b1.Property<bool>("VeListaDeConvidados")
                                .HasColumnName("VeListaDeConvidados")
                                .HasColumnType("bit");

                            b1.HasKey("ConviteEventoId", "ConviteUsuarioId");

                            b1.ToTable("Convite");

                            b1.WithOwner()
                                .HasForeignKey("ConviteEventoId", "ConviteUsuarioId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}