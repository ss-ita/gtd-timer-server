using Microsoft.EntityFrameworkCore.Migrations;

namespace Timer.DAL.Migrations
{
    public partial class timercontextchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
