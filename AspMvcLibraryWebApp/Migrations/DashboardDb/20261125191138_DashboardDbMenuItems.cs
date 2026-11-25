using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspMvcLibraryWebApp.Migrations.DashboardDb
{
    /// <inheritdoc />
    public partial class DashboardDbMenuItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MainMenuItems",
                columns: new[] { "Id", "Controller", "Title" },
                values: new object[,]
                {
                    { 1, "Genre", "Genres" },
                    { 2, "Publisher", "Publishers" },
                    { 3, "Author", "Authors" },
                    { 4, "Book", "Books" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MainMenuItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MainMenuItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MainMenuItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MainMenuItems",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
