using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Schedule.io.Infra.Data.SqlServerDB.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agenda",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CriadoAs = table.Column<DateTime>(nullable: false),
                    AtualizadoAs = table.Column<DateTime>(nullable: false),
                    Inativo = table.Column<bool>(nullable: false),
                    Titulo = table.Column<string>(type: "varchar(150)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", nullable: true),
                    Publico = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agenda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgendaUsuario",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CriadoAs = table.Column<DateTime>(nullable: false),
                    AtualizadoAs = table.Column<DateTime>(nullable: false),
                    Inativo = table.Column<bool>(nullable: false),
                    AgendaId = table.Column<string>(type: "varchar(200)", nullable: false),
                    UsuarioId = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventoAgenda",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CriadoAs = table.Column<DateTime>(nullable: false),
                    AtualizadoAs = table.Column<DateTime>(nullable: false),
                    Inativo = table.Column<bool>(nullable: false),
                    AgendaId = table.Column<string>(type: "varchar(200)", nullable: false),
                    UsuarioId = table.Column<string>(type: "varchar(200)", nullable: false),
                    IdentificadorExterno = table.Column<string>(nullable: true),
                    Titulo = table.Column<string>(type: "varchar(150)", nullable: false),
                    EventoAgenda_Descricao = table.Column<string>(type: "varchar(500)", nullable: true),
                    LocalId = table.Column<string>(type: "varchar(200)", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataFinal = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataLimiteConfirmacao = table.Column<DateTime>(type: "datetime", nullable: true),
                    QuantidadeMinimaDeUsuarios = table.Column<int>(type: "int", nullable: false),
                    OcupaUsuario = table.Column<bool>(nullable: false),
                    Publico = table.Column<bool>(type: "bit", nullable: false),
                    Tipo_Id = table.Column<string>(nullable: true),
                    Tipo_CriadoAs = table.Column<DateTime>(nullable: true),
                    Tipo_AtualizadoAs = table.Column<DateTime>(nullable: true),
                    Tipo_Inativo = table.Column<bool>(nullable: true),
                    Nome = table.Column<string>(type: "varchar(120)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(500)", nullable: true),
                    Frequencia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoAgenda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Local",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CriadoAs = table.Column<DateTime>(nullable: false),
                    AtualizadoAs = table.Column<DateTime>(nullable: false),
                    Inativo = table.Column<bool>(nullable: false),
                    IdentificadorExterno = table.Column<string>(type: "varchar(200)", nullable: true),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", nullable: true),
                    Reserva = table.Column<bool>(type: "bit", nullable: false),
                    LotacaoMaxima = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Local", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoredEvents",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AggregatedId = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    DataOcorrencia = table.Column<DateTime>(nullable: false),
                    Dados = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CriadoAs = table.Column<DateTime>(nullable: false),
                    AtualizadoAs = table.Column<DateTime>(nullable: false),
                    Inativo = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Convite",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CriadoAs = table.Column<DateTime>(nullable: false),
                    AtualizadoAs = table.Column<DateTime>(nullable: false),
                    Inativo = table.Column<bool>(nullable: false),
                    EventoId = table.Column<string>(type: "varchar(200)", nullable: false),
                    UsuarioId = table.Column<string>(type: "varchar(200)", nullable: false),
                    EmailConvidado = table.Column<string>(type: "varchar(200)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ModificaEvento = table.Column<bool>(type: "bit", nullable: true),
                    ConvidaUsuario = table.Column<bool>(type: "bit", nullable: true),
                    VeListaDeConvidados = table.Column<bool>(type: "bit", nullable: true),
                    EventoAgendaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Convite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Convite_EventoAgenda_EventoAgendaId",
                        column: x => x.EventoAgendaId,
                        principalTable: "EventoAgenda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Convite_EventoAgendaId",
                table: "Convite",
                column: "EventoAgendaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agenda");

            migrationBuilder.DropTable(
                name: "AgendaUsuario");

            migrationBuilder.DropTable(
                name: "Convite");

            migrationBuilder.DropTable(
                name: "Local");

            migrationBuilder.DropTable(
                name: "StoredEvents");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "EventoAgenda");
        }
    }
}
