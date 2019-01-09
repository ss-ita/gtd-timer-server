﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Timer.DAL.Migrations
{
    public partial class changepresetmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 300,
                column: "ConcurrencyStamp",
                value: "74d9c2a5-ac6a-4bfb-bad0-9d6ae74e731a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 301,
                column: "ConcurrencyStamp",
                value: "f3d804e3-3d3a-4e72-a030-597ea5999ce2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 302,
                column: "ConcurrencyStamp",
                value: "5b5c22eb-2a2f-4955-bb59-773e0770df63");
        }
    }
}