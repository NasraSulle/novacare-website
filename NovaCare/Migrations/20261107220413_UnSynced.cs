using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class UnSynced : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MediicineTypeId",
                table: "Services",
                newName: "MedicineTypeId");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Consultations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_DepartmentId",
                table: "Services",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_DoctorId",
                table: "Services",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_ServiceId",
                table: "Consultations",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Services_ServiceId",
                table: "Consultations",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_DoctorTypes_DepartmentId",
                table: "Services",
                column: "DepartmentId",
                principalTable: "DoctorTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Doctors_DoctorId",
                table: "Services",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Services_ServiceId",
                table: "Consultations");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_DoctorTypes_DepartmentId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Doctors_DoctorId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_DepartmentId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_DoctorId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Consultations_ServiceId",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Consultations");

            migrationBuilder.RenameColumn(
                name: "MedicineTypeId",
                table: "Services",
                newName: "MediicineTypeId");
        }
    }
}
