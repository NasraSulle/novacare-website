using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class DataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Services_ServiceId",
                table: "Consultations");

            migrationBuilder.DropTable(
                name: "BillServices");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DeleteData(
                table: "VisitTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "TreatmentCode",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "TreatmentName",
                table: "Treatments");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Consultations",
                newName: "MedicalDepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Consultations_ServiceId",
                table: "Consultations",
                newName: "IX_Consultations_MedicalDepartmentId");

            migrationBuilder.AddColumn<bool>(
                name: "HasDoctorId",
                table: "TreatmentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasMedicalDeptId",
                table: "TreatmentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasMedicineId",
                table: "TreatmentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasRoomId",
                table: "TreatmentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasSurgeryId",
                table: "TreatmentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasTherapyId",
                table: "TreatmentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasWardId",
                table: "TreatmentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Treatments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishDate",
                table: "Treatments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MedicalDeptId",
                table: "Treatments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicineId",
                table: "Treatments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Treatments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Treatments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SurgeryId",
                table: "Treatments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TherapyId",
                table: "Treatments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TreatmentCost",
                table: "Treatments",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<double>(
                name: "TreatmentQty",
                table: "Treatments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "WardId",
                table: "Treatments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicalDepartmentId",
                table: "DoctorTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicalDeptId",
                table: "DoctorTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ConsultationCost",
                table: "Consultations",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishDate",
                table: "Consultations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Consultations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Consultations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "MedicalDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalDepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalDepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalDepartments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Surgery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurgeryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurgeryCodeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurgeryCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MedicalDepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surgery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Surgery_MedicalDepartments_MedicalDepartmentId",
                        column: x => x.MedicalDepartmentId,
                        principalTable: "MedicalDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Therapies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TherapyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TherapyCodeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TherapyCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MedicalDepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Therapies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Therapies_MedicalDepartments_MedicalDepartmentId",
                        column: x => x.MedicalDepartmentId,
                        principalTable: "MedicalDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ward",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WardCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MedicalDepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ward", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ward_MedicalDepartments_MedicalDepartmentId",
                        column: x => x.MedicalDepartmentId,
                        principalTable: "MedicalDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BedCodeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bed_Ward_WardId",
                        column: x => x.WardId,
                        principalTable: "Ward",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "DiseaseTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "DiseaseTypeName",
                value: "Genetic");

            migrationBuilder.UpdateData(
                table: "DiseaseTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "DiseaseTypeName",
                value: "Physiological");

            migrationBuilder.InsertData(
                table: "DiseaseTypes",
                columns: new[] { "Id", "DiseaseTypeCode", "DiseaseTypeName" },
                values: new object[,]
                {
                    { 6, "006", "Autoimmune" },
                    { 7, "007", "Degenerative" },
                    { 8, "008", "Neoplastic" },
                    { 9, "009", "Psychiatric" },
                    { 10, "010", "Metabolic" },
                    { 11, "011", "Congenital" },
                    { 12, "012", "Allergic / Immunological" }
                });

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DoctorTypeName", "MedicalDepartmentId", "MedicalDeptId" },
                values: new object[] { "General Practitioner (MD)", null, 1 });

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DoctorTypeName", "MedicalDepartmentId", "MedicalDeptId" },
                values: new object[] { "Gynecologist", null, 2 });

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "MedicalDepartmentId", "MedicalDeptId" },
                values: new object[] { null, 7 });

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "MedicalDepartmentId", "MedicalDeptId" },
                values: new object[] { null, 9 });

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "MedicalDepartmentId", "MedicalDeptId" },
                values: new object[] { null, 10 });

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DoctorTypeName", "MedicalDepartmentId", "MedicalDeptId" },
                values: new object[] { "Orthopedic Surgeon", null, 8 });

            migrationBuilder.InsertData(
                table: "DoctorTypes",
                columns: new[] { "Id", "DoctorTypeCode", "DoctorTypeName", "MedicalDepartmentId", "MedicalDeptId" },
                values: new object[,]
                {
                    { 7, "007", "Emergency Physician", null, 3 },
                    { 8, "008", "Radiologist", null, 4 },
                    { 9, "009", "Pediatrician", null, 5 },
                    { 10, "010", "Intensivist", null, 6 },
                    { 11, "011", "Dermatologist", null, 11 },
                    { 12, "012", "Psychiatrist", null, 12 },
                    { 13, "013", "Gastroenterologist", null, 13 },
                    { 14, "014", "Ophthalmologist", null, 14 },
                    { 15, "015", "General Surgeon", null, 15 }
                });

            migrationBuilder.InsertData(
                table: "MedicalDepartments",
                columns: new[] { "Id", "MedicalDepartmentCode", "MedicalDepartmentName" },
                values: new object[,]
                {
                    { 1, "", "OPD" },
                    { 2, "", "OB-GYN" },
                    { 3, "", "ER" },
                    { 4, "", "Radiology" },
                    { 5, "", "Pediatrics" },
                    { 6, "", "ICU" },
                    { 7, "", "Cardiology" },
                    { 8, "", "Orthopedics" },
                    { 9, "", "Neurology" },
                    { 10, "", "Oncology" },
                    { 11, "", "Dermatology" },
                    { 12, "", "Psychiatry" },
                    { 13, "", "Gastroenterology" },
                    { 14, "", "Ophthalmology" },
                    { 15, "", "General Surgery" }
                });

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "RoomTypeName",
                value: "Therapy Room");

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "RoomTypeCode", "RoomTypeName" },
                values: new object[] { 5, "005", "Radiology" });

            migrationBuilder.UpdateData(
                table: "TreatmentTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HasDoctorId", "HasMedicalDeptId", "HasMedicineId", "HasRoomId", "HasSurgeryId", "HasTherapyId", "HasWardId" },
                values: new object[] { false, false, true, false, false, false, false });

            migrationBuilder.UpdateData(
                table: "TreatmentTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HasDoctorId", "HasMedicalDeptId", "HasMedicineId", "HasRoomId", "HasSurgeryId", "HasTherapyId", "HasWardId" },
                values: new object[] { true, true, false, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "TreatmentTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HasDoctorId", "HasMedicalDeptId", "HasMedicineId", "HasRoomId", "HasSurgeryId", "HasTherapyId", "HasWardId" },
                values: new object[] { true, true, false, true, false, true, false });

            migrationBuilder.UpdateData(
                table: "TreatmentTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "HasDoctorId", "HasMedicalDeptId", "HasMedicineId", "HasRoomId", "HasSurgeryId", "HasTherapyId", "HasWardId", "TreatmentTypeName" },
                values: new object[] { true, true, false, false, false, false, true, "Hospitalized" });

            migrationBuilder.InsertData(
                table: "VisitTypes",
                columns: new[] { "Id", "TypeNme" },
                values: new object[,]
                {
                    { 2, "Check-Up" },
                    { 3, "Treatment" }
                });

            migrationBuilder.InsertData(
                table: "Surgery",
                columns: new[] { "Id", "MedicalDepartmentId", "SurgeryCodeNumber", "SurgeryCost", "SurgeryName" },
                values: new object[,]
                {
                    { 1, 7, "", 430000m, "Heart Transplant" },
                    { 2, 7, "", 150000m, "Coronary Artery Bypass Graft (CABG)" },
                    { 3, 2, "", 25000m, "Cesarean Section (C-Section)" },
                    { 4, 2, "", 35000m, "Hysterectomy" },
                    { 5, 9, "", 180000m, "Craniotomy (Brain Surgery)" },
                    { 6, 9, "", 110000m, "Spinal Fusion" },
                    { 7, 8, "", 45000m, "Total Knee Replacement" },
                    { 8, 8, "", 50000m, "Total Hip Replacement" },
                    { 9, 10, "", 95000m, "Tumor Resection Surgery" },
                    { 10, 14, "", 12000m, "Cataract Surgery" },
                    { 11, 15, "", 18000m, "Appendectomy" },
                    { 12, 15, "", 22000m, "Cholecystectomy (Gallbladder Removal)" },
                    { 13, 15, "", 15000m, "Hernia Repair" }
                });

            migrationBuilder.InsertData(
                table: "Therapies",
                columns: new[] { "Id", "MedicalDepartmentId", "TherapyCodeNumber", "TherapyCost", "TherapyName" },
                values: new object[,]
                {
                    { 1, 1, "", 30000m, "Dialysis" },
                    { 2, 10, "", 90000m, "Radiotherapy" },
                    { 3, 8, "", 20000m, "Physical Therapy" },
                    { 4, 10, "", 70000m, "Chemotherapy" },
                    { 5, 12, "", 15000m, "Cognitive Behavioral Therapy" },
                    { 6, 8, "", 22000m, "Occupational Therapy" },
                    { 7, 9, "", 20000m, "Speech Therapy" },
                    { 8, 7, "", 25000m, "Cardiac Rehabilitation" },
                    { 9, 6, "", 35000m, "Respiratory Therapy" },
                    { 10, 11, "", 18000m, "Phototherapy (UV Light)" },
                    { 11, 8, "", 28000m, "Hydrotherapy" },
                    { 12, 12, "", 65000m, "Electroconvulsive Therapy (ECT)" }
                });

            migrationBuilder.InsertData(
                table: "Ward",
                columns: new[] { "Id", "MedicalDepartmentId", "WardCost", "WardName" },
                values: new object[,]
                {
                    { 1, 2, 20000m, "Maternity Ward" },
                    { 2, 5, 15000m, "General Pediatric Ward" },
                    { 3, 5, 65000m, "Neonatal ICU (NICU)" },
                    { 4, 6, 75000m, "Medical ICU" },
                    { 5, 6, 80000m, "Surgical ICU" },
                    { 6, 7, 70000m, "Cardiac Care Unit (CCU)" },
                    { 7, 7, 35000m, "Cardiology Step-Down Ward" },
                    { 8, 8, 25000m, "Orthopedic & Rehab Ward" },
                    { 9, 9, 40000m, "Stroke & Neurology Ward" },
                    { 10, 10, 45000m, "Oncology Ward" },
                    { 11, 12, 30000m, "Psychiatric Inpatient Unit" },
                    { 12, 15, 30000m, "Post-Surgical Recovery Ward" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_DoctorId",
                table: "Treatments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_MedicalDeptId",
                table: "Treatments",
                column: "MedicalDeptId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_MedicineId",
                table: "Treatments",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_RoomId",
                table: "Treatments",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_SurgeryId",
                table: "Treatments",
                column: "SurgeryId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_TherapyId",
                table: "Treatments",
                column: "TherapyId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_WardId",
                table: "Treatments",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorTypes_MedicalDepartmentId",
                table: "DoctorTypes",
                column: "MedicalDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_RoomId",
                table: "Consultations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Bed_WardId",
                table: "Bed",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_Surgery_MedicalDepartmentId",
                table: "Surgery",
                column: "MedicalDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Therapies_MedicalDepartmentId",
                table: "Therapies",
                column: "MedicalDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Ward_MedicalDepartmentId",
                table: "Ward",
                column: "MedicalDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_MedicalDepartments_MedicalDepartmentId",
                table: "Consultations",
                column: "MedicalDepartmentId",
                principalTable: "MedicalDepartments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Rooms_RoomId",
                table: "Consultations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorTypes_MedicalDepartments_MedicalDepartmentId",
                table: "DoctorTypes",
                column: "MedicalDepartmentId",
                principalTable: "MedicalDepartments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Doctors_DoctorId",
                table: "Treatments",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_MedicalDepartments_MedicalDeptId",
                table: "Treatments",
                column: "MedicalDeptId",
                principalTable: "MedicalDepartments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Medicines_MedicineId",
                table: "Treatments",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Rooms_RoomId",
                table: "Treatments",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Surgery_SurgeryId",
                table: "Treatments",
                column: "SurgeryId",
                principalTable: "Surgery",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Therapies_TherapyId",
                table: "Treatments",
                column: "TherapyId",
                principalTable: "Therapies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Ward_WardId",
                table: "Treatments",
                column: "WardId",
                principalTable: "Ward",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_MedicalDepartments_MedicalDepartmentId",
                table: "Consultations");

            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Rooms_RoomId",
                table: "Consultations");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorTypes_MedicalDepartments_MedicalDepartmentId",
                table: "DoctorTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Doctors_DoctorId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_MedicalDepartments_MedicalDeptId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Medicines_MedicineId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Rooms_RoomId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Surgery_SurgeryId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Therapies_TherapyId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Ward_WardId",
                table: "Treatments");

            migrationBuilder.DropTable(
                name: "Bed");

            migrationBuilder.DropTable(
                name: "Surgery");

            migrationBuilder.DropTable(
                name: "Therapies");

            migrationBuilder.DropTable(
                name: "Ward");

            migrationBuilder.DropTable(
                name: "MedicalDepartments");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_DoctorId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_MedicalDeptId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_MedicineId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_RoomId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_SurgeryId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_TherapyId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_WardId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_DoctorTypes_MedicalDepartmentId",
                table: "DoctorTypes");

            migrationBuilder.DropIndex(
                name: "IX_Consultations_RoomId",
                table: "Consultations");

            migrationBuilder.DeleteData(
                table: "DiseaseTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DiseaseTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DiseaseTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DiseaseTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DiseaseTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "DiseaseTypes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "DiseaseTypes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "VisitTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VisitTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "HasDoctorId",
                table: "TreatmentTypes");

            migrationBuilder.DropColumn(
                name: "HasMedicalDeptId",
                table: "TreatmentTypes");

            migrationBuilder.DropColumn(
                name: "HasMedicineId",
                table: "TreatmentTypes");

            migrationBuilder.DropColumn(
                name: "HasRoomId",
                table: "TreatmentTypes");

            migrationBuilder.DropColumn(
                name: "HasSurgeryId",
                table: "TreatmentTypes");

            migrationBuilder.DropColumn(
                name: "HasTherapyId",
                table: "TreatmentTypes");

            migrationBuilder.DropColumn(
                name: "HasWardId",
                table: "TreatmentTypes");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "FinishDate",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "MedicalDeptId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "MedicineId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "SurgeryId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "TherapyId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "TreatmentCost",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "TreatmentQty",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "MedicalDepartmentId",
                table: "DoctorTypes");

            migrationBuilder.DropColumn(
                name: "MedicalDeptId",
                table: "DoctorTypes");

            migrationBuilder.DropColumn(
                name: "ConsultationCost",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "FinishDate",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Consultations");

            migrationBuilder.RenameColumn(
                name: "MedicalDepartmentId",
                table: "Consultations",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Consultations_MedicalDepartmentId",
                table: "Consultations",
                newName: "IX_Consultations_ServiceId");

            migrationBuilder.AddColumn<string>(
                name: "TreatmentCode",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TreatmentName",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    HasDept = table.Column<bool>(type: "bit", nullable: false),
                    HasDoctor = table.Column<bool>(type: "bit", nullable: false),
                    HasGenre = table.Column<bool>(type: "bit", nullable: false),
                    HasName = table.Column<bool>(type: "bit", nullable: false),
                    HasType = table.Column<bool>(type: "bit", nullable: false),
                    MedicineGenreId = table.Column<int>(type: "int", nullable: true),
                    MedicineTypeId = table.Column<int>(type: "int", nullable: true),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_DoctorTypes_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "DoctorTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BillServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    InsuranceDeduction = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NetAmountDue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    VAT = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillServices_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "DiseaseTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "DiseaseTypeName",
                value: "Genetic ");

            migrationBuilder.UpdateData(
                table: "DiseaseTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "DiseaseTypeName",
                value: "Physiological ");

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DoctorTypeName",
                value: "MD");

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DoctorTypeName",
                value: "OB-GYN");

            migrationBuilder.UpdateData(
                table: "DoctorTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "DoctorTypeName",
                value: "Orthopedic");

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "RoomTypeName",
                value: "Radiology");

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Cost", "DepartmentId", "DoctorId", "HasDept", "HasDoctor", "HasGenre", "HasName", "HasType", "MedicineGenreId", "MedicineTypeId", "ServiceName" },
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

            migrationBuilder.UpdateData(
                table: "TreatmentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "TreatmentTypeName",
                value: "Implantation");

            migrationBuilder.InsertData(
                table: "VisitTypes",
                columns: new[] { "Id", "TypeNme" },
                values: new object[] { 7, "Check-Up " });

            migrationBuilder.CreateIndex(
                name: "IX_BillServices_BillId",
                table: "BillServices",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillServices_ServiceId",
                table: "BillServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_DepartmentId",
                table: "Services",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_DoctorId",
                table: "Services",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Services_ServiceId",
                table: "Consultations",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }
    }
}
