﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Timer.DAL.Migrations
{
    public partial class timermigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Goal",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "PresetBigBreakTimeTime",
                table: "Presets");

            migrationBuilder.DropColumn(
                name: "PresetSmalBreakTime",
                table: "Presets");

            migrationBuilder.DropColumn(
                name: "PresetWorkTime",
                table: "Presets");

            migrationBuilder.AddColumn<string>(
                name: "TaskDescription",
                table: "Tasks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Timers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimerName = table.Column<string>(nullable: true),
                    TimerTime = table.Column<TimeSpan>(nullable: false),
                    PresetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timers_Presets_PresetId",
                        column: x => x.PresetId,
                        principalTable: "Presets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Timers_PresetId",
                table: "Timers",
                column: "PresetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Timers");

            migrationBuilder.DropColumn(
                name: "TaskDescription",
                table: "Tasks");

            migrationBuilder.AddColumn<DateTime>(
                name: "Goal",
                table: "Tasks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "PresetBigBreakTimeTime",
                table: "Presets",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "PresetSmalBreakTime",
                table: "Presets",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "PresetWorkTime",
                table: "Presets",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}