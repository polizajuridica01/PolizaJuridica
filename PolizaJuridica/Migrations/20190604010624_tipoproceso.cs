using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PolizaJuridica.Migrations
{
    public partial class tipoproceso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoProcesoId",
                table: "UsuariosSolicitud",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TipoProceso",
                columns: table => new
                {
                    TipoProcesoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    descripcion = table.Column<string>(nullable: true),
                    orden = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoProceso", x => x.TipoProcesoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosSolicitud_TipoProcesoId",
                table: "UsuariosSolicitud",
                column: "TipoProcesoId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosSolicitud_TipoProceso_TipoProcesoId",
                table: "UsuariosSolicitud",
                column: "TipoProcesoId",
                principalTable: "TipoProceso",
                principalColumn: "TipoProcesoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosSolicitud_TipoProceso_TipoProcesoId",
                table: "UsuariosSolicitud");

            migrationBuilder.DropTable(
                name: "TipoProceso");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosSolicitud_TipoProcesoId",
                table: "UsuariosSolicitud");

            migrationBuilder.DropColumn(
                name: "TipoProcesoId",
                table: "UsuariosSolicitud");
        }
    }
}
