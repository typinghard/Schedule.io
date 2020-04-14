using Microsoft.EntityFrameworkCore.Migrations;

namespace Schedule.io.Infra.Data.SqlServerDB.Migrations
{
    public partial class Inicial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgendaUsuario_PermissoesAgenda_PermissoesAgendaUsuarioTempId",
                table: "AgendaUsuario");

            migrationBuilder.DropTable(
                name: "PermissoesAgenda");

            migrationBuilder.DropIndex(
                name: "IX_AgendaUsuario_PermissoesAgendaUsuarioTempId",
                table: "AgendaUsuario");

            migrationBuilder.DropColumn(
                name: "PermissoesAgendaUsuarioTempId",
                table: "AgendaUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgendaUsuario",
                table: "AgendaUsuario",
                column: "AgendaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AgendaUsuario",
                table: "AgendaUsuario");

            migrationBuilder.AddColumn<int>(
                name: "PermissoesAgendaUsuarioTempId",
                table: "AgendaUsuario",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PermissoesAgenda",
                columns: table => new
                {
                    AgendaUsuarioTempId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissoesAgenda", x => x.AgendaUsuarioTempId);
                });

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
        }
    }
}
