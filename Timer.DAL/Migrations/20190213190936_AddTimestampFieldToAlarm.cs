using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GtdTimerDAL.Migrations
{
    public partial class AddTimestampFieldToAlarm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Alarms",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Alarms");
        }
    }
}
