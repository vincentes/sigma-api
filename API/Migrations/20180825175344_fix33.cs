using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class fix33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventoGrupo_Events_EscritoId",
                table: "EventoGrupo");

            migrationBuilder.DropForeignKey(
                name: "FK_EventoGrupo_Events_ParcialId",
                table: "EventoGrupo");

            migrationBuilder.DropForeignKey(
                name: "FK_EventoGrupo_Events_TareaId",
                table: "EventoGrupo");

            migrationBuilder.DropTable(
                name: "EventNotifications");

            migrationBuilder.DropIndex(
                name: "IX_EventoGrupo_EscritoId",
                table: "EventoGrupo");

            migrationBuilder.DropIndex(
                name: "IX_EventoGrupo_ParcialId",
                table: "EventoGrupo");

            migrationBuilder.DropIndex(
                name: "IX_EventoGrupo_TareaId",
                table: "EventoGrupo");

            migrationBuilder.DropColumn(
                name: "EscritoId",
                table: "EventoGrupo");

            migrationBuilder.DropColumn(
                name: "ParcialId",
                table: "EventoGrupo");

            migrationBuilder.DropColumn(
                name: "TareaId",
                table: "EventoGrupo");

            migrationBuilder.AddColumn<bool>(
                name: "Notified",
                table: "EventoGrupo",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
