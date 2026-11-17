using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class ReferralData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReferredHospital",
                table: "Referrals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferredHospital",
                table: "Referrals");
        }
    }
}
