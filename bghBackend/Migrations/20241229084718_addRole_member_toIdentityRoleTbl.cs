using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bghBackend.Migrations
{
    /// <inheritdoc />
    public partial class addRole_member_toIdentityRoleTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5740ddd3-f2d1-4d52-b588-f4c87da33855");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cce92d0b-bc86-45b1-aefc-c03e6affe618");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "db997e4c-6797-4317-a742-a1fe0f71a0e9", "32470c95-f507-41ed-8b6d-7efa54072074", "member", "MEMBER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "5740ddd3-f2d1-4d52-b588-f4c87da33855", "2a156d2f-8d2d-4027-beba-8a33c8a9bdd6", "user", "USER" },
                    { "cce92d0b-bc86-45b1-aefc-c03e6affe618", "66f5e229-3b30-425f-a967-020b1351edf9", "admin", "ADMIN" }
                });
        }
    }
}
