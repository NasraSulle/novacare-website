using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class deskToRecp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "StaffTitles",
                keyColumn: "Id",
                keyValue: 5,
                column: "StaffTitleName",
                value: "Receptionist");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "StaffTitles",
                keyColumn: "Id",
                keyValue: 5,
                column: "StaffTitleName",
                value: "Help Desk");
        }
    }
}
