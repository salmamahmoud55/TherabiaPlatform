using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace therabia.Migrations
{
    /// <inheritdoc />
    public partial class Update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionChangeRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfessionalId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
                    TransactionImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionChangeRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionChangeRequests_Professionals_ProfessionalId",
                        column: x => x.ProfessionalId,
                        principalTable: "Professionals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubscriptionChangeRequests_subscriptionplans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "subscriptionplans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionChangeRequests_ProfessionalId",
                table: "SubscriptionChangeRequests",
                column: "ProfessionalId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionChangeRequests_SubscriptionPlanId",
                table: "SubscriptionChangeRequests",
                column: "SubscriptionPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionChangeRequests");
        }
    }
}
