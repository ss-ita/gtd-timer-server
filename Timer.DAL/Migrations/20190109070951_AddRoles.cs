using Microsoft.EntityFrameworkCore.Migrations;

namespace Timer.DAL.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 300,
                column: "ConcurrencyStamp",
                value: "c5397efe-ae49-4655-bbb0-8957cc635399");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 301,
                column: "ConcurrencyStamp",
                value: "20be3687-ccdd-4193-9d8b-04058ebc2710");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 302,
                column: "ConcurrencyStamp",
                value: "efeeba61-d378-4d42-a85f-f58ef56bd409");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
