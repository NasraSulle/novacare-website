using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class MoveTYStToVisit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_PatientStatuses_PatientStatusId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_PatientTypes_PatientTypeId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_PatientStatusId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_PatientTypeId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientStatusId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientTypeId",
                table: "Patients");

            migrationBuilder.AddColumn<int>(
                name: "PatientStatusId",
                table: "Visits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientTypeId",
                table: "Visits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Visits_PatientStatusId",
                table: "Visits",
                column: "PatientStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_PatientTypeId",
                table: "Visits",
                column: "PatientTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_PatientStatuses_PatientStatusId",
                table: "Visits",
                column: "PatientStatusId",
                principalTable: "PatientStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_PatientTypes_PatientTypeId",
                table: "Visits",
                column: "PatientTypeId",
                principalTable: "PatientTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_PatientStatuses_PatientStatusId",
                table: "Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_PatientTypes_PatientTypeId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_PatientStatusId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_PatientTypeId",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "PatientStatusId",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "PatientTypeId",
                table: "Visits");

            migrationBuilder.AddColumn<int>(
                name: "PatientStatusId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientTypeId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientStatusId",
                table: "Patients",
                column: "PatientStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientTypeId",
                table: "Patients",
                column: "PatientTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_PatientStatuses_PatientStatusId",
                table: "Patients",
                column: "PatientStatusId",
                principalTable: "PatientStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_PatientTypes_PatientTypeId",
                table: "Patients",
                column: "PatientTypeId",
                principalTable: "PatientTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
