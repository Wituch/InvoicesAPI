using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoicesAPI.Migrations
{
    /// <inheritdoc />
    public partial class InvoicesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_BuyerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_RecipientId",
                table: "Invoices");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_BuyerId",
                table: "Invoices",
                column: "BuyerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_RecipientId",
                table: "Invoices",
                column: "RecipientId",
                principalTable: "Customers",
                principalColumn: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_BuyerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_RecipientId",
                table: "Invoices");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_BuyerId",
                table: "Invoices",
                column: "BuyerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_RecipientId",
                table: "Invoices",
                column: "RecipientId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
