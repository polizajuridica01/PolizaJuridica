using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PolizaJuridica.Migrations
{
    public partial class DatosEscritura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EscrituraNumero",
                table: "FiadorM",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "FiadorM",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FolioMercantil",
                table: "FiadorM",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Licenciado1",
                table: "FiadorM",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Licenciado2",
                table: "FiadorM",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreApoderado",
                table: "FiadorM",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotariaNumero1",
                table: "FiadorM",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotariaNumero2",
                table: "FiadorM",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NumeralFecha",
                table: "FiadorM",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RazonSocial",
                table: "FiadorM",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistritoJudicial",
                table: "FiadorF",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EscrituraNumero",
                table: "FiadorF",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Licenciado",
                table: "FiadorF",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroNotaria",
                table: "FiadorF",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PartidaFecha",
                table: "FiadorF",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PartidaLibro",
                table: "FiadorF",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartidaNumero",
                table: "FiadorF",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartidaSeccion",
                table: "FiadorF",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartidaVolumen",
                table: "FiadorF",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EscrituraNumero",
                table: "FiadorM");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "FiadorM");

            migrationBuilder.DropColumn(
                name: "FolioMercantil",
                table: "FiadorM");

            migrationBuilder.DropColumn(
                name: "Licenciado1",
                table: "FiadorM");

            migrationBuilder.DropColumn(
                name: "Licenciado2",
                table: "FiadorM");

            migrationBuilder.DropColumn(
                name: "NombreApoderado",
                table: "FiadorM");

            migrationBuilder.DropColumn(
                name: "NotariaNumero1",
                table: "FiadorM");

            migrationBuilder.DropColumn(
                name: "NotariaNumero2",
                table: "FiadorM");

            migrationBuilder.DropColumn(
                name: "NumeralFecha",
                table: "FiadorM");

            migrationBuilder.DropColumn(
                name: "RazonSocial",
                table: "FiadorM");

            migrationBuilder.DropColumn(
                name: "DistritoJudicial",
                table: "FiadorF");

            migrationBuilder.DropColumn(
                name: "EscrituraNumero",
                table: "FiadorF");

            migrationBuilder.DropColumn(
                name: "Licenciado",
                table: "FiadorF");

            migrationBuilder.DropColumn(
                name: "NumeroNotaria",
                table: "FiadorF");

            migrationBuilder.DropColumn(
                name: "PartidaFecha",
                table: "FiadorF");

            migrationBuilder.DropColumn(
                name: "PartidaLibro",
                table: "FiadorF");

            migrationBuilder.DropColumn(
                name: "PartidaNumero",
                table: "FiadorF");

            migrationBuilder.DropColumn(
                name: "PartidaSeccion",
                table: "FiadorF");

            migrationBuilder.DropColumn(
                name: "PartidaVolumen",
                table: "FiadorF");
        }
    }
}
