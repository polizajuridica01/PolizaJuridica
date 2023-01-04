using Microsoft.EntityFrameworkCore.Migrations;

namespace PolizaJuridica.Migrations
{
    public partial class AddDataUserAndRepresentacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioCelular",
                table: "Usuarios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Representacion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelefonoOficina",
                table: "Representacion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioCelular",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Representacion");

            migrationBuilder.DropColumn(
                name: "TelefonoOficina",
                table: "Representacion");
        }
    }
}
