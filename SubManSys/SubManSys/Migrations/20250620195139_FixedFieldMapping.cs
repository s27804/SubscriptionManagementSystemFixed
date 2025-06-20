using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubManSys.Migrations
{
    /// <inheritdoc />
    public partial class FixedFieldMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubscriptionId",
                table: "Subscriptions",
                newName: "IdSubscription");

            migrationBuilder.RenameColumn(
                name: "SubscriptionId",
                table: "Sales",
                newName: "IdSubscription");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Sales",
                newName: "IdClient");

            migrationBuilder.RenameColumn(
                name: "SaleId",
                table: "Sales",
                newName: "IdSale");

            migrationBuilder.RenameColumn(
                name: "SubscriptionId",
                table: "Payments",
                newName: "IdSubscription");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Payments",
                newName: "IdClient");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "Payments",
                newName: "IdPayment");

            migrationBuilder.RenameColumn(
                name: "SubscriptionId",
                table: "Discsounts",
                newName: "IdSubscription");

            migrationBuilder.RenameColumn(
                name: "DiscountId",
                table: "Discsounts",
                newName: "IdDiscount");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Clients",
                newName: "IdClient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdSubscription",
                table: "Subscriptions",
                newName: "SubscriptionId");

            migrationBuilder.RenameColumn(
                name: "IdSubscription",
                table: "Sales",
                newName: "SubscriptionId");

            migrationBuilder.RenameColumn(
                name: "IdClient",
                table: "Sales",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "IdSale",
                table: "Sales",
                newName: "SaleId");

            migrationBuilder.RenameColumn(
                name: "IdSubscription",
                table: "Payments",
                newName: "SubscriptionId");

            migrationBuilder.RenameColumn(
                name: "IdClient",
                table: "Payments",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "IdPayment",
                table: "Payments",
                newName: "PaymentId");

            migrationBuilder.RenameColumn(
                name: "IdSubscription",
                table: "Discsounts",
                newName: "SubscriptionId");

            migrationBuilder.RenameColumn(
                name: "IdDiscount",
                table: "Discsounts",
                newName: "DiscountId");

            migrationBuilder.RenameColumn(
                name: "IdClient",
                table: "Clients",
                newName: "ClientId");
        }
    }
}
