using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class Sync261113Asr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StaffId",
                table: "Bills",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_StaffId",
                table: "Bills",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_AspNetUsers_StaffId",
                table: "Bills",
                column: "StaffId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_AspNetUsers_StaffId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_StaffId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Bills");
        }
    }
}
