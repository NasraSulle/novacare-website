using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class MarekebishoForMed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicineGenreId",
                table: "Medicines",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicineType",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicineGenreId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "MedicineType",
                table: "Medicines");
        }
    }
}
