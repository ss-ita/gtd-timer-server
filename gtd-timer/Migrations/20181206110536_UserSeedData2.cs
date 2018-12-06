﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace gtdtimer.Migrations
{
    public partial class UserSeedData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { 45, "b55b5ea3-da91-4fd0-94aa-cf9ef308c4e4" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 77, 0, "da7b23a8-1978-4caf-8493-23c4ec617063", "example@gmail.com", false, "Alice", "Smith", false, null, null, null, "1234567", null, false, null, false, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { 77, "da7b23a8-1978-4caf-8493-23c4ec617063" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 45, 0, "b55b5ea3-da91-4fd0-94aa-cf9ef308c4e4", "example@gmail.com", false, "Eric", "100 Main St", false, null, null, null, "1234567", null, false, null, false, null });
        }
    }
}