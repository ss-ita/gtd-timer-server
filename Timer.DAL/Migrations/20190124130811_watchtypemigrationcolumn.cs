using Microsoft.EntityFrameworkCore.Migrations;

namespace GtdTimerDAL.Migrations
{
    public partial class watchtypemigrationcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "migration",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "WatchType",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WatchType",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "migration",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);
        }
    }
}
