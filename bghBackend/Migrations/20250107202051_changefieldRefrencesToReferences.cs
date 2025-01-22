using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bghBackend.Migrations
{
    /// <inheritdoc />
    public partial class changefieldRefrencesToReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0716626a-3e6b-49b8-8933-b7290fa6593c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ed7c113-ea29-4266-8035-36df464f3873");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afbc898a-8276-4d01-b1b2-7138bcf634ac");

            migrationBuilder.RenameColumn(
                name: "Refrences",
                table: "Posts",
                newName: "References");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "References",
                table: "Posts",
                newName: "Refrences");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0716626a-3e6b-49b8-8933-b7290fa6593c", "2ed74102-8bf9-4830-a1e0-5f35ebec180b", "user", "USER" },
                    { "3ed7c113-ea29-4266-8035-36df464f3873", "73dbd65b-89c6-4a6b-a06f-f23d23b2a6df", "admin", "ADMIN" },
                    { "afbc898a-8276-4d01-b1b2-7138bcf634ac", "af616b9e-4262-4c6f-aa1a-bb08abb77dee", "member", "MEMBER" }
                });
        }
    }
}
