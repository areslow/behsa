using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bghBackend.Migrations
{
    /// <inheritdoc />
    public partial class addRolesToRolesTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6e711a8d-c900-4c9e-bb76-3c4e9748dced", "cfbee4a6-b707-4a92-a3c2-5e8fcf3cfb5e", "admin", "ADMIN" },
                    { "ce733e66-618d-42fc-93bb-3ef7a341d3c7", "6eeee441-189b-4d1e-9f41-5a818ba16395", "member", "MEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e711a8d-c900-4c9e-bb76-3c4e9748dced");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce733e66-618d-42fc-93bb-3ef7a341d3c7");
        }
    }
}
