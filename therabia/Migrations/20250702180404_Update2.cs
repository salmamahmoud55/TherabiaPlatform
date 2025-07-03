using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace therabia.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Professionalrequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Professionalrequests",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ProfessionalId",
                table: "Professionalrequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionImage",
                table: "Professionalrequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Professionalrequests_ProfessionalId",
                table: "Professionalrequests",
                column: "ProfessionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Professionalrequests_Professionals_ProfessionalId",
                table: "Professionalrequests",
                column: "ProfessionalId",
                principalTable: "Professionals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professionalrequests_Professionals_ProfessionalId",
                table: "Professionalrequests");

            migrationBuilder.DropIndex(
                name: "IX_Professionalrequests_ProfessionalId",
                table: "Professionalrequests");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Professionalrequests");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Professionalrequests");

            migrationBuilder.DropColumn(
                name: "ProfessionalId",
                table: "Professionalrequests");

            migrationBuilder.DropColumn(
                name: "TransactionImage",
                table: "Professionalrequests");
        }
    }
}
