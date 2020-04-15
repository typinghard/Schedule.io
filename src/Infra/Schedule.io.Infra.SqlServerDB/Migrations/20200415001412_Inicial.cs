using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Schedule.io.Infra.SqlServerDB.Migrations
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
                    Titulo = table.Column<string>(type: "varchar(150)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", nullable: true),
                    Publico = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioIdCriador = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agenda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgendaUsuario",
                columns: table => new
                {
                    AgendaId = table.Column<string>(type: "varchar(200)", nullable: false),
                    UsuarioId = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaUsuario", x => new { x.AgendaId, x.UsuarioId });
                });

            migrationBuilder.CreateTable(
                name: "Convite",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(type: "varchar(200)", nullable: false),
                    EventoId = table.Column<string>(type: "varchar(200)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ModificaEvento = table.Column<bool>(type: "bit", nullable: true),
                    ConvidaUsuario = table.Column<bool>(type: "bit", nullable: true),
                    VeListaDeConvidados = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Convite", x => new { x.EventoId, x.UsuarioId });
                });

            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CriadoAs = table.Column<DateTime>(nullable: false),
                    AtualizadoAs = table.Column<DateTime>(nullable: false),
                    AgendaId = table.Column<string>(type: "varchar(200)", nullable: false),
                    UsuarioIdCriador = table.Column<string>(type: "varchar(200)", nullable: false),
                    IdTipoEvento = table.Column<string>(nullable: true),
                    IdentificadorExterno = table.Column<string>(nullable: true),
                    Titulo = table.Column<string>(type: "varchar(150)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", nullable: true),
                    LocalId = table.Column<string>(type: "varchar(200)", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataFinal = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataLimiteConfirmacao = table.Column<DateTime>(type: "datetime", nullable: true),
                    QuantidadeMinimaDeUsuarios = table.Column<int>(type: "int", nullable: false),
                    OcupaUsuario = table.Column<bool>(nullable: false),
                    Publico = table.Column<bool>(type: "bit", nullable: false),
                    Frequencia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Local",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CriadoAs = table.Column<DateTime>(nullable: false),
                    AtualizadoAs = table.Column<DateTime>(nullable: false),
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
                name: "TipoEvento",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CriadoAs = table.Column<DateTime>(nullable: false),
                    AtualizadoAs = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(120)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEvento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CriadoAs = table.Column<DateTime>(nullable: false),
                    AtualizadoAs = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });
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
                name: "Evento");

            migrationBuilder.DropTable(
                name: "Local");

            migrationBuilder.DropTable(
                name: "StoredEvents");

            migrationBuilder.DropTable(
                name: "TipoEvento");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
