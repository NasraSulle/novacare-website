using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class NewPatientTreatmentMappers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bed_Ward_WardId",
                table: "Bed");

            migrationBuilder.DropForeignKey(
                name: "FK_Surgery_MedicalDepartments_MedicalDepartmentId",
                table: "Surgery");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Surgery_SurgeryId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Ward_WardId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Ward_MedicalDepartments_MedicalDepartmentId",
                table: "Ward");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ward",
                table: "Ward");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Surgery",
                table: "Surgery");

            migrationBuilder.DropColumn(
                name: "MedicineType",
                table: "Medicines");

            migrationBuilder.RenameTable(
                name: "Ward",
                newName: "Wards");

            migrationBuilder.RenameTable(
                name: "Surgery",
                newName: "Surgeries");

            migrationBuilder.RenameIndex(
                name: "IX_Ward_MedicalDepartmentId",
                table: "Wards",
                newName: "IX_Wards_MedicalDepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Surgery_MedicalDepartmentId",
                table: "Surgeries",
                newName: "IX_Surgeries_MedicalDepartmentId");

            migrationBuilder.AlterColumn<int>(
                name: "TreatmentQty",
                table: "Treatments",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Treatments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "MedicineGenreId",
                table: "MedicineTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicineTypeId",
                table: "Medicines",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MedicalDepartmentId",
                table: "Wards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wards",
                table: "Wards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Surgeries",
                table: "Surgeries",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "MedicineTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "MedicineGenreId",
                value: null);

            migrationBuilder.UpdateData(
                table: "MedicineTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "MedicineGenreId",
                value: null);

            migrationBuilder.UpdateData(
                table: "MedicineTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "MedicineGenreId",
                value: null);

            migrationBuilder.UpdateData(
                table: "MedicineTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "MedicineGenreId",
                value: null);

            migrationBuilder.UpdateData(
                table: "MedicineTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "MedicineGenreId",
                value: null);

            migrationBuilder.UpdateData(
                table: "MedicineTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "MedicineGenreId",
                value: null);

            migrationBuilder.UpdateData(
                table: "TreatmentStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "TreatmentStatusName",
                value: "Requested");

            migrationBuilder.UpdateData(
                table: "TreatmentStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "TreatmentStatusName",
                value: "OnGoing");

            migrationBuilder.InsertData(
                table: "TreatmentStatuses",
                columns: new[] { "Id", "TreatmentStatusName" },
                values: new object[] { 3, "Completed" });

            migrationBuilder.CreateIndex(
                name: "IX_MedicineTypes_MedicineGenreId",
                table: "MedicineTypes",
                column: "MedicineGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_MedicineTypeId",
                table: "Medicines",
                column: "MedicineTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bed_Wards_WardId",
                table: "Bed",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_MedicineTypes_MedicineTypeId",
                table: "Medicines",
                column: "MedicineTypeId",
                principalTable: "MedicineTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineTypes_MedicineGenres_MedicineGenreId",
                table: "MedicineTypes",
                column: "MedicineGenreId",
                principalTable: "MedicineGenres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Surgeries_MedicalDepartments_MedicalDepartmentId",
                table: "Surgeries",
                column: "MedicalDepartmentId",
                principalTable: "MedicalDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Surgeries_SurgeryId",
                table: "Treatments",
                column: "SurgeryId",
                principalTable: "Surgeries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Wards_WardId",
                table: "Treatments",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wards_MedicalDepartments_MedicalDepartmentId",
                table: "Wards",
                column: "MedicalDepartmentId",
                principalTable: "MedicalDepartments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bed_Wards_WardId",
                table: "Bed");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_MedicineTypes_MedicineTypeId",
                table: "Medicines");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineTypes_MedicineGenres_MedicineGenreId",
                table: "MedicineTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Surgeries_MedicalDepartments_MedicalDepartmentId",
                table: "Surgeries");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Surgeries_SurgeryId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Wards_WardId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Wards_MedicalDepartments_MedicalDepartmentId",
                table: "Wards");

            migrationBuilder.DropIndex(
                name: "IX_MedicineTypes_MedicineGenreId",
                table: "MedicineTypes");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_MedicineTypeId",
                table: "Medicines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wards",
                table: "Wards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Surgeries",
                table: "Surgeries");

            migrationBuilder.DeleteData(
                table: "TreatmentStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "MedicineGenreId",
                table: "MedicineTypes");

            migrationBuilder.DropColumn(
                name: "MedicineTypeId",
                table: "Medicines");

            migrationBuilder.RenameTable(
                name: "Wards",
                newName: "Ward");

            migrationBuilder.RenameTable(
                name: "Surgeries",
                newName: "Surgery");

            migrationBuilder.RenameIndex(
                name: "IX_Wards_MedicalDepartmentId",
                table: "Ward",
                newName: "IX_Ward_MedicalDepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Surgeries_MedicalDepartmentId",
                table: "Surgery",
                newName: "IX_Surgery_MedicalDepartmentId");

            migrationBuilder.AlterColumn<double>(
                name: "TreatmentQty",
                table: "Treatments",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Treatments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicineType",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "MedicalDepartmentId",
                table: "Ward",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ward",
                table: "Ward",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Surgery",
                table: "Surgery",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "TreatmentStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "TreatmentStatusName",
                value: "OnGoing");

            migrationBuilder.UpdateData(
                table: "TreatmentStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "TreatmentStatusName",
                value: "Completed");

            migrationBuilder.AddForeignKey(
                name: "FK_Bed_Ward_WardId",
                table: "Bed",
                column: "WardId",
                principalTable: "Ward",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Surgery_MedicalDepartments_MedicalDepartmentId",
                table: "Surgery",
                column: "MedicalDepartmentId",
                principalTable: "MedicalDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Surgery_SurgeryId",
                table: "Treatments",
                column: "SurgeryId",
                principalTable: "Surgery",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Ward_WardId",
                table: "Treatments",
                column: "WardId",
                principalTable: "Ward",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ward_MedicalDepartments_MedicalDepartmentId",
                table: "Ward",
                column: "MedicalDepartmentId",
                principalTable: "MedicalDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
