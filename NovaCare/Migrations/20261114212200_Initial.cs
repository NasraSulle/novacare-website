using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NovaCare.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "DoctorCodeSequence",
                startValue: 1000L);

            migrationBuilder.CreateSequence<int>(
                name: "EmployeeIdCodeSequence",
                startValue: 1000L);

            migrationBuilder.CreateSequence<int>(
                name: "PatientCodeSequence",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeNme = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiseaseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiseaseTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiseaseTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Insuarances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuaranceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrivillageCodeNumber = table.Column<int>(type: "int", nullable: false),
                    IntegrationType = table.Column<int>(type: "int", nullable: false),
                    RequiresPreAuth = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PortalUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insuarances", x => x.Id);
                });

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
                name: "MedicineGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineGenreCode = table.Column<int>(type: "int", nullable: false),
                    MedicineGenreName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineGenres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicineStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentModes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentModeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentModes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffTitleGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffTitleGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TreatmentStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TreatmentTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TreatmentTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasSurgeryId = table.Column<bool>(type: "bit", nullable: false),
                    HasTherapyId = table.Column<bool>(type: "bit", nullable: false),
                    HasMedicalDeptId = table.Column<bool>(type: "bit", nullable: false),
                    HasWardId = table.Column<bool>(type: "bit", nullable: false),
                    HasDoctorId = table.Column<bool>(type: "bit", nullable: false),
                    HasRoomId = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VisitStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VisitTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiseaseCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiseaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LabTestCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiseaseTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diseases_DiseaseTypes_DiseaseTypeId",
                        column: x => x.DiseaseTypeId,
                        principalTable: "DiseaseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceSchemes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchemeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchemeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultCoveragePercent = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DefaultCopayPercent = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AnnualLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PerVisitLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    InsuaranceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceSchemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuranceSchemes_Insuarances_InsuaranceId",
                        column: x => x.InsuaranceId,
                        principalTable: "Insuarances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeptConsultationFee = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MedicalDeptId = table.Column<int>(type: "int", nullable: false),
                    MedicalDepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorTypes_MedicalDepartments_MedicalDepartmentId",
                        column: x => x.MedicalDepartmentId,
                        principalTable: "MedicalDepartments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Surgeries",
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
                    table.PrimaryKey("PK_Surgeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Surgeries_MedicalDepartments_MedicalDepartmentId",
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
                name: "Wards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WardCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MedicalDepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wards_MedicalDepartments_MedicalDepartmentId",
                        column: x => x.MedicalDepartmentId,
                        principalTable: "MedicalDepartments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MedicineTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineTypeCode = table.Column<int>(type: "int", nullable: false),
                    MedicineTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicineGenreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineTypes_MedicineGenres_MedicineGenreId",
                        column: x => x.MedicineGenreId,
                        principalTable: "MedicineGenres",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientPhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    KinFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KinPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KinHomeAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientStatusId = table.Column<int>(type: "int", nullable: false),
                    PatientTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_PatientStatuses_PatientStatusId",
                        column: x => x.PatientStatusId,
                        principalTable: "PatientStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patients_PatientTypes_PatientTypeId",
                        column: x => x.PatientTypeId,
                        principalTable: "PatientTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomStatusId = table.Column<int>(type: "int", nullable: false),
                    RoomTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomStatus_RoomStatusId",
                        column: x => x.RoomStatusId,
                        principalTable: "RoomStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FisrtName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffPhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    IsGetStaffModules = table.Column<bool>(type: "bit", nullable: false),
                    IsViewAllPatients = table.Column<bool>(type: "bit", nullable: false),
                    IsViewAllDoctors = table.Column<bool>(type: "bit", nullable: false),
                    IsViewAllStaffs = table.Column<bool>(type: "bit", nullable: false),
                    IsViewAllHelps = table.Column<bool>(type: "bit", nullable: false),
                    IsViewAllLeaders = table.Column<bool>(type: "bit", nullable: false),
                    IsViewAllMedicines = table.Column<bool>(type: "bit", nullable: false),
                    IsViewAllRooms = table.Column<bool>(type: "bit", nullable: false),
                    IsViewAllNurses = table.Column<bool>(type: "bit", nullable: false),
                    IsViewAllAdmins = table.Column<bool>(type: "bit", nullable: false),
                    IsViewAllLabTests = table.Column<bool>(type: "bit", nullable: false),
                    IsViewLabTestById = table.Column<bool>(type: "bit", nullable: false),
                    IsViewPatientById = table.Column<bool>(type: "bit", nullable: false),
                    IsViewDoctorById = table.Column<bool>(type: "bit", nullable: false),
                    IsViewStaffById = table.Column<bool>(type: "bit", nullable: false),
                    IsViewHelpById = table.Column<bool>(type: "bit", nullable: false),
                    IsViewLeaderById = table.Column<bool>(type: "bit", nullable: false),
                    IsViewMedicineById = table.Column<bool>(type: "bit", nullable: false),
                    IsViewRoomById = table.Column<bool>(type: "bit", nullable: false),
                    IsCreateNewLabTest = table.Column<bool>(type: "bit", nullable: false),
                    IsCreateNewPatient = table.Column<bool>(type: "bit", nullable: false),
                    IsCreateNewStaff = table.Column<bool>(type: "bit", nullable: false),
                    IsCreateNewMedicine = table.Column<bool>(type: "bit", nullable: false),
                    AccessConfigurations = table.Column<bool>(type: "bit", nullable: false),
                    IsUpdateOrEditLabTest = table.Column<bool>(type: "bit", nullable: false),
                    IsUpdateOrEditPatient = table.Column<bool>(type: "bit", nullable: false),
                    IsUpdateOrEditStaff = table.Column<bool>(type: "bit", nullable: false),
                    IsUpdateOrEditMedicine = table.Column<bool>(type: "bit", nullable: false),
                    IsStaffOfTTheWeek = table.Column<bool>(type: "bit", nullable: false),
                    CanChooseStaffOfTheWeek = table.Column<bool>(type: "bit", nullable: false),
                    StaffStatusId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_StaffStatuses_StaffStatusId",
                        column: x => x.StaffStatusId,
                        principalTable: "StaffStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffTitleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StaffTypeId = table.Column<int>(type: "int", nullable: false),
                    TitleGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffTitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffTitles_StaffTitleGroups_TitleGroupId",
                        column: x => x.TitleGroupId,
                        principalTable: "StaffTitleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffTitles_StaffTypes_StaffTypeId",
                        column: x => x.StaffTypeId,
                        principalTable: "StaffTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsurerTariffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceItemId = table.Column<int>(type: "int", nullable: false),
                    InsurerItemCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsurerItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgreedPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CoveragePercent = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsCovered = table.Column<bool>(type: "bit", nullable: false),
                    RequiresPreApproval = table.Column<bool>(type: "bit", nullable: false),
                    InsuaranceId = table.Column<int>(type: "int", nullable: false),
                    InsuranceSchemeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurerTariffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsurerTariffs_Insuarances_InsuaranceId",
                        column: x => x.InsuaranceId,
                        principalTable: "Insuarances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsurerTariffs_InsuranceSchemes_InsuranceSchemeId",
                        column: x => x.InsuranceSchemeId,
                        principalTable: "InsuranceSchemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Beds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BedCodeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BedAvailability = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beds_Wards_WardId",
                        column: x => x.WardId,
                        principalTable: "Wards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    MedicineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicineGenre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicineGenreId = table.Column<int>(type: "int", nullable: true),
                    MedicineType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicineTypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicineQty = table.Column<double>(type: "float", nullable: false),
                    BuyingPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MedicineStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicines_MedicineStatuses_MedicineStatusId",
                        column: x => x.MedicineStatusId,
                        principalTable: "MedicineStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medicines_MedicineTypes_MedicineTypeId",
                        column: x => x.MedicineTypeId,
                        principalTable: "MedicineTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientCodeNumber = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [PatientCodeSequence]"),
                    PatientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientCodes_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientDiseases",
                columns: table => new
                {
                    PatientDiseaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiseaseId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDiseases", x => x.PatientDiseaseId);
                    table.ForeignKey(
                        name: "FK_PatientDiseases_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientDiseases_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientInsuarances",
                columns: table => new
                {
                    PatientInsuaranceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrincipalMemberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationshipToPrincipal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    InsuaranceId = table.Column<int>(type: "int", nullable: false),
                    InsuranceSchemeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientInsuarances", x => x.PatientInsuaranceId);
                    table.ForeignKey(
                        name: "FK_PatientInsuarances_Insuarances_InsuaranceId",
                        column: x => x.InsuaranceId,
                        principalTable: "Insuarances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientInsuarances_InsuranceSchemes_InsuranceSchemeId",
                        column: x => x.InsuranceSchemeId,
                        principalTable: "InsuranceSchemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientInsuarances_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserPasskeys",
                columns: table => new
                {
                    CredentialId = table.Column<byte[]>(type: "varbinary(1024)", maxLength: 1024, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserPasskeys", x => x.CredentialId);
                    table.ForeignKey(
                        name: "FK_AspNetUserPasskeys_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalLicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorConsultationFee = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DoctorTypeId = table.Column<int>(type: "int", nullable: false),
                    DoctorStatusId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: true),
                    StaffId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LicenceExpDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_AspNetUsers_StaffId",
                        column: x => x.StaffId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_DoctorStatuses_DoctorStatusId",
                        column: x => x.DoctorStatusId,
                        principalTable: "DoctorStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_DoctorTypes_DoctorTypeId",
                        column: x => x.DoctorTypeId,
                        principalTable: "DoctorTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeIdCode = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [EmployeeIdCodeSequence]"),
                    StaffId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeIds_AspNetUsers_StaffId",
                        column: x => x.StaffId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StaffStaffTitle",
                columns: table => new
                {
                    StaffTitlesId = table.Column<int>(type: "int", nullable: false),
                    StaffsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffStaffTitle", x => new { x.StaffTitlesId, x.StaffsId });
                    table.ForeignKey(
                        name: "FK_StaffStaffTitle_AspNetUsers_StaffsId",
                        column: x => x.StaffsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffStaffTitle_StaffTitles_StaffTitlesId",
                        column: x => x.StaffTitlesId,
                        principalTable: "StaffTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicineMedicineTypes",
                columns: table => new
                {
                    MedicineMedicineTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineTypeId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineMedicineTypes", x => x.MedicineMedicineTypeId);
                    table.ForeignKey(
                        name: "FK_MedicineMedicineTypes_MedicineTypes_MedicineTypeId",
                        column: x => x.MedicineTypeId,
                        principalTable: "MedicineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineMedicineTypes_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DischargedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitTypeId = table.Column<int>(type: "int", nullable: false),
                    VisitStatusId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    PaymentModeId = table.Column<int>(type: "int", nullable: false),
                    PatientInsuaranceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visits_PatientInsuarances_PatientInsuaranceId",
                        column: x => x.PatientInsuaranceId,
                        principalTable: "PatientInsuarances",
                        principalColumn: "PatientInsuaranceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visits_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_PaymentModes_PaymentModeId",
                        column: x => x.PaymentModeId,
                        principalTable: "PaymentModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visits_VisitStatuses_VisitStatusId",
                        column: x => x.VisitStatusId,
                        principalTable: "VisitStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_VisitTypes_VisitTypeId",
                        column: x => x.VisitTypeId,
                        principalTable: "VisitTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocCode = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [DoctorCodeSequence]"),
                    DoctorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorCodes_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DoctorPatients",
                columns: table => new
                {
                    DoctorPatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorPatients", x => x.DoctorPatientId);
                    table.ForeignKey(
                        name: "FK_DoctorPatients_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorPatients_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BillNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalPatientAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalInsuranceAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PatientPaidAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    InsurancePaidAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    VisitId = table.Column<int>(type: "int", nullable: false),
                    BillTypeId = table.Column<int>(type: "int", nullable: false),
                    PaymentModeId = table.Column<int>(type: "int", nullable: false),
                    PaymentStatusId = table.Column<int>(type: "int", nullable: false),
                    PatientInsuaranceId = table.Column<int>(type: "int", nullable: true),
                    StaffId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_AspNetUsers_StaffId",
                        column: x => x.StaffId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_BillTypes_BillTypeId",
                        column: x => x.BillTypeId,
                        principalTable: "BillTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_PatientInsuarances_PatientInsuaranceId",
                        column: x => x.PatientInsuaranceId,
                        principalTable: "PatientInsuarances",
                        principalColumn: "PatientInsuaranceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_PaymentModes_PaymentModeId",
                        column: x => x.PaymentModeId,
                        principalTable: "PaymentModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_PaymentStatus_PaymentStatusId",
                        column: x => x.PaymentStatusId,
                        principalTable: "PaymentStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreliminaryTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreliminaryWeight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PreliminaryHeight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PreliminaryPressure = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    VisitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreliminaryTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreliminaryTests_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitInsuranceAuths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorizationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferralNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchemeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    VerificationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerificationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorizedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VisitId = table.Column<int>(type: "int", nullable: false),
                    PatientInsuaranceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitInsuranceAuths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitInsuranceAuths_PatientInsuarances_PatientInsuaranceId",
                        column: x => x.PatientInsuaranceId,
                        principalTable: "PatientInsuarances",
                        principalColumn: "PatientInsuaranceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitInsuranceAuths_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsurerItemCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsInsuranceCovered = table.Column<bool>(type: "bit", nullable: false),
                    InsuranceCoveragePercent = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    InsuranceCoveredAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PatientPayableAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ClaimItemStatus = table.Column<int>(type: "int", nullable: false),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillItems_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FolioNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorizationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimedAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ApprovedAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RejectedAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SettledAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdjudicatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SettledDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrimaryIcd10Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueryOrRejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubmissionPayloadJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillId = table.Column<int>(type: "int", nullable: false),
                    InsuaranceId = table.Column<int>(type: "int", nullable: false),
                    PatientInsuaranceId = table.Column<int>(type: "int", nullable: true),
                    SubmittedByStaffId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuranceClaims_AspNetUsers_SubmittedByStaffId",
                        column: x => x.SubmittedByStaffId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InsuranceClaims_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsuranceClaims_Insuarances_InsuaranceId",
                        column: x => x.InsuaranceId,
                        principalTable: "Insuarances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsuranceClaims_PatientInsuarances_PatientInsuaranceId",
                        column: x => x.PatientInsuaranceId,
                        principalTable: "PatientInsuarances",
                        principalColumn: "PatientInsuaranceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Consultations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icd10Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryIcd10Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClinicalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsultationCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    VisitId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    MedicalDepartmentId = table.Column<int>(type: "int", nullable: true),
                    BillItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultations_BillItems_BillItemId",
                        column: x => x.BillItemId,
                        principalTable: "BillItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Consultations_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Consultations_MedicalDepartments_MedicalDepartmentId",
                        column: x => x.MedicalDepartmentId,
                        principalTable: "MedicalDepartments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Consultations_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Consultations_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PayType = table.Column<int>(type: "int", nullable: false),
                    CashReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CashReceivedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChequeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChequeBank = table.Column<int>(type: "int", nullable: true),
                    ChequeAccName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChequeAccNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WireReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WireTransferDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WireSourceAcc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WireSourceBank = table.Column<int>(type: "int", nullable: true),
                    CardHolder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardOfType = table.Column<int>(type: "int", nullable: true),
                    MNONumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MNOReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MNOOperator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsurerSettlementReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SettlementBatchNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillId = table.Column<int>(type: "int", nullable: false),
                    PaymentStatusId = table.Column<int>(type: "int", nullable: false),
                    InsuranceClaimId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_InsuranceClaims_InsuranceClaimId",
                        column: x => x.InsuranceClaimId,
                        principalTable: "InsuranceClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentStatus_PaymentStatusId",
                        column: x => x.PaymentStatusId,
                        principalTable: "PaymentStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LabTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabTestCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiseaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LabTestStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultAttachment = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    RequesteeDoctor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdaterLabTechnician = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequesteeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdaterId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LabTestCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    VisitId = table.Column<int>(type: "int", nullable: false),
                    ConsultationId = table.Column<int>(type: "int", nullable: true),
                    BillItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabTests_BillItems_BillItemId",
                        column: x => x.BillItemId,
                        principalTable: "BillItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LabTests_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LabTests_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescribedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    DispensedQuantity = table.Column<double>(type: "float", nullable: false),
                    IsDispensed = table.Column<bool>(type: "bit", nullable: false),
                    DispensedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TreatmentQty = table.Column<int>(type: "int", nullable: false),
                    TreatmentCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PreAuthNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClinicalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitId = table.Column<int>(type: "int", nullable: false),
                    ConsultationId = table.Column<int>(type: "int", nullable: true),
                    BillItemId = table.Column<int>(type: "int", nullable: true),
                    SurgeryId = table.Column<int>(type: "int", nullable: true),
                    TherapyId = table.Column<int>(type: "int", nullable: true),
                    MedicalDeptId = table.Column<int>(type: "int", nullable: true),
                    WardId = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    RoomId = table.Column<int>(type: "int", nullable: true),
                    TreatmentStatusId = table.Column<int>(type: "int", nullable: false),
                    TreatmentTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Treatments_BillItems_BillItemId",
                        column: x => x.BillItemId,
                        principalTable: "BillItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Treatments_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Treatments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Treatments_MedicalDepartments_MedicalDeptId",
                        column: x => x.MedicalDeptId,
                        principalTable: "MedicalDepartments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Treatments_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Treatments_Surgeries_SurgeryId",
                        column: x => x.SurgeryId,
                        principalTable: "Surgeries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Treatments_Therapies_TherapyId",
                        column: x => x.TherapyId,
                        principalTable: "Therapies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Treatments_TreatmentStatuses_TreatmentStatusId",
                        column: x => x.TreatmentStatusId,
                        principalTable: "TreatmentStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Treatments_TreatmentTypes_TreatmentTypeId",
                        column: x => x.TreatmentTypeId,
                        principalTable: "TreatmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Treatments_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Treatments_Wards_WardId",
                        column: x => x.WardId,
                        principalTable: "Wards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LabTestResultCompositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestComposiiton = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LabTestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTestResultCompositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabTestResultCompositions_LabTests_LabTestId",
                        column: x => x.LabTestId,
                        principalTable: "LabTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiseaseTreatments",
                columns: table => new
                {
                    DiseaseTreatmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiseaseId = table.Column<int>(type: "int", nullable: false),
                    TreatmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseTreatments", x => x.DiseaseTreatmentId);
                    table.ForeignKey(
                        name: "FK_DiseaseTreatments_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiseaseTreatments_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicineTreatments",
                columns: table => new
                {
                    MedicineTreatmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    TreatmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineTreatments", x => x.MedicineTreatmentId);
                    table.ForeignKey(
                        name: "FK_MedicineTreatments_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineTreatments_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BillTypes",
                columns: new[] { "Id", "TypeNme" },
                values: new object[,]
                {
                    { 1, "Final" },
                    { 2, "Interim" },
                    { 3, "Advance" }
                });

            migrationBuilder.InsertData(
                table: "DiseaseTypes",
                columns: new[] { "Id", "DiseaseTypeCode", "DiseaseTypeName" },
                values: new object[,]
                {
                    { 1, "001", "Infectious" },
                    { 2, "002", "Non-Infectious" },
                    { 3, "003", "Genetic" },
                    { 4, "004", "Deficiency" },
                    { 5, "005", "Physiological" },
                    { 6, "006", "Autoimmune" },
                    { 7, "007", "Degenerative" },
                    { 8, "008", "Neoplastic" },
                    { 9, "009", "Psychiatric" },
                    { 10, "010", "Metabolic" },
                    { 11, "011", "Congenital" },
                    { 12, "012", "Allergic / Immunological" }
                });

            migrationBuilder.InsertData(
                table: "DoctorStatuses",
                columns: new[] { "Id", "DoctorStatusName" },
                values: new object[,]
                {
                    { 1, "Free" },
                    { 2, "Busy" },
                    { 3, "WithPatients" }
                });

            migrationBuilder.InsertData(
                table: "DoctorTypes",
                columns: new[] { "Id", "DeptConsultationFee", "DoctorTypeCode", "DoctorTypeName", "MedicalDepartmentId", "MedicalDeptId" },
                values: new object[,]
                {
                    { 1, 10000m, "001", "General Practitioner (MD)", null, 1 },
                    { 2, 30000m, "002", "Gynecologist", null, 2 },
                    { 3, 50000m, "003", "Cardiologist", null, 7 },
                    { 4, 50000m, "004", "Neurologist", null, 9 },
                    { 5, 50000m, "005", "Oncologist", null, 10 },
                    { 6, 10000m, "006", "Orthopedic Surgeon", null, 8 },
                    { 7, 50000m, "007", "Emergency Physician", null, 3 },
                    { 8, 10000m, "008", "Radiologist", null, 4 },
                    { 9, 10000m, "009", "Pediatrician", null, 5 },
                    { 10, 15000m, "010", "Intensivist", null, 6 },
                    { 11, 25000m, "011", "Dermatologist", null, 11 },
                    { 12, 25000m, "012", "Psychiatrist", null, 12 },
                    { 13, 50000m, "013", "Gastroenterologist", null, 13 },
                    { 14, 10000m, "014", "Ophthalmologist", null, 14 },
                    { 15, 10000m, "015", "General Surgeon", null, 15 }
                });

            migrationBuilder.InsertData(
                table: "Insuarances",
                columns: new[] { "Id", "InsuaranceName", "InsuranceNumber", "IntegrationType", "IsActive", "PortalUrl", "PrivillageCodeNumber", "RequiresPreAuth" },
                values: new object[,]
                {
                    { 1, "NHIF", "", 1, true, null, 1, true },
                    { 2, "ZHIF", "", 1, true, null, 2, true },
                    { 3, "Jubilee", "", 2, true, null, 3, false },
                    { 4, "Strategis", "", 2, true, null, 4, false },
                    { 5, "AAR", "", 2, true, null, 5, false },
                    { 6, "Britam", "", 2, true, null, 6, false },
                    { 7, "NSSF", "", 3, true, null, 7, true },
                    { 8, "Other", "", 3, true, null, 8, false }
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

            migrationBuilder.InsertData(
                table: "MedicineGenres",
                columns: new[] { "Id", "MedicineGenreCode", "MedicineGenreName" },
                values: new object[,]
                {
                    { 1, 1, "Painkillers" },
                    { 2, 2, "Antibiotics" },
                    { 3, 3, "Antifungals" },
                    { 4, 4, "Antivirals" },
                    { 5, 5, "Vaccines" },
                    { 6, 6, "Anthelmintics" },
                    { 7, 7, "Antimalarials " }
                });

            migrationBuilder.InsertData(
                table: "MedicineStatuses",
                columns: new[] { "Id", "MedicineStatusName" },
                values: new object[,]
                {
                    { 1, "InStock" },
                    { 2, "OutOfStock" }
                });

            migrationBuilder.InsertData(
                table: "MedicineTypes",
                columns: new[] { "Id", "MedicineGenreId", "MedicineTypeCode", "MedicineTypeName" },
                values: new object[,]
                {
                    { 1, null, 1, "Tablets" },
                    { 2, null, 2, "Injection" },
                    { 3, null, 3, "Syrup" },
                    { 4, null, 4, "Drop" },
                    { 5, null, 5, "Cream" },
                    { 6, null, 6, "Drip" }
                });

            migrationBuilder.InsertData(
                table: "PatientStatuses",
                columns: new[] { "Id", "PatientStatusName" },
                values: new object[,]
                {
                    { 1, "Emergency" },
                    { 2, "Not Emergency" }
                });

            migrationBuilder.InsertData(
                table: "PatientTypes",
                columns: new[] { "Id", "PatientTypeCode", "PatientTypeName" },
                values: new object[,]
                {
                    { 1, "001", "OutSide" },
                    { 2, "002", "Hospitalized" },
                    { 3, "003", "Clinic" }
                });

            migrationBuilder.InsertData(
                table: "PaymentModes",
                columns: new[] { "Id", "PaymentModeName" },
                values: new object[,]
                {
                    { 1, "Cash" },
                    { 2, "Insurance" }
                });

            migrationBuilder.InsertData(
                table: "PaymentStatus",
                columns: new[] { "Id", "PaymentStatusName" },
                values: new object[,]
                {
                    { 1, "Unpaid" },
                    { 2, "Partial Payment" },
                    { 3, "Paid" },
                    { 4, "Insurance Review" }
                });

            migrationBuilder.InsertData(
                table: "RoomStatus",
                columns: new[] { "Id", "RoomStatusName" },
                values: new object[,]
                {
                    { 1, "InUse" },
                    { 2, "Available" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "RoomTypeCode", "RoomTypeName" },
                values: new object[,]
                {
                    { 1, "001", "OR" },
                    { 2, "002", "Consultation Room" },
                    { 3, "003", "Lab" },
                    { 4, "004", "Therapy Room" },
                    { 5, "005", "Radiology" }
                });

            migrationBuilder.InsertData(
                table: "StaffStatuses",
                columns: new[] { "Id", "StaffStatusName" },
                values: new object[,]
                {
                    { 1, "Available" },
                    { 2, "OnLeave" }
                });

            migrationBuilder.InsertData(
                table: "StaffTitleGroups",
                columns: new[] { "Id", "GroupName", "RegisteredDate" },
                values: new object[,]
                {
                    { 1, "Leaders", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Doctors", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Nurses", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Administrators", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Helpers", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "StaffTypes",
                columns: new[] { "Id", "RegisteredDate", "StaffTypeCode", "StaffTypeName" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "001", "Administrative" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "002", "Clinical" }
                });

            migrationBuilder.InsertData(
                table: "TreatmentStatuses",
                columns: new[] { "Id", "TreatmentStatusName" },
                values: new object[,]
                {
                    { 1, "Requested" },
                    { 2, "OnGoing" },
                    { 3, "Completed" }
                });

            migrationBuilder.InsertData(
                table: "TreatmentTypes",
                columns: new[] { "Id", "HasDoctorId", "HasMedicalDeptId", "HasRoomId", "HasSurgeryId", "HasTherapyId", "HasWardId", "TreatmentTypeCode", "TreatmentTypeName" },
                values: new object[,]
                {
                    { 1, true, true, true, true, false, false, "001", "Surgery" },
                    { 2, true, true, true, false, true, false, "002", "Therapy" },
                    { 3, true, true, false, false, false, true, "003", "Hospitalized" }
                });

            migrationBuilder.InsertData(
                table: "VisitStatuses",
                columns: new[] { "Id", "VisitStatusName" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Completed" },
                    { 3, "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "VisitTypes",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { 1, "Consultation" },
                    { 2, "Check-Up" },
                    { 3, "Treatment" },
                    { 4, "Pharmacy" }
                });

            migrationBuilder.InsertData(
                table: "InsuranceSchemes",
                columns: new[] { "Id", "AnnualLimit", "DefaultCopayPercent", "DefaultCoveragePercent", "InsuaranceId", "IsActive", "PerVisitLimit", "SchemeCode", "SchemeName" },
                values: new object[,]
                {
                    { 1, null, 0m, 100m, 1, true, null, "NHIF-STD", "NHIF Standard" },
                    { 2, null, 0m, 100m, 1, true, null, "NHIF-TOTO", "NHIF Toto Afya" },
                    { 3, null, 0m, 100m, 2, true, null, "ZHIF-STD", "ZHIF Standard" },
                    { 4, null, 0m, 100m, 3, true, null, "JUB-100", "Jubilee Corporate Full" },
                    { 5, null, 20m, 80m, 3, true, null, "JUB-80", "Jubilee 80/20 Copay" },
                    { 6, null, 0m, 100m, 4, true, null, "STR-EXEC", "Strategis Executive" },
                    { 7, null, 0m, 100m, 5, true, null, "AAR-STD", "AAR Standard" },
                    { 8, null, 0m, 100m, 6, true, null, "BRT-CORP", "Britam Corporate" }
                });

            migrationBuilder.InsertData(
                table: "StaffTitles",
                columns: new[] { "Id", "RegisteredDate", "StaffTitleName", "StaffTypeId", "TitleGroupId" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doctor", 2, 2 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nurse", 2, 3 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Director", 1, 1 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HOD", 1, 1 },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Help Desk", 1, 5 },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maintenance Office", 1, 5 },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pharmacist", 2, 5 },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System Admin", 1, 4 }
                });

            migrationBuilder.InsertData(
                table: "Surgeries",
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
                table: "Wards",
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserPasskeys_UserId",
                table: "AspNetUserPasskeys",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StaffStatusId",
                table: "AspNetUsers",
                column: "StaffStatusId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Beds_WardId",
                table: "Beds",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_BillId",
                table: "BillItems",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BillTypeId",
                table: "Bills",
                column: "BillTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PatientInsuaranceId",
                table: "Bills",
                column: "PatientInsuaranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PaymentModeId",
                table: "Bills",
                column: "PaymentModeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PaymentStatusId",
                table: "Bills",
                column: "PaymentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_StaffId",
                table: "Bills",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_VisitId",
                table: "Bills",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_BillItemId",
                table: "Consultations",
                column: "BillItemId",
                unique: true,
                filter: "[BillItemId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_DoctorId",
                table: "Consultations",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_MedicalDepartmentId",
                table: "Consultations",
                column: "MedicalDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_RoomId",
                table: "Consultations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_VisitId",
                table: "Consultations",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_DiseaseTypeId",
                table: "Diseases",
                column: "DiseaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DiseaseTreatments_DiseaseId",
                table: "DiseaseTreatments",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_DiseaseTreatments_TreatmentId",
                table: "DiseaseTreatments",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorCodes_DocCode",
                table: "DoctorCodes",
                column: "DocCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorCodes_DoctorId",
                table: "DoctorCodes",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPatients_DoctorId",
                table: "DoctorPatients",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPatients_PatientId",
                table: "DoctorPatients",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DoctorStatusId",
                table: "Doctors",
                column: "DoctorStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DoctorTypeId",
                table: "Doctors",
                column: "DoctorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_RoomId",
                table: "Doctors",
                column: "RoomId",
                unique: true,
                filter: "[RoomId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_StaffId",
                table: "Doctors",
                column: "StaffId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorTypes_MedicalDepartmentId",
                table: "DoctorTypes",
                column: "MedicalDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeIds_EmployeeIdCode",
                table: "EmployeeIds",
                column: "EmployeeIdCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeIds_StaffId",
                table: "EmployeeIds",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceClaims_BillId",
                table: "InsuranceClaims",
                column: "BillId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceClaims_InsuaranceId",
                table: "InsuranceClaims",
                column: "InsuaranceId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceClaims_PatientInsuaranceId",
                table: "InsuranceClaims",
                column: "PatientInsuaranceId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceClaims_SubmittedByStaffId",
                table: "InsuranceClaims",
                column: "SubmittedByStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceSchemes_InsuaranceId",
                table: "InsuranceSchemes",
                column: "InsuaranceId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurerTariffs_InsuaranceId",
                table: "InsurerTariffs",
                column: "InsuaranceId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurerTariffs_InsuranceSchemeId",
                table: "InsurerTariffs",
                column: "InsuranceSchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_LabTestResultCompositions_LabTestId",
                table: "LabTestResultCompositions",
                column: "LabTestId");

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
                name: "IX_LabTests_VisitId",
                table: "LabTests",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineMedicineTypes_MedicineId",
                table: "MedicineMedicineTypes",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineMedicineTypes_MedicineTypeId",
                table: "MedicineMedicineTypes",
                column: "MedicineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_MedicineStatusId",
                table: "Medicines",
                column: "MedicineStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_MedicineTypeId",
                table: "Medicines",
                column: "MedicineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineTreatments_MedicineId",
                table: "MedicineTreatments",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineTreatments_TreatmentId",
                table: "MedicineTreatments",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineTypes_MedicineGenreId",
                table: "MedicineTypes",
                column: "MedicineGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientCodes_PatientId",
                table: "PatientCodes",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiseases_DiseaseId",
                table: "PatientDiseases",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiseases_PatientId",
                table: "PatientDiseases",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientInsuarances_InsuaranceId",
                table: "PatientInsuarances",
                column: "InsuaranceId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientInsuarances_InsuranceSchemeId",
                table: "PatientInsuarances",
                column: "InsuranceSchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientInsuarances_PatientId",
                table: "PatientInsuarances",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientStatusId",
                table: "Patients",
                column: "PatientStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientTypeId",
                table: "Patients",
                column: "PatientTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BillId",
                table: "Payments",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InsuranceClaimId",
                table: "Payments",
                column: "InsuranceClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentStatusId",
                table: "Payments",
                column: "PaymentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PreliminaryTests_VisitId",
                table: "PreliminaryTests",
                column: "VisitId",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomStatusId",
                table: "Rooms",
                column: "RoomStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffStaffTitle_StaffsId",
                table: "StaffStaffTitle",
                column: "StaffsId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffTitles_StaffTypeId",
                table: "StaffTitles",
                column: "StaffTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffTitles_TitleGroupId",
                table: "StaffTitles",
                column: "TitleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Surgeries_MedicalDepartmentId",
                table: "Surgeries",
                column: "MedicalDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Therapies_MedicalDepartmentId",
                table: "Therapies",
                column: "MedicalDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_BillItemId",
                table: "Treatments",
                column: "BillItemId",
                unique: true,
                filter: "[BillItemId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_ConsultationId",
                table: "Treatments",
                column: "ConsultationId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_DoctorId",
                table: "Treatments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_MedicalDeptId",
                table: "Treatments",
                column: "MedicalDeptId");

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
                name: "IX_Treatments_TreatmentStatusId",
                table: "Treatments",
                column: "TreatmentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_TreatmentTypeId",
                table: "Treatments",
                column: "TreatmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_VisitId",
                table: "Treatments",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_WardId",
                table: "Treatments",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitInsuranceAuths_PatientInsuaranceId",
                table: "VisitInsuranceAuths",
                column: "PatientInsuaranceId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitInsuranceAuths_VisitId",
                table: "VisitInsuranceAuths",
                column: "VisitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visits_PatientId",
                table: "Visits",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_PatientInsuaranceId",
                table: "Visits",
                column: "PatientInsuaranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_PaymentModeId",
                table: "Visits",
                column: "PaymentModeId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_VisitStatusId",
                table: "Visits",
                column: "VisitStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_VisitTypeId",
                table: "Visits",
                column: "VisitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_MedicalDepartmentId",
                table: "Wards",
                column: "MedicalDepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserPasskeys");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Beds");

            migrationBuilder.DropTable(
                name: "DiseaseTreatments");

            migrationBuilder.DropTable(
                name: "DoctorCodes");

            migrationBuilder.DropTable(
                name: "DoctorPatients");

            migrationBuilder.DropTable(
                name: "EmployeeIds");

            migrationBuilder.DropTable(
                name: "InsurerTariffs");

            migrationBuilder.DropTable(
                name: "LabTestResultCompositions");

            migrationBuilder.DropTable(
                name: "MedicineMedicineTypes");

            migrationBuilder.DropTable(
                name: "MedicineTreatments");

            migrationBuilder.DropTable(
                name: "PatientCodes");

            migrationBuilder.DropTable(
                name: "PatientDiseases");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PreliminaryTests");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "StaffStaffTitle");

            migrationBuilder.DropTable(
                name: "VisitInsuranceAuths");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "LabTests");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "InsuranceClaims");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "StaffTitles");

            migrationBuilder.DropTable(
                name: "Consultations");

            migrationBuilder.DropTable(
                name: "Surgeries");

            migrationBuilder.DropTable(
                name: "Therapies");

            migrationBuilder.DropTable(
                name: "TreatmentStatuses");

            migrationBuilder.DropTable(
                name: "TreatmentTypes");

            migrationBuilder.DropTable(
                name: "Wards");

            migrationBuilder.DropTable(
                name: "DiseaseTypes");

            migrationBuilder.DropTable(
                name: "MedicineStatuses");

            migrationBuilder.DropTable(
                name: "MedicineTypes");

            migrationBuilder.DropTable(
                name: "StaffTitleGroups");

            migrationBuilder.DropTable(
                name: "StaffTypes");

            migrationBuilder.DropTable(
                name: "BillItems");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "MedicineGenres");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "DoctorStatuses");

            migrationBuilder.DropTable(
                name: "DoctorTypes");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BillTypes");

            migrationBuilder.DropTable(
                name: "PaymentStatus");

            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "MedicalDepartments");

            migrationBuilder.DropTable(
                name: "RoomStatus");

            migrationBuilder.DropTable(
                name: "RoomTypes");

            migrationBuilder.DropTable(
                name: "StaffStatuses");

            migrationBuilder.DropTable(
                name: "PatientInsuarances");

            migrationBuilder.DropTable(
                name: "PaymentModes");

            migrationBuilder.DropTable(
                name: "VisitStatuses");

            migrationBuilder.DropTable(
                name: "VisitTypes");

            migrationBuilder.DropTable(
                name: "InsuranceSchemes");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Insuarances");

            migrationBuilder.DropTable(
                name: "PatientStatuses");

            migrationBuilder.DropTable(
                name: "PatientTypes");

            migrationBuilder.DropSequence(
                name: "DoctorCodeSequence");

            migrationBuilder.DropSequence(
                name: "EmployeeIdCodeSequence");

            migrationBuilder.DropSequence(
                name: "PatientCodeSequence");
        }
    }
}
