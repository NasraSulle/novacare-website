using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class BedModal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bed_Wards_WardId",
                table: "Bed");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bed",
                table: "Bed");

            migrationBuilder.RenameTable(
                name: "Bed",
                newName: "Beds");

            migrationBuilder.RenameIndex(
                name: "IX_Bed_WardId",
                table: "Beds",
                newName: "IX_Beds_WardId");

            migrationBuilder.AddColumn<string>(
                name: "BedAvailability",
                table: "Beds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Beds",
                table: "Beds",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Beds_Wards_WardId",
                table: "Beds",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beds_Wards_WardId",
                table: "Beds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Beds",
                table: "Beds");

            migrationBuilder.DropColumn(
                name: "BedAvailability",
                table: "Beds");

            migrationBuilder.RenameTable(
                name: "Beds",
                newName: "Bed");

            migrationBuilder.RenameIndex(
                name: "IX_Beds_WardId",
                table: "Bed",
                newName: "IX_Bed_WardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bed",
                table: "Bed",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bed_Wards_WardId",
                table: "Bed",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "Id");
        }
    }
}
