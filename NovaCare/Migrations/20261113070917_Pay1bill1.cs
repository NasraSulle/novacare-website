using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class Pay1bill1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bills_BillId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_BillId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Payments");

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "Bills",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "Bills",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "PaymentStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "PaymentStatusName",
                value: "Paid");

            migrationBuilder.UpdateData(
                table: "PaymentStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "PaymentStatusName",
                value: "Insurance Review");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PaymentId",
                table: "Bills",
                column: "PaymentId",
                unique: true,
                filter: "[PaymentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Payments_PaymentId",
                table: "Bills",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Payments_PaymentId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_PaymentId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Bills");

            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "PaymentStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "PaymentStatusName",
                value: "Partial Payment");

            migrationBuilder.UpdateData(
                table: "PaymentStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "PaymentStatusName",
                value: "Full Payment");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BillId",
                table: "Payments",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bills_BillId",
                table: "Payments",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
