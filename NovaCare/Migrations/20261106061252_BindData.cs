using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class BindData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BillTypes",
                columns: new[] { "Id", "TypeNme" },
                values: new object[,]
                {
                    { 1, "Final" },
                    { 2, "Interim" },
                    { 3, "Advance" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Cost", "DepartmentId", "DoctorId", "HasDept", "HasDoctor", "HasGenre", "HasName", "HasType", "MedicineGenreId", "MediicineTypeId", "ServiceName" },
                values: new object[,]
                {
                    { 1, 0m, null, null, false, false, false, false, false, null, null, "Check-Up" },
                    { 2, 0m, null, null, false, false, false, false, false, null, null, "Dialysis" },
                    { 3, 0m, null, null, false, false, false, false, false, null, null, "Hospitalized" },
                    { 4, 0m, null, null, false, false, false, false, false, null, null, "Medicine" },
                    { 5, 0m, null, null, false, false, false, false, false, null, null, "Vaccines" },
                    { 6, 0m, null, null, false, false, false, false, false, null, null, "Therapy" },
                    { 7, 0m, null, null, false, false, false, false, false, null, null, "Consultation" }
                });

            migrationBuilder.InsertData(
                table: "VisitTypes",
                columns: new[] { "Id", "TypeNme" },
                values: new object[,]
                {
                    { 1, "Consultation" },
                    { 7, "Check-Up " }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BillTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BillTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BillTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "VisitTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VisitTypes",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
