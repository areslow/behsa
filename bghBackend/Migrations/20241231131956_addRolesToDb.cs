using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bghBackend.Migrations
{
    /// <inheritdoc />
    public partial class addRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5913ceb3-ed7f-4d9c-aefd-c7968bcd9019");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e8c1483-4c01-44a3-972d-71b266a7ac88");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c477d09-d69a-4e5d-9d4f-4bf9002592b0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5171383c-c46d-4796-9f33-4faab6dc828e", "dc358ba4-9dde-4666-9e12-8bf0225375cd", "admin", "ADMIN" },
                    { "89770b84-54a5-414d-93ef-1d40e7a4713a", "fba90a5d-11af-4473-a8e1-1a2f92382e7c", "member", "MEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5171383c-c46d-4796-9f33-4faab6dc828e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89770b84-54a5-414d-93ef-1d40e7a4713a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5913ceb3-ed7f-4d9c-aefd-c7968bcd9019", "ee007af4-a2d7-4778-af07-2f65ac0b322c", "member", "MEMBER" },
                    { "7e8c1483-4c01-44a3-972d-71b266a7ac88", "cba7b79a-2916-420e-bfe5-1d36f09829aa", "user", "USER" },
                    { "8c477d09-d69a-4e5d-9d4f-4bf9002592b0", "68da8912-8aab-4ac5-9929-724f488f9d10", "admin", "ADMIN" }
                });
        }
    }
}
