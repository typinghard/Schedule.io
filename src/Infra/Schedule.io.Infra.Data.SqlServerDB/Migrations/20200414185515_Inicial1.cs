using Microsoft.EntityFrameworkCore.Migrations;

namespace Schedule.io.Infra.Data.SqlServerDB.Migrations
{
    public partial class Inicial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Convite",
                table: "Convite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AgendaUsuario",
                table: "AgendaUsuario");

            migrationBuilder.DropColumn(
                name: "ConvidaUsuario",
                table: "Convite");

            migrationBuilder.DropColumn(
                name: "ModificaEvento",
                table: "Convite");

            migrationBuilder.DropColumn(
                name: "VeListaDeConvidados",
                table: "Convite");

            migrationBuilder.AddColumn<int>(
                name: "PermissoesConviteTempId",
                table: "Convite",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PermissoesAgendaUsuarioTempId",
                table: "AgendaUsuario",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PermissoesAgenda",
                columns: table => new
                {
                    AgendaUsuarioTempId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissoesAgenda", x => x.AgendaUsuarioTempId);
                });

            migrationBuilder.CreateTable(
                name: "PermissoesConvite",
                columns: table => new
                {
                    ConviteTempId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModificaEvento = table.Column<bool>(type: "bit", nullable: false),
                    ConvidaUsuario = table.Column<bool>(type: "bit", nullable: false),
                    VeListaDeConvidados = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissoesConvite", x => x.ConviteTempId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Convite_PermissoesConviteTempId",
                table: "Convite",
                column: "PermissoesConviteTempId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaUsuario_PermissoesAgendaUsuarioTempId",
                table: "AgendaUsuario",
                column: "PermissoesAgendaUsuarioTempId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgendaUsuario_PermissoesAgenda_PermissoesAgendaUsuarioTempId",
                table: "AgendaUsuario",
                column: "PermissoesAgendaUsuarioTempId",
                principalTable: "PermissoesAgenda",
                principalColumn: "AgendaUsuarioTempId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Convite_PermissoesConvite_PermissoesConviteTempId",
                table: "Convite",
                column: "PermissoesConviteTempId",
                principalTable: "PermissoesConvite",
                principalColumn: "ConviteTempId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgendaUsuario_PermissoesAgenda_PermissoesAgendaUsuarioTempId",
                table: "AgendaUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Convite_PermissoesConvite_PermissoesConviteTempId",
                table: "Convite");

            migrationBuilder.DropTable(
                name: "PermissoesAgenda");

            migrationBuilder.DropTable(
                name: "PermissoesConvite");

            migrationBuilder.DropIndex(
                name: "IX_Convite_PermissoesConviteTempId",
                table: "Convite");

            migrationBuilder.DropIndex(
                name: "IX_AgendaUsuario_PermissoesAgendaUsuarioTempId",
                table: "AgendaUsuario");

            migrationBuilder.DropColumn(
                name: "PermissoesConviteTempId",
                table: "Convite");

            migrationBuilder.DropColumn(
                name: "PermissoesAgendaUsuarioTempId",
                table: "AgendaUsuario");

            migrationBuilder.AddColumn<bool>(
                name: "ConvidaUsuario",
                table: "Convite",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ModificaEvento",
                table: "Convite",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "VeListaDeConvidados",
                table: "Convite",
                type: "bit",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Convite",
                table: "Convite",
                column: "EventoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgendaUsuario",
                table: "AgendaUsuario",
                column: "UsuarioId");
        }
    }
}
