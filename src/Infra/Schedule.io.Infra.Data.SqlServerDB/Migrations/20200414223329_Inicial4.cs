using Microsoft.EntityFrameworkCore.Migrations;

namespace Schedule.io.Infra.Data.SqlServerDB.Migrations
{
    public partial class Inicial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AgendaUsuario",
                table: "AgendaUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgendaUsuario",
                table: "AgendaUsuario",
                columns: new[] { "AgendaId", "UsuarioId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AgendaUsuario",
                table: "AgendaUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgendaUsuario",
                table: "AgendaUsuario",
                column: "AgendaId");
        }
    }
}
