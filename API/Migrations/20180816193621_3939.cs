using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class _3939 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parciales",
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
                    table.PrimaryKey("PK_Parciales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parciales_AspNetUsers_DocenteId",
                        column: x => x.DocenteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parciales_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParcialGrupo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    ParcialId = table.Column<int>(nullable: false),
                    GrupoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcialGrupo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParcialGrupo_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParcialGrupo_Parciales_ParcialId",
                        column: x => x.ParcialId,
                        principalTable: "Parciales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parciales_DocenteId",
                table: "Parciales",
                column: "DocenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Parciales_MateriaId",
                table: "Parciales",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ParcialGrupo_GrupoId",
                table: "ParcialGrupo",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_ParcialGrupo_ParcialId",
                table: "ParcialGrupo",
                column: "ParcialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
