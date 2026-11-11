using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class MarekebishoForBill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Bills_BillId",
                table: "Consultations");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTests_Bills_BillId",
                table: "LabTests");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Bills_BillId",
                table: "Medicines");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Visits_VisitId",
                table: "Medicines");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Bills_BillId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Medicines_MedicineId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Visits_VisitId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_BillId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_BillId",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_VisitId",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_LabTests_BillId",
                table: "LabTests");

            migrationBuilder.DropIndex(
                name: "IX_Consultations_BillId",
                table: "Consultations");

            migrationBuilder.DeleteData(
                table: "TreatmentTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "HasMedicineId",
                table: "TreatmentTypes");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "VisitId",
                table: "Medicines");

            migrationBuilder.RenameColumn(
                name: "TypeNme",
                table: "VisitTypes",
                newName: "TypeName");

            migrationBuilder.RenameColumn(
                name: "MedicineId",
                table: "Treatments",
                newName: "ConsultationId");

            migrationBuilder.RenameColumn(
                name: "BillId",
                table: "Treatments",
                newName: "BillItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Treatments_MedicineId",
                table: "Treatments",
                newName: "IX_Treatments_ConsultationId");

            migrationBuilder.RenameColumn(
                name: "LabTestStatusname",
                table: "LabTests",
                newName: "LabTestStatusName");

            migrationBuilder.RenameColumn(
                name: "BillId",
                table: "LabTests",
                newName: "BillItemId");

            migrationBuilder.RenameColumn(
                name: "BillId",
                table: "Consultations",
                newName: "BillItemId");

            migrationBuilder.RenameColumn(
                name: "Itemname",
                table: "BillItems",
                newName: "ItemName");

            migrationBuilder.RenameColumn(
                name: "cost",
                table: "BillItems",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "Qty",
                table: "BillItems",
                newName: "SourceItemId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DischargedDate",
                table: "Visits",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "VisitId",
                table: "Treatments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishDate",
                table: "Treatments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishDate",
                table: "Consultations",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "BillItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "SourceType",
                table: "BillItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescribedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    VisitId = table.Column<int>(type: "int", nullable: false),
                    ConsultationId = table.Column<int>(type: "int", nullable: true),
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    BillItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_BillItems_BillItemId",
                        column: x => x.BillItemId,
                        principalTable: "BillItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Prescriptions_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Prescriptions_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Prescriptions_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "PaymentStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentStatusName",
                value: "Unpaid");

            migrationBuilder.InsertData(
                table: "PaymentStatus",
                columns: new[] { "Id", "PaymentStatusName" },
                values: new object[] { 3, "Full Payment" });

            migrationBuilder.UpdateData(
                table: "TreatmentTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HasDoctorId", "HasMedicalDeptId", "HasRoomId", "HasSurgeryId", "TreatmentTypeName" },
                values: new object[] { true, true, true, true, "Surgery" });

            migrationBuilder.UpdateData(
                table: "TreatmentTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HasSurgeryId", "HasTherapyId", "TreatmentTypeName" },
                values: new object[] { false, true, "Therapy" });

            migrationBuilder.UpdateData(
                table: "TreatmentTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HasRoomId", "HasTherapyId", "HasWardId", "TreatmentTypeName" },
                values: new object[] { false, false, true, "Hospitalized" });

            migrationBuilder.InsertData(
                table: "VisitTypes",
                columns: new[] { "Id", "TypeName" },
                values: new object[] { 4, "Pharmacy" });

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_BillItemId",
                table: "Treatments",
                column: "BillItemId",
                unique: true,
                filter: "[BillItemId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_BillItemId",
                table: "LabTests",
                column: "BillItemId",
                unique: true,
                filter: "[BillItemId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_ConsultationId",
                table: "LabTests",
                column: "ConsultationId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_BillItemId",
                table: "Consultations",
                column: "BillItemId",
                unique: true,
                filter: "[BillItemId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_BillItemId",
                table: "Prescriptions",
                column: "BillItemId",
                unique: true,
                filter: "[BillItemId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_ConsultationId",
                table: "Prescriptions",
                column: "ConsultationId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DoctorId",
                table: "Prescriptions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_MedicineId",
                table: "Prescriptions",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_VisitId",
                table: "Prescriptions",
                column: "VisitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_BillItems_BillItemId",
                table: "Consultations",
                column: "BillItemId",
                principalTable: "BillItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTests_BillItems_BillItemId",
                table: "LabTests",
                column: "BillItemId",
                principalTable: "BillItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTests_Consultations_ConsultationId",
                table: "LabTests",
                column: "ConsultationId",
                principalTable: "Consultations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_BillItems_BillItemId",
                table: "Treatments",
                column: "BillItemId",
                principalTable: "BillItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Consultations_ConsultationId",
                table: "Treatments",
                column: "ConsultationId",
                principalTable: "Consultations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Visits_VisitId",
                table: "Treatments",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_BillItems_BillItemId",
                table: "Consultations");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTests_BillItems_BillItemId",
                table: "LabTests");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTests_Consultations_ConsultationId",
                table: "LabTests");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_BillItems_BillItemId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Consultations_ConsultationId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Visits_VisitId",
                table: "Treatments");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_BillItemId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_LabTests_BillItemId",
                table: "LabTests");

            migrationBuilder.DropIndex(
                name: "IX_LabTests_ConsultationId",
                table: "LabTests");

            migrationBuilder.DropIndex(
                name: "IX_Consultations_BillItemId",
                table: "Consultations");

            migrationBuilder.DeleteData(
                table: "PaymentStatus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "VisitTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "SourceType",
                table: "BillItems");

            migrationBuilder.RenameColumn(
                name: "TypeName",
                table: "VisitTypes",
                newName: "TypeNme");

            migrationBuilder.RenameColumn(
                name: "ConsultationId",
                table: "Treatments",
                newName: "MedicineId");

            migrationBuilder.RenameColumn(
                name: "BillItemId",
                table: "Treatments",
                newName: "BillId");

            migrationBuilder.RenameIndex(
                name: "IX_Treatments_ConsultationId",
                table: "Treatments",
                newName: "IX_Treatments_MedicineId");

            migrationBuilder.RenameColumn(
                name: "LabTestStatusName",
                table: "LabTests",
                newName: "LabTestStatusname");

            migrationBuilder.RenameColumn(
                name: "BillItemId",
                table: "LabTests",
                newName: "BillId");

            migrationBuilder.RenameColumn(
                name: "BillItemId",
                table: "Consultations",
                newName: "BillId");

            migrationBuilder.RenameColumn(
                name: "ItemName",
                table: "BillItems",
                newName: "Itemname");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "BillItems",
                newName: "cost");

            migrationBuilder.RenameColumn(
                name: "SourceItemId",
                table: "BillItems",
                newName: "Qty");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DischargedDate",
                table: "Visits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasMedicineId",
                table: "TreatmentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "VisitId",
                table: "Treatments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishDate",
                table: "Treatments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "Medicines",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisitId",
                table: "Medicines",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishDate",
                table: "Consultations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "PaymentStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentStatusName",
                value: "Full Payment");

            migrationBuilder.UpdateData(
                table: "TreatmentTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HasDoctorId", "HasMedicalDeptId", "HasMedicineId", "HasRoomId", "HasSurgeryId", "TreatmentTypeName" },
                values: new object[] { false, false, true, false, false, "Medication" });

            migrationBuilder.UpdateData(
                table: "TreatmentTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HasMedicineId", "HasSurgeryId", "HasTherapyId", "TreatmentTypeName" },
                values: new object[] { false, true, false, "Surgery" });

            migrationBuilder.UpdateData(
                table: "TreatmentTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HasMedicineId", "HasRoomId", "HasTherapyId", "HasWardId", "TreatmentTypeName" },
                values: new object[] { false, true, true, false, "Therapy" });

            migrationBuilder.InsertData(
                table: "TreatmentTypes",
                columns: new[] { "Id", "HasDoctorId", "HasMedicalDeptId", "HasMedicineId", "HasRoomId", "HasSurgeryId", "HasTherapyId", "HasWardId", "TreatmentTypeCode", "TreatmentTypeName" },
                values: new object[] { 4, true, true, false, false, false, false, true, "004", "Hospitalized" });

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_BillId",
                table: "Treatments",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_BillId",
                table: "Medicines",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_VisitId",
                table: "Medicines",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_BillId",
                table: "LabTests",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_BillId",
                table: "Consultations",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Bills_BillId",
                table: "Consultations",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTests_Bills_BillId",
                table: "LabTests",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Bills_BillId",
                table: "Medicines",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Visits_VisitId",
                table: "Medicines",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Bills_BillId",
                table: "Treatments",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Medicines_MedicineId",
                table: "Treatments",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Visits_VisitId",
                table: "Treatments",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id");
        }
    }
}
