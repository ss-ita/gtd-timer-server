using Microsoft.EntityFrameworkCore.Migrations;

namespace gtdtimer.Migrations
{
    public partial class TestUserDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "ConcurrencyStamp", "Email" },
                values: new object[] { "6607f09c-2b94-43ea-a791-447c14f03945", "example1@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 41, 0, "6b585269-e807-4566-af30-7408870b99df", "example2@gmail.com", false, "Bob", "Johns", false, null, null, null, "54237829", null, false, null, false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 31, 0, "ed5809a5-ebfa-425d-9268-985086848f36", "example3@gmail.com", false, "Sam", "Paul", false, null, null, null, "0978687687", null, false, null, false, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { 31, "ed5809a5-ebfa-425d-9268-985086848f36" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { 41, "6b585269-e807-4566-af30-7408870b99df" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "ConcurrencyStamp", "Email" },
                values: new object[] { "da7b23a8-1978-4caf-8493-23c4ec617063", "example@gmail.com" });
        }
    }
}
