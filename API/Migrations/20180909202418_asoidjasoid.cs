using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class asoidjasoid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdscriptoId",
                table: "EncuestasGlobales",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EncuestasGlobales_AdscriptoId",
                table: "EncuestasGlobales",
                column: "AdscriptoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EncuestasGlobales_AspNetUsers_AdscriptoId",
                table: "EncuestasGlobales",
                column: "AdscriptoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EncuestasGlobales_AspNetUsers_AdscriptoId",
                table: "EncuestasGlobales");

            migrationBuilder.DropIndex(
                name: "IX_EncuestasGlobales_AdscriptoId",
                table: "EncuestasGlobales");

            migrationBuilder.DropColumn(
                name: "AdscriptoId",
                table: "EncuestasGlobales");
        }
    }
}
