using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class asopd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventoGrupo_Grupos_GrupoId1",
                table: "EventoGrupo");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_Tarea_DocenteId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Materias_Tarea_MateriaId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_TareaImagen_Events_TareaId",
                table: "TareaImagen");

            migrationBuilder.DropTable(
                name: "Respuesta");

            migrationBuilder.DropIndex(
                name: "IX_Events_Tarea_DocenteId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_Tarea_MateriaId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_EventoGrupo_GrupoId1",
                table: "EventoGrupo");

            migrationBuilder.DropColumn(
                name: "Contenido",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Tarea_DocenteId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Tarea_MateriaId",
                table: "Events");

            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DocenteId = table.Column<string>(nullable: true),
                    MateriaId = table.Column<int>(nullable: false),
                    Contenido = table.Column<string>(nullable: true),
                    EventoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tareas_AspNetUsers_DocenteId",
                        column: x => x.DocenteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tareas_Events_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tareas_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TareaGrupo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    TareaId = table.Column<int>(nullable: false),
                    GrupoId = table.Column<int>(nullable: false),
                    Notified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareaGrupo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TareaGrupo_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TareaGrupo_Tareas_TareaId",
                        column: x => x.TareaId,
                        principalTable: "Tareas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TareaGrupo_GrupoId",
                table: "TareaGrupo",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_TareaGrupo_TareaId",
                table: "TareaGrupo",
                column: "TareaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_DocenteId",
                table: "Tareas",
                column: "DocenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_EventoId",
                table: "Tareas",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_MateriaId",
                table: "Tareas",
                column: "MateriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TareaImagen_Tareas_TareaId",
                table: "TareaImagen",
                column: "TareaId",
                principalTable: "Tareas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TareaImagen_Tareas_TareaId",
                table: "TareaImagen");

            migrationBuilder.DropTable(
                name: "TareaGrupo");

            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.AddColumn<string>(
                name: "Contenido",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tarea_DocenteId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tarea_MateriaId",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Respuesta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AlumnoId = table.Column<string>(nullable: true),
                    PreguntaId = table.Column<int>(nullable: true),
                    Valor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuesta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Respuesta_AspNetUsers_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Respuesta_Pregunta_PreguntaId",
                        column: x => x.PreguntaId,
                        principalTable: "Pregunta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_Tarea_DocenteId",
                table: "Events",
                column: "Tarea_DocenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Tarea_MateriaId",
                table: "Events",
                column: "Tarea_MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_EventoGrupo_GrupoId1",
                table: "EventoGrupo",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Respuesta_AlumnoId",
                table: "Respuesta",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Respuesta_PreguntaId",
                table: "Respuesta",
                column: "PreguntaId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventoGrupo_Grupos_GrupoId1",
                table: "EventoGrupo",
                column: "GrupoId",
                principalTable: "Grupos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_Tarea_DocenteId",
                table: "Events",
                column: "Tarea_DocenteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Materias_Tarea_MateriaId",
                table: "Events",
                column: "Tarea_MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TareaImagen_Events_TareaId",
                table: "TareaImagen",
                column: "TareaId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
