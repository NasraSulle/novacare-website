using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class Sync261113 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardHolder",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CardOfType",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CashReceivedBy",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CashReferenceNumber",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChequeAccName",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChequeAccNumber",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChequeBank",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChequeNumber",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MNONumber",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MNOOperator",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MNOReference",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayType",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WireReferenceNo",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WireSourceAcc",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WireSourceBank",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WireTransferDate",
                table: "Payments",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardHolder",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CardOfType",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CashReceivedBy",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CashReferenceNumber",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ChequeAccName",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ChequeAccNumber",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ChequeBank",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ChequeNumber",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "MNONumber",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "MNOOperator",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "MNOReference",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PayType",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "WireReferenceNo",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "WireSourceAcc",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "WireSourceBank",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "WireTransferDate",
                table: "Payments");
        }
    }
}
