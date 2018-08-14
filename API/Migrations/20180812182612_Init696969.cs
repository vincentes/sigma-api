using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Init696969 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TareaGrupo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Deadline = table.Column<DateTime>(nullable: false),
                    TareaId = table.Column<int>(nullable: false),
                    GrupoId = table.Column<int>(nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
