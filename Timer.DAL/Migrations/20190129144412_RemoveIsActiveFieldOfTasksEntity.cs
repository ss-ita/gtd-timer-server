using Microsoft.EntityFrameworkCore.Migrations;

namespace GtdTimerDAL.Migrations
{
    public partial class RemoveIsActiveFieldOfTasksEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "IsTurnOn",
                table: "Alarms",
                newName: "SoundOn");

            migrationBuilder.RenameColumn(
                name: "IsSound",
                table: "Alarms",
                newName: "IsOn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SoundOn",
                table: "Alarms",
                newName: "IsTurnOn");

            migrationBuilder.RenameColumn(
                name: "IsOn",
                table: "Alarms",
                newName: "IsSound");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Tasks",
                nullable: false,
                defaultValue: false);
        }
    }
}
