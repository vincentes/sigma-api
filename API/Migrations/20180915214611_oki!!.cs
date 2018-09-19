using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class oki : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pregunta_EncuestasGlobales_EncuestaGlobalId",
                table: "Pregunta");

            migrationBuilder.DropForeignKey(
                name: "FK_PreguntaOpcion_Pregunta_PreguntaId",
                table: "PreguntaOpcion");

            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_AspNetUsers_AlumnoId",
                table: "Respuesta");

            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_Pregunta_PreguntaId",
                table: "Respuesta");

            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_Pregunta_PreguntaVariadaId",
                table: "Respuesta");

            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_PreguntaOpcion_RespuestaOpcionId",
                table: "Respuesta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Respuesta",
                table: "Respuesta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pregunta",
                table: "Pregunta");

            migrationBuilder.RenameTable(
                name: "Respuesta",
                newName: "Respuestas");

            migrationBuilder.RenameTable(
                name: "Pregunta",
                newName: "Preguntas");

            migrationBuilder.RenameIndex(
                name: "IX_Respuesta_RespuestaOpcionId",
                table: "Respuestas",
                newName: "IX_Respuestas_RespuestaOpcionId");

            migrationBuilder.RenameIndex(
                name: "IX_Respuesta_PreguntaVariadaId",
                table: "Respuestas",
                newName: "IX_Respuestas_PreguntaVariadaId");

            migrationBuilder.RenameIndex(
                name: "IX_Respuesta_PreguntaId",
                table: "Respuestas",
                newName: "IX_Respuestas_PreguntaId");

            migrationBuilder.RenameIndex(
                name: "IX_Respuesta_AlumnoId",
                table: "Respuestas",
                newName: "IX_Respuestas_AlumnoId");

            migrationBuilder.RenameIndex(
                name: "IX_Pregunta_EncuestaGlobalId",
                table: "Preguntas",
                newName: "IX_Preguntas_EncuestaGlobalId");

            migrationBuilder.AddColumn<int>(
                name: "PreguntaLibreId",
                table: "Respuestas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Texto",
                table: "Respuestas",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Respuestas",
                table: "Respuestas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Preguntas",
                table: "Preguntas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_PreguntaLibreId",
                table: "Respuestas",
                column: "PreguntaLibreId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreguntaOpcion_Preguntas_PreguntaId",
                table: "PreguntaOpcion",
                column: "PreguntaId",
                principalTable: "Preguntas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Preguntas_EncuestasGlobales_EncuestaGlobalId",
                table: "Preguntas",
                column: "EncuestaGlobalId",
                principalTable: "EncuestasGlobales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Respuestas_AspNetUsers_AlumnoId",
                table: "Respuestas",
                column: "AlumnoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Respuestas_Preguntas_PreguntaId",
                table: "Respuestas",
                column: "PreguntaId",
                principalTable: "Preguntas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Respuestas_Preguntas_PreguntaLibreId",
                table: "Respuestas",
                column: "PreguntaLibreId",
                principalTable: "Preguntas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Respuestas_Preguntas_PreguntaVariadaId",
                table: "Respuestas",
                column: "PreguntaVariadaId",
                principalTable: "Preguntas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Respuestas_PreguntaOpcion_RespuestaOpcionId",
                table: "Respuestas",
                column: "RespuestaOpcionId",
                principalTable: "PreguntaOpcion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreguntaOpcion_Preguntas_PreguntaId",
                table: "PreguntaOpcion");

            migrationBuilder.DropForeignKey(
                name: "FK_Preguntas_EncuestasGlobales_EncuestaGlobalId",
                table: "Preguntas");

            migrationBuilder.DropForeignKey(
                name: "FK_Respuestas_AspNetUsers_AlumnoId",
                table: "Respuestas");

            migrationBuilder.DropForeignKey(
                name: "FK_Respuestas_Preguntas_PreguntaId",
                table: "Respuestas");

            migrationBuilder.DropForeignKey(
                name: "FK_Respuestas_Preguntas_PreguntaLibreId",
                table: "Respuestas");

            migrationBuilder.DropForeignKey(
                name: "FK_Respuestas_Preguntas_PreguntaVariadaId",
                table: "Respuestas");

            migrationBuilder.DropForeignKey(
                name: "FK_Respuestas_PreguntaOpcion_RespuestaOpcionId",
                table: "Respuestas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Respuestas",
                table: "Respuestas");

            migrationBuilder.DropIndex(
                name: "IX_Respuestas_PreguntaLibreId",
                table: "Respuestas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Preguntas",
                table: "Preguntas");

            migrationBuilder.DropColumn(
                name: "PreguntaLibreId",
                table: "Respuestas");

            migrationBuilder.DropColumn(
                name: "Texto",
                table: "Respuestas");

            migrationBuilder.RenameTable(
                name: "Respuestas",
                newName: "Respuesta");

            migrationBuilder.RenameTable(
                name: "Preguntas",
                newName: "Pregunta");

            migrationBuilder.RenameIndex(
                name: "IX_Respuestas_RespuestaOpcionId",
                table: "Respuesta",
                newName: "IX_Respuesta_RespuestaOpcionId");

            migrationBuilder.RenameIndex(
                name: "IX_Respuestas_PreguntaVariadaId",
                table: "Respuesta",
                newName: "IX_Respuesta_PreguntaVariadaId");

            migrationBuilder.RenameIndex(
                name: "IX_Respuestas_PreguntaId",
                table: "Respuesta",
                newName: "IX_Respuesta_PreguntaId");

            migrationBuilder.RenameIndex(
                name: "IX_Respuestas_AlumnoId",
                table: "Respuesta",
                newName: "IX_Respuesta_AlumnoId");

            migrationBuilder.RenameIndex(
                name: "IX_Preguntas_EncuestaGlobalId",
                table: "Pregunta",
                newName: "IX_Pregunta_EncuestaGlobalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Respuesta",
                table: "Respuesta",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pregunta",
                table: "Pregunta",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pregunta_EncuestasGlobales_EncuestaGlobalId",
                table: "Pregunta",
                column: "EncuestaGlobalId",
                principalTable: "EncuestasGlobales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreguntaOpcion_Pregunta_PreguntaId",
                table: "PreguntaOpcion",
                column: "PreguntaId",
                principalTable: "Pregunta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Respuesta_AspNetUsers_AlumnoId",
                table: "Respuesta",
                column: "AlumnoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Respuesta_Pregunta_PreguntaId",
                table: "Respuesta",
                column: "PreguntaId",
                principalTable: "Pregunta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Respuesta_Pregunta_PreguntaVariadaId",
                table: "Respuesta",
                column: "PreguntaVariadaId",
                principalTable: "Pregunta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Respuesta_PreguntaOpcion_RespuestaOpcionId",
                table: "Respuesta",
                column: "RespuestaOpcionId",
                principalTable: "PreguntaOpcion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
