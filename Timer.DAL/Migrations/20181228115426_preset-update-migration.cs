﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Timer.DAL.Migrations
{
    public partial class presetupdatemigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 300,
                column: "ConcurrencyStamp",
                value: "8d771c10-2814-4948-9888-60c3cd1ca129");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 301,
                column: "ConcurrencyStamp",
                value: "ac61ae6c-5487-432a-8bfb-fd5788ae2452");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 302,
                column: "ConcurrencyStamp",
                value: "941686de-a2d1-482c-8851-af1f9c0bcb3a");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 300,
                column: "ConcurrencyStamp",
                value: "0d13f334-13e6-4f85-992e-64ec253ff59c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 301,
                column: "ConcurrencyStamp",
                value: "f12d3c7c-5960-442c-bb9c-7717253fda15");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 302,
                column: "ConcurrencyStamp",
                value: "0dadb0f8-312a-492e-8aeb-faff137cd4da");
        }
    }
}