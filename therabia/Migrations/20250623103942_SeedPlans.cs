using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace therabia.Migrations
{
    /// <inheritdoc />
    public partial class SeedPlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "subscriptionplans",
                columns: new[] { "Id", "MaxPatients", "Price", "Type" },
                values: new object[,]
                {
                    { 1, 20, 0m, 0 },
                    { 2, 50, 300m, 3 },
                    { 3, 150, 500m, 1 },
                    { 4, 300, 800m, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscriptionplans",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "subscriptionplans",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "subscriptionplans",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "subscriptionplans",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
