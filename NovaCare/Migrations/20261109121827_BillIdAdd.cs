using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class BillIdAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VisitStatusId",
                table: "Visits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "Treatments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "Medicines",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "LabTests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "Consultations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VisitStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitStatus", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "VisitStatus",
                columns: new[] { "Id", "VisitStatusName" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Completed" },
                    { 3, "Cancelled" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visits_VisitStatusId",
                table: "Visits",
                column: "VisitStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_BillId",
                table: "Treatments",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_BillId",
                table: "Medicines",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_BillId",
                table: "LabTests",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_BillId",
                table: "Consultations",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Bills_BillId",
                table: "Consultations",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTests_Bills_BillId",
                table: "LabTests",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Bills_BillId",
                table: "Medicines",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Bills_BillId",
                table: "Treatments",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_VisitStatus_VisitStatusId",
                table: "Visits",
                column: "VisitStatusId",
                principalTable: "VisitStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Bills_BillId",
                table: "Consultations");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTests_Bills_BillId",
                table: "LabTests");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Bills_BillId",
                table: "Medicines");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Bills_BillId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_VisitStatus_VisitStatusId",
                table: "Visits");

            migrationBuilder.DropTable(
                name: "VisitStatus");

            migrationBuilder.DropIndex(
                name: "IX_Visits_VisitStatusId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_BillId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_BillId",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_LabTests_BillId",
                table: "LabTests");

            migrationBuilder.DropIndex(
                name: "IX_Consultations_BillId",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "VisitStatusId",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Consultations");
        }
    }
}
