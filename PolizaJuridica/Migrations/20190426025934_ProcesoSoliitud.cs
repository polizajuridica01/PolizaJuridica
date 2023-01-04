using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PolizaJuridica.Migrations
{
    public partial class ProcesoSoliitud : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SolicitudProceso",
                columns: table => new
                {
                    SolicitudProcesoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SolicitudProcesoEstatus = table.Column<int>(nullable: false),
                    MotivoCancelacíon = table.Column<string>(nullable: true),
                    fecha = table.Column<string>(nullable: true),
                    SolicitudUsuarioId = table.Column<int>(nullable: false),
                    SolicitudUsuario = table.Column<string>(nullable: true),
                    SolicitudId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudProceso", x => x.SolicitudProcesoId);
                    table.ForeignKey(
                        name: "FK_SolicitudProceso_Solicitud_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "Solicitud",
                        principalColumn: "SolicitudId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudProceso_SolicitudId",
                table: "SolicitudProceso",
                column: "SolicitudId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitudProceso");
        }
    }
}
