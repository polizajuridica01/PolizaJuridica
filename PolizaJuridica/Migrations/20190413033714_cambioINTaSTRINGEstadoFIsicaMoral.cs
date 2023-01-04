using Microsoft.EntityFrameworkCore.Migrations;

namespace PolizaJuridica.Migrations
{
    public partial class cambioINTaSTRINGEstadoFIsicaMoral : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SFisicaEstadoTrabajo",
                table: "FisicaMoral",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "SFisicaEstado",
                table: "FisicaMoral",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "EstadoRL",
                table: "FisicaMoral",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SFisicaEstadoTrabajo",
                table: "FisicaMoral",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SFisicaEstado",
                table: "FisicaMoral",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EstadoRL",
                table: "FisicaMoral",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
