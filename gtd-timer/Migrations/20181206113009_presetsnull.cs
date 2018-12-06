﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace gtdtimer.Migrations
{
    public partial class presetsnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presets_AspNetUsers_UserId",
                table: "Presets");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Presets",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Presets_AspNetUsers_UserId",
                table: "Presets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presets_AspNetUsers_UserId",
                table: "Presets");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Presets",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Presets_AspNetUsers_UserId",
                table: "Presets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
