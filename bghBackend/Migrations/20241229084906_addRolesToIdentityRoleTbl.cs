using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bghBackend.Migrations
{
    /// <inheritdoc />
    public partial class addRolesToIdentityRoleTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "db997e4c-6797-4317-a742-a1fe0f71a0e9");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "db997e4c-6797-4317-a742-a1fe0f71a0e9", "32470c95-f507-41ed-8b6d-7efa54072074", "member", "MEMBER" });
        }
    }
}
