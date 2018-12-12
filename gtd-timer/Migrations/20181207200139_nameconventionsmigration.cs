﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace gtdtimer.Migrations
{
    public partial class nameconventionsmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimerTime",
                table: "Timers",
                newName: "Interval");

            migrationBuilder.RenameColumn(
                name: "TimerName",
                table: "Timers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TaskName",
                table: "Tasks",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TaskDescription",
                table: "Tasks",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "PresetName",
                table: "Presets",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "MessageText",
                table: "Messages",
                newName: "Text");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 300, 0, "0d13f334-13e6-4f85-992e-64ec253ff59c", "example33@gmail.com", false, "Alice", "Smith", false, null, null, null, "1234567", null, false, null, false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 301, 0, "f12d3c7c-5960-442c-bb9c-7717253fda15", "example34@gmail.com", false, "Bob", "Johns", false, null, null, null, "54237829", null, false, null, false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 302, 0, "0dadb0f8-312a-492e-8aeb-faff137cd4da", "example35@gmail.com", false, "Sam", "Paul", false, null, null, null, "0978687687", null, false, null, false, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { 300, "0d13f334-13e6-4f85-992e-64ec253ff59c" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { 301, "f12d3c7c-5960-442c-bb9c-7717253fda15" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { 302, "0dadb0f8-312a-492e-8aeb-faff137cd4da" });

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Timers",
                newName: "TimerName");

            migrationBuilder.RenameColumn(
                name: "Interval",
                table: "Timers",
                newName: "TimerTime");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tasks",
                newName: "TaskName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Tasks",
                newName: "TaskDescription");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Presets",
                newName: "PresetName");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Messages",
                newName: "MessageText");
        }
    }
}