using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace therabia.Migrations
{
    /// <inheritdoc />
    public partial class Update9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Verificationtokens",
                newName: "ExpiryDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "Verificationtokens",
                newName: "CreatedAt");
        }
    }
}
