using Microsoft.EntityFrameworkCore.Migrations;

namespace PolizaJuridica.Migrations
{
    public partial class complementodomicilio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlcaldiaMunicipioActual",
                table: "Solicitud",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlcaldiaMunicipioArrendar",
                table: "Solicitud",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodigoPostalActual",
                table: "Solicitud",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodigoPostalArrendar",
                table: "Solicitud",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColoniaActual",
                table: "Solicitud",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColoniaArrendar",
                table: "Solicitud",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstadoActual",
                table: "Solicitud",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstadoArrendar",
                table: "Solicitud",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlcaldiaMunicipioActual",
                table: "Solicitud");

            migrationBuilder.DropColumn(
                name: "AlcaldiaMunicipioArrendar",
                table: "Solicitud");

            migrationBuilder.DropColumn(
                name: "CodigoPostalActual",
                table: "Solicitud");

            migrationBuilder.DropColumn(
                name: "CodigoPostalArrendar",
                table: "Solicitud");

            migrationBuilder.DropColumn(
                name: "ColoniaActual",
                table: "Solicitud");

            migrationBuilder.DropColumn(
                name: "ColoniaArrendar",
                table: "Solicitud");

            migrationBuilder.DropColumn(
                name: "EstadoActual",
                table: "Solicitud");

            migrationBuilder.DropColumn(
                name: "EstadoArrendar",
                table: "Solicitud");
        }
    }
}
