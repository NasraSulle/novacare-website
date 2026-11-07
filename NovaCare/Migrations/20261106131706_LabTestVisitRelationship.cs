using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class LabTestVisitRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabTests_Patients_PatientId",
                table: "LabTests");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTests_Visits_VisitId",
                table: "LabTests");

            migrationBuilder.DropIndex(
                name: "IX_LabTests_PatientId",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "LabTests");

            migrationBuilder.AlterColumn<int>(
                name: "VisitId",
                table: "LabTests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LabTests_Visits_VisitId",
                table: "LabTests",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabTests_Visits_VisitId",
                table: "LabTests");

            migrationBuilder.AlterColumn<int>(
                name: "VisitId",
                table: "LabTests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "LabTests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_PatientId",
                table: "LabTests",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTests_Patients_PatientId",
                table: "LabTests",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LabTests_Visits_VisitId",
                table: "LabTests",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id");
        }
    }
}
