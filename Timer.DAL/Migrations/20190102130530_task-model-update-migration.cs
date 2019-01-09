using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Timer.DAL.Migrations
{
    public partial class taskmodelupdatemigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishTime",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Tasks",
                newName: "LastStartTime");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ElapsedTime",
                table: "Tasks",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Goal",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRunning",
                table: "Tasks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 300,
                column: "ConcurrencyStamp",
                value: "3848dd1c-02bb-48c8-ad67-b78d351ac472");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 301,
                column: "ConcurrencyStamp",
                value: "a4276f8c-f5e9-415e-9eb6-6f8eec22079e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 302,
                column: "ConcurrencyStamp",
                value: "e6970037-744e-449f-aa22-b288279230b2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElapsedTime",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Goal",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IsRunning",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "LastStartTime",
                table: "Tasks",
                newName: "StartTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishTime",
                table: "Tasks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "Tasks",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 300,
                column: "ConcurrencyStamp",
                value: "d5165ab8-046d-4b2e-8b81-6878cc3dbbe7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 301,
                column: "ConcurrencyStamp",
                value: "5ce548ee-94f2-4677-88cb-7ae7b52dda2f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 302,
                column: "ConcurrencyStamp",
                value: "22b914d8-5070-40a3-b808-62813cb1d74f");
        }
    }
}
