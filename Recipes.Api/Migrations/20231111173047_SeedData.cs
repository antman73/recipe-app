using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recipes.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CookTimeMinutes", "Description", "Image", "PrepTimeMinutes", "Servings", "Title" },
                values: new object[,]
                {
                    { 1, 10, "Recipe 1 desc", null, 15, 4, "Recipe 1" },
                    { 2, 20, "Recipe 2 desc", null, 30, 6, "Recipe 2" },
                    { 3, 30, "Recipe 3 desc", null, 45, 8, "Recipe 3" },
                    { 4, 40, "Recipe 4 desc", null, 60, 10, "Recipe 4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
