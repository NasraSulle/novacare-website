using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class fixreltn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorCodes_Beds_BedId",
                table: "DoctorCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorCodes_Doctors_DoctorId",
                table: "DoctorCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorCodes_Rooms_RoomId",
                table: "DoctorCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorTypes_MedicalDepartments_MedicalDepartmentId",
                table: "DoctorTypes");

            migrationBuilder.DropIndex(
                name: "IX_DoctorTypes_MedicalDepartmentId",
                table: "DoctorTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorCodes",
                table: "DoctorCodes");

            migrationBuilder.DeleteData(
                table: "DoctorStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "MedicalDepartmentId",
                table: "DoctorTypes");

            migrationBuilder.RenameTable(
                name: "DoctorCodes",
                newName: "GenericCodes");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorCodes_RoomId",
                table: "GenericCodes",
                newName: "IX_GenericCodes_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorCodes_DoctorId",
                table: "GenericCodes",
                newName: "IX_GenericCodes_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorCodes_CodeNumber",
                table: "GenericCodes",
                newName: "IX_GenericCodes_CodeNumber");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorCodes_BedId",
                table: "GenericCodes",
                newName: "IX_GenericCodes_BedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenericCodes",
                table: "GenericCodes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorTypes_MedicalDeptId",
                table: "DoctorTypes",
                column: "MedicalDeptId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorTypes_MedicalDepartments_MedicalDeptId",
                table: "DoctorTypes",
                column: "MedicalDeptId",
                principalTable: "MedicalDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenericCodes_Beds_BedId",
                table: "GenericCodes",
                column: "BedId",
                principalTable: "Beds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GenericCodes_Doctors_DoctorId",
                table: "GenericCodes",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GenericCodes_Rooms_RoomId",
                table: "GenericCodes",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorTypes_MedicalDepartments_MedicalDeptId",
                table: "DoctorTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_GenericCodes_Beds_BedId",
                table: "GenericCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_GenericCodes_Doctors_DoctorId",
                table: "GenericCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_GenericCodes_Rooms_RoomId",
                table: "GenericCodes");

            migrationBuilder.DropIndex(
                name: "IX_DoctorTypes_MedicalDeptId",
                table: "DoctorTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenericCodes",
                table: "GenericCodes");

            migrationBuilder.RenameTable(
                name: "GenericCodes",
                newName: "DoctorCodes");

            migrationBuilder.RenameIndex(
                name: "IX_GenericCodes_RoomId",
                table: "DoctorCodes",
                newName: "IX_DoctorCodes_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_GenericCodes_DoctorId",
                table: "DoctorCodes",
                newName: "IX_DoctorCodes_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_GenericCodes_CodeNumber",
                table: "DoctorCodes",
                newName: "IX_DoctorCodes_CodeNumber");

            migrationBuilder.RenameIndex(
                name: "IX_GenericCodes_BedId",
                table: "DoctorCodes",
                newName: "IX_DoctorCodes_BedId");

            migrationBuilder.AddColumn<int>(
                name: "MedicalDepartmentId",
                table: "DoctorTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorCodes",
                table: "DoctorCodes",
                column: "Id");

            migrationBuilder.InsertData(
                table: "DoctorStatuses",
                columns: new[] { "Id", "DoctorStatusName" },
                values: new object[] { 3, "WithPatients" });

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "MedicalDepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "MedicalDepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "MedicalDepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "MedicalDepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "MedicalDepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "MedicalDepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "MedicalDepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 8,
                column: "MedicalDepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 9,
                column: "MedicalDepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 10,
                column: "MedicalDepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 11,
                column: "MedicalDepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 12,
                column: "MedicalDepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 13,
                column: "MedicalDepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 14,
                column: "MedicalDepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 15,
                column: "MedicalDepartmentId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorTypes_MedicalDepartmentId",
                table: "DoctorTypes",
                column: "MedicalDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorCodes_Beds_BedId",
                table: "DoctorCodes",
                column: "BedId",
                principalTable: "Beds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorCodes_Doctors_DoctorId",
                table: "DoctorCodes",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorCodes_Rooms_RoomId",
                table: "DoctorCodes",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorTypes_MedicalDepartments_MedicalDepartmentId",
                table: "DoctorTypes",
                column: "MedicalDepartmentId",
                principalTable: "MedicalDepartments",
                principalColumn: "Id");
        }
    }
}
