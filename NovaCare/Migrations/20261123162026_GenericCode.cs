using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class GenericCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocCode",
                table: "DoctorCodes",
                newName: "CodeNumber");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorCodes_DocCode",
                table: "DoctorCodes",
                newName: "IX_DoctorCodes_CodeNumber");

            migrationBuilder.AddColumn<int>(
                name: "BedId",
                table: "DoctorCodes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "DoctorCodes",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "RoomTypeName",
                value: "Pharmacy");

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "RoomTypeCode", "RoomTypeName" },
                values: new object[,]
                {
                    { 6, "006", "Counter" },
                    { 7, "007", "Radiology" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorCodes_BedId",
                table: "DoctorCodes",
                column: "BedId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorCodes_RoomId",
                table: "DoctorCodes",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorCodes_Beds_BedId",
                table: "DoctorCodes",
                column: "BedId",
                principalTable: "Beds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorCodes_Rooms_RoomId",
                table: "DoctorCodes",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorCodes_Beds_BedId",
                table: "DoctorCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorCodes_Rooms_RoomId",
                table: "DoctorCodes");

            migrationBuilder.DropIndex(
                name: "IX_DoctorCodes_BedId",
                table: "DoctorCodes");

            migrationBuilder.DropIndex(
                name: "IX_DoctorCodes_RoomId",
                table: "DoctorCodes");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "BedId",
                table: "DoctorCodes");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "DoctorCodes");

            migrationBuilder.RenameColumn(
                name: "CodeNumber",
                table: "DoctorCodes",
                newName: "DocCode");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorCodes_CodeNumber",
                table: "DoctorCodes",
                newName: "IX_DoctorCodes_DocCode");

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "RoomTypeName",
                value: "Radiology");
        }
    }
}
