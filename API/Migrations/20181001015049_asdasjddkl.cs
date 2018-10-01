using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class asdasjddkl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EscritoGrupo_Events_EscritoId",
                table: "EscritoGrupo");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_DocenteId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Materias_MateriaId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Events_EventoId",
                table: "Tareas");

            migrationBuilder.DropIndex(
                name: "IX_Tareas_EventoId",
                table: "Tareas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventoId",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Escritos");

            migrationBuilder.RenameIndex(
                name: "IX_Events_MateriaId",
                table: "Escritos",
                newName: "IX_Escritos_MateriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_DocenteId",
                table: "Escritos",
                newName: "IX_Escritos_DocenteId");

            migrationBuilder.AlterColumn<int>(
                name: "MateriaId",
                table: "Escritos",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Escritos",
                table: "Escritos",
                column: "Id");

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
            migrationBuilder.DropForeignKey(
                name: "FK_EscritoGrupo_Escritos_EscritoId",
                table: "EscritoGrupo");

            migrationBuilder.DropForeignKey(
                name: "FK_Escritos_AspNetUsers_DocenteId",
                table: "Escritos");

            migrationBuilder.DropForeignKey(
                name: "FK_Escritos_Materias_MateriaId",
                table: "Escritos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Escritos",
                table: "Escritos");

            migrationBuilder.RenameTable(
                name: "Escritos",
                newName: "Events");

            migrationBuilder.RenameIndex(
                name: "IX_Escritos_MateriaId",
                table: "Events",
                newName: "IX_Events_MateriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Escritos_DocenteId",
                table: "Events",
                newName: "IX_Events_DocenteId");

            migrationBuilder.AddColumn<int>(
                name: "EventoId",
                table: "Tareas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MateriaId",
                table: "Events",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Events",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_EventoId",
                table: "Tareas",
                column: "EventoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EscritoGrupo_Events_EscritoId",
                table: "EscritoGrupo",
                column: "EscritoId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_DocenteId",
                table: "Events",
                column: "DocenteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Materias_MateriaId",
                table: "Events",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Events_EventoId",
                table: "Tareas",
                column: "EventoId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
