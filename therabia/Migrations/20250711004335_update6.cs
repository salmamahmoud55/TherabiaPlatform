using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace therabia.Migrations
{
    /// <inheritdoc />
    public partial class update6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SessionDate",
                table: "Professionalrequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Professionalrequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professionalrequests_SessionId",
                table: "Professionalrequests",
                column: "SessionId",
                unique: true,
                filter: "[SessionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Professionalrequests_Sessions_SessionId",
                table: "Professionalrequests",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "SessioId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professionalrequests_Sessions_SessionId",
                table: "Professionalrequests");

            migrationBuilder.DropIndex(
                name: "IX_Professionalrequests_SessionId",
                table: "Professionalrequests");

            migrationBuilder.DropColumn(
                name: "SessionDate",
                table: "Professionalrequests");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Professionalrequests");
        }
    }
}
