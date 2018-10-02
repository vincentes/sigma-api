using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class asdasjddkl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddForeignKey(
                name: "FK_EscritoGrupo_Escritos_EscritoId",
                table: "EscritoGrupo",
                column: "EscritoId",
                principalTable: "Escritos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Escritos_AspNetUsers_DocenteId",
                table: "Escritos",
                column: "DocenteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Escritos_Materias_MateriaId",
                table: "Escritos",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
