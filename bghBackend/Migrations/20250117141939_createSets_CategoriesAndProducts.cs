using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bghBackend.Migrations
{
    /// <inheritdoc />
    public partial class createSets_CategoriesAndProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e711a8d-c900-4c9e-bb76-3c4e9748dced");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce733e66-618d-42fc-93bb-3ef7a341d3c7");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e964479-5ce7-4bc7-81f5-4bdf491f94da", "c002b556-9ac5-45db-9417-213be2764b93", "support", "SUPPORT" },
                    { "bb5e0a7e-f371-449a-be1f-530c2392a527", "86e09e10-6acb-4ebc-b83f-809a5752997c", "admin", "ADMIN" },
                    { "cbb29afc-44a8-4c9a-8867-13046def80f0", "8205a5fd-cf06-435e-8c73-981b39edbe16", "member", "MEMBER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e964479-5ce7-4bc7-81f5-4bdf491f94da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb5e0a7e-f371-449a-be1f-530c2392a527");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cbb29afc-44a8-4c9a-8867-13046def80f0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6e711a8d-c900-4c9e-bb76-3c4e9748dced", "cfbee4a6-b707-4a92-a3c2-5e8fcf3cfb5e", "admin", "ADMIN" },
                    { "ce733e66-618d-42fc-93bb-3ef7a341d3c7", "6eeee441-189b-4d1e-9f41-5a818ba16395", "member", "MEMBER" }
                });
        }
    }
}
