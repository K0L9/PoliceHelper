using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminLTE.Migrations
{
    public partial class AddAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4b934bf-19fd-499b-afe6-35b7de86465b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "44d84efb-52d7-4ee7-a188-32b9ab525472", "fb991d4f-852a-45c1-b848-4d8b5b5255b8", "administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44d84efb-52d7-4ee7-a188-32b9ab525472");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a4b934bf-19fd-499b-afe6-35b7de86465b", "76e95b7d-0708-4302-b61f-4f67e7c99d39", "administrator", "ADMINISTRATOR" });
        }
    }
}
