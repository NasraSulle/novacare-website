using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class SyncData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "DeptConsultationFee",
                value: 50000m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "DeptConsultationFee",
                value: 0m);
        }
    }
}
