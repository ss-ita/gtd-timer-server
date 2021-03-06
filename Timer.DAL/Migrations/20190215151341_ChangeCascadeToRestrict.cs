﻿// <auto-generated />

using Microsoft.EntityFrameworkCore.Migrations;

namespace GtdTimerDAL.Migrations
{
    public partial class ChangeCascadeToRestrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "Records",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Records",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Records_TaskId",
                table: "Records",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_UserId",
                table: "Records",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Tasks_TaskId",
                table: "Records",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_AspNetUsers_UserId",
                table: "Records",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Tasks_TaskId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_AspNetUsers_UserId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_TaskId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_UserId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Records");
        }
    }
}
