using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class ConsultationFees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DeptConsultationFee",
                table: "DoctorTypes",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DoctorConsultationFee",
                table: "Doctors",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DeptConsultationFee",
                value: 10000m);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DeptConsultationFee",
                value: 30000m);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "DeptConsultationFee",
                value: 50000m);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "DeptConsultationFee",
                value: 50000m);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "DeptConsultationFee",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "DeptConsultationFee",
                value: 10000m);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "DeptConsultationFee",
                value: 50000m);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 8,
                column: "DeptConsultationFee",
                value: 10000m);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 9,
                column: "DeptConsultationFee",
                value: 10000m);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 10,
                column: "DeptConsultationFee",
                value: 15000m);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 11,
                column: "DeptConsultationFee",
                value: 25000m);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 12,
                column: "DeptConsultationFee",
                value: 25000m);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 13,
                column: "DeptConsultationFee",
                value: 50000m);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 14,
                column: "DeptConsultationFee",
                value: 10000m);

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 15,
                column: "DeptConsultationFee",
                value: 10000m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeptConsultationFee",
                table: "DoctorTypes");

            migrationBuilder.DropColumn(
                name: "DoctorConsultationFee",
                table: "Doctors");
        }
    }
}
