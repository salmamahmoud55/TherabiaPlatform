using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace therabia.Migrations
{
    /// <inheritdoc />
    public partial class update8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletRequests_Patients_PatientId",
                table: "WalletRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_WalletRequests_Sessions_SessionId",
                table: "WalletRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_WalletRequests_Patients_PatientId",
                table: "WalletRequests",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WalletRequests_Sessions_SessionId",
                table: "WalletRequests",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "SessioId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletRequests_Patients_PatientId",
                table: "WalletRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_WalletRequests_Sessions_SessionId",
                table: "WalletRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_WalletRequests_Patients_PatientId",
                table: "WalletRequests",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_WalletRequests_Sessions_SessionId",
                table: "WalletRequests",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "SessioId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
