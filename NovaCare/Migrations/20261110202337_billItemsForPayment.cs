using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class billItemsForPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientPaymentModes_Payments_PaymentId",
                table: "PatientPaymentModes");

            migrationBuilder.DropIndex(
                name: "IX_PatientPaymentModes_PaymentId",
                table: "PatientPaymentModes");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "PatientPaymentModes");

            migrationBuilder.AddColumn<int>(
                name: "PaymentModeId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BillItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Itemname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    cost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillItems_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentModeId",
                table: "Payments",
                column: "PaymentModeId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_BillId",
                table: "BillItems",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentModes_PaymentModeId",
                table: "Payments",
                column: "PaymentModeId",
                principalTable: "PaymentModes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentModes_PaymentModeId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "BillItems");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PaymentModeId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentModeId",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "PatientPaymentModes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientPaymentModes_PaymentId",
                table: "PatientPaymentModes",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientPaymentModes_Payments_PaymentId",
                table: "PatientPaymentModes",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }
    }
}
