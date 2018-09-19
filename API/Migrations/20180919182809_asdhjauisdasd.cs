using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class asdhjauisdasd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respuestas_PreguntaOpcion_RespuestaOpcionId",
                table: "Respuestas");

            migrationBuilder.DropIndex(
                name: "IX_Respuestas_RespuestaOpcionId",
                table: "Respuestas");

            migrationBuilder.DropColumn(
                name: "RespuestaOpcionId",
                table: "Respuestas");

            migrationBuilder.AddColumn<int>(
                name: "PreguntaOpcionId",
                table: "Respuestas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RespuestaMOId",
                table: "PreguntaOpcion",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OpcionRespuesta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OpcionId = table.Column<int>(nullable: false),
                    RespuestaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcionRespuesta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpcionRespuesta_PreguntaOpcion_Id",
                        column: x => x.Id,
                        principalTable: "PreguntaOpcion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpcionRespuesta_Respuestas_Id",
                        column: x => x.Id,
                        principalTable: "Respuestas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_PreguntaOpcionId",
                table: "Respuestas",
                column: "PreguntaOpcionId");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntaOpcion_RespuestaMOId",
                table: "PreguntaOpcion",
                column: "RespuestaMOId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreguntaOpcion_Respuestas_RespuestaMOId",
                table: "PreguntaOpcion",
                column: "RespuestaMOId",
                principalTable: "Respuestas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Respuestas_PreguntaOpcion_PreguntaOpcionId",
                table: "Respuestas",
                column: "PreguntaOpcionId",
                principalTable: "PreguntaOpcion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreguntaOpcion_Respuestas_RespuestaMOId",
                table: "PreguntaOpcion");

            migrationBuilder.DropForeignKey(
                name: "FK_Respuestas_PreguntaOpcion_PreguntaOpcionId",
                table: "Respuestas");

            migrationBuilder.DropTable(
                name: "OpcionRespuesta");

            migrationBuilder.DropIndex(
                name: "IX_Respuestas_PreguntaOpcionId",
                table: "Respuestas");

            migrationBuilder.DropIndex(
                name: "IX_PreguntaOpcion_RespuestaMOId",
                table: "PreguntaOpcion");

            migrationBuilder.DropColumn(
                name: "PreguntaOpcionId",
                table: "Respuestas");

            migrationBuilder.DropColumn(
                name: "RespuestaMOId",
                table: "PreguntaOpcion");

            migrationBuilder.AddColumn<int>(
                name: "RespuestaOpcionId",
                table: "Respuestas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_RespuestaOpcionId",
                table: "Respuestas",
                column: "RespuestaOpcionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Respuestas_PreguntaOpcion_RespuestaOpcionId",
                table: "Respuestas",
                column: "RespuestaOpcionId",
                principalTable: "PreguntaOpcion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
