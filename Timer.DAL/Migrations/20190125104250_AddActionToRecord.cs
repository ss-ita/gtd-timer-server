using Microsoft.EntityFrameworkCore.Migrations;

namespace GtdTimerDAL.Migrations
{
    public partial class AddActionToRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "Records",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "Records");
        }
    }
}
