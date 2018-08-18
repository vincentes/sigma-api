using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class _39393713 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Escritos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DocenteId = table.Column<string>(nullable: true),
                    MateriaId = table.Column<int>(nullable: false),
                    Temas = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Escritos_AspNetUsers_DocenteId",
                        column: x => x.DocenteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Escritos_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EscritoGrupo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    EscritoId = table.Column<int>(nullable: false),
                    GrupoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscritoGrupo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EscritoGrupo_Escritos_EscritoId",
                        column: x => x.EscritoId,
                        principalTable: "Escritos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EscritoGrupo_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EscritoGrupo_EscritoId",
                table: "EscritoGrupo",
                column: "EscritoId");

            migrationBuilder.CreateIndex(
                name: "IX_EscritoGrupo_GrupoId",
                table: "EscritoGrupo",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Escritos_DocenteId",
                table: "Escritos",
                column: "DocenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Escritos_MateriaId",
                table: "Escritos",
                column: "MateriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EscritoGrupo");

            migrationBuilder.DropTable(
                name: "Escritos");
        }
    }
}
