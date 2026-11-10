using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NovaCare.Modals.DiseaseModals;
using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.JoinTables;
using NovaCare.Modals.LabTestModals;
using NovaCare.Modals.MedicineModals;
using NovaCare.Modals.PatientModals;
using NovaCare.Modals.PaymentModals;
using NovaCare.Modals.RoomModals;
using NovaCare.Modals.StaffModals;
using NovaCare.Modals.TreatmentModals;

namespace NovaCare.Data
{
    //public class NovaCareDbContext : IdentityDbContext<Staff, IdentityRole, string>
    //{
    //    public NovaCareDbContext(DbContextOptions<NovaCareDbContext> options) : base(options)
    //    { }

    //}

    public class NovaCareDbContext(DbContextOptions<NovaCareDbContext> options) : IdentityDbContext<Staff>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(NovaCareDbContext).Assembly);

            #region[PreDefinedData]
            builder.Entity<TreatmentType>().HasData(
                new TreatmentType { Id = 1, TreatmentTypeCode = "001", TreatmentTypeName = "Medication", HasMedicalDeptId = false, HasMedicineId = true, HasSurgeryId = false, HasTherapyId = false, HasWardId = false, HasDoctorId = false, HasRoomId = false },
                new TreatmentType { Id = 2, TreatmentTypeCode = "002", TreatmentTypeName = "Surgery", HasMedicalDeptId = true, HasMedicineId = false, HasSurgeryId = true, HasTherapyId = false, HasWardId = false, HasDoctorId = true, HasRoomId = true },
                new TreatmentType { Id = 3, TreatmentTypeCode = "003", TreatmentTypeName = "Therapy", HasMedicalDeptId = true, HasMedicineId = false, HasSurgeryId = false, HasTherapyId = true, HasWardId = false, HasDoctorId = true, HasRoomId = true },
                new TreatmentType { Id = 4, TreatmentTypeCode = "004", TreatmentTypeName = "Hospitalized", HasMedicalDeptId = true, HasMedicineId = false, HasSurgeryId = false, HasTherapyId = false, HasWardId = true, HasDoctorId = true, HasRoomId = false }
                );

            builder.Entity<TreatmentStatus>().HasData(
                new TreatmentStatus { Id = 1, TreatmentStatusName = "Requested" },
                new TreatmentStatus { Id = 2, TreatmentStatusName = "OnGoing" },
                new TreatmentStatus { Id = 3, TreatmentStatusName = "Completed" }
                );

            builder.Entity<StaffType>().HasData(
                new StaffType { Id = 1, StaffTypeCode = "001", StaffTypeName = "Administrative" },
                new StaffType { Id = 2, StaffTypeCode = "002", StaffTypeName = "Clinical" }
                );

            builder.Entity<StaffTitleGroup>().HasData(
                new StaffTitleGroup { Id = 1, GroupName = "Leaders" },
                new StaffTitleGroup { Id = 2, GroupName = "Doctors" },
                new StaffTitleGroup { Id = 3, GroupName = "Nurses" },
                new StaffTitleGroup { Id = 4, GroupName = "Administrators" },
                new StaffTitleGroup { Id = 5, GroupName = "Helpers" }
                );

            builder.Entity<StaffTitle>().HasData(
                new StaffTitle { Id = 1, StaffTypeId = 2, StaffTitleName = "Doctor", TitleGroupId = 2 },
                new StaffTitle { Id = 2, StaffTypeId = 2, StaffTitleName = "Nurse", TitleGroupId = 3 },
                new StaffTitle { Id = 3, StaffTypeId = 1, StaffTitleName = "Director", TitleGroupId = 1 },
                new StaffTitle { Id = 4, StaffTypeId = 1, StaffTitleName = "HOD", TitleGroupId = 1 },
                new StaffTitle { Id = 5, StaffTypeId = 1, StaffTitleName = "Help Desk", TitleGroupId = 5 },
                new StaffTitle { Id = 6, StaffTypeId = 1, StaffTitleName = "Maintenance Office", TitleGroupId = 5 },
                new StaffTitle { Id = 7, StaffTypeId = 2, StaffTitleName = "Pharmacist", TitleGroupId = 5 },
                new StaffTitle { Id = 8, StaffTypeId = 1, StaffTitleName = "System Admin", TitleGroupId = 4 }
                );

            builder.Entity<StaffStatus>().HasData(
                new StaffStatus { Id = 1, StaffStatusName = "Available" },
                new StaffStatus { Id = 2, StaffStatusName = "OnLeave" }
                );

            builder.Entity<RoomType>().HasData(
                new RoomType { Id = 1, RoomTypeCode = "001", RoomTypeName = "OR" },
                new RoomType { Id = 2, RoomTypeCode = "002", RoomTypeName = "Consultation Room" },
                new RoomType { Id = 3, RoomTypeCode = "003", RoomTypeName = "Lab" },
                new RoomType { Id = 4, RoomTypeCode = "004", RoomTypeName = "Therapy Room" },
                new RoomType { Id = 5, RoomTypeCode = "005", RoomTypeName = "Radiology" }
                );

            builder.Entity<RoomStatus>().HasData(
                new RoomStatus { Id = 1, RoomStatusName = "InUse" },
                new RoomStatus { Id = 2, RoomStatusName = "Available" }
                );

            builder.Entity<PaymentStatus>().HasData(
                new PaymentStatus { Id = 1, PaymentStatusName = "Full Payment" },
                new PaymentStatus { Id = 2, PaymentStatusName = "Partial Payment" }
                );

            builder.Entity<PaymentMode>().HasData(
                new PaymentMode { Id = 1, PaymentModeName = "Cash" },
                new PaymentMode { Id = 2, PaymentModeName = "Insurance" }
                );

            builder.Entity<Insuarance>().HasData(
                new Insuarance { Id = 1, PrivillageCodeNumber = 1, InsuaranceName = "NHIF" },
                new Insuarance { Id = 2, PrivillageCodeNumber = 2, InsuaranceName = "ZHIF" },
                new Insuarance { Id = 3, PrivillageCodeNumber = 3, InsuaranceName = "Jubelee" },
                new Insuarance { Id = 4, PrivillageCodeNumber = 4, InsuaranceName = "NSSF" },
                new Insuarance { Id = 5, PrivillageCodeNumber = 5, InsuaranceName = "Other" }
                );

            builder.Entity<PatientType>().HasData(
                new PatientType { Id = 1, PatientTypeCode = "001", PatientTypeName = "OutSide" },
                new PatientType { Id = 2, PatientTypeCode = "002", PatientTypeName = "Hospitalized" },
                new PatientType { Id = 3, PatientTypeCode = "003", PatientTypeName = "Clinic" }
                );

            builder.Entity<PatientStatus>().HasData(
                new PatientStatus { Id = 1, PatientStatusName = "Emergency" },
                new PatientStatus { Id = 2, PatientStatusName = "Not Emergency" }
                );

            builder.Entity<MedicineType>().HasData(
                new MedicineType { Id = 1, MedicineTypeCode = 1, MedicineTypeName = "Tablets" },
                new MedicineType { Id = 2, MedicineTypeCode = 2, MedicineTypeName = "Injection" },
                new MedicineType { Id = 3, MedicineTypeCode = 3, MedicineTypeName = "Syrup" },
                new MedicineType { Id = 4, MedicineTypeCode = 4, MedicineTypeName = "Drop" },
                new MedicineType { Id = 5, MedicineTypeCode = 5, MedicineTypeName = "Cream" },
                new MedicineType { Id = 6, MedicineTypeCode = 6, MedicineTypeName = "Drip" }
                );

            builder.Entity<MedicineStatus>().HasData(
                    new MedicineStatus { Id = 1, MedicineStatusName = "InStock" },
                    new MedicineStatus { Id = 2, MedicineStatusName = "OutOfStock" }
                );

            builder.Entity<DoctorType>().HasData(
                new DoctorType { Id = 1, DoctorTypeCode = "001", DoctorTypeName = "General Practitioner (MD)", MedicalDeptId = 1, DeptConsultationFee = 10000 }, // OPD
                new DoctorType { Id = 2, DoctorTypeCode = "002", DoctorTypeName = "Gynecologist", MedicalDeptId = 2, DeptConsultationFee = 30000 },         // OB-GYN
                new DoctorType { Id = 3, DoctorTypeCode = "003", DoctorTypeName = "Cardiologist", MedicalDeptId = 7, DeptConsultationFee = 50000 },         // Cardiology
                new DoctorType { Id = 4, DoctorTypeCode = "004", DoctorTypeName = "Neurologist", MedicalDeptId = 9, DeptConsultationFee = 50000 },          // Neurology
                new DoctorType { Id = 5, DoctorTypeCode = "005", DoctorTypeName = "Oncologist", MedicalDeptId = 10, DeptConsultationFee = 50000 },          // Oncology
                new DoctorType { Id = 6, DoctorTypeCode = "006", DoctorTypeName = "Orthopedic Surgeon", MedicalDeptId = 8, DeptConsultationFee = 10000 },   // Orthopedics
                new DoctorType { Id = 7, DoctorTypeCode = "007", DoctorTypeName = "Emergency Physician", MedicalDeptId = 3, DeptConsultationFee = 50000 },  // ER
                new DoctorType { Id = 8, DoctorTypeCode = "008", DoctorTypeName = "Radiologist", MedicalDeptId = 4, DeptConsultationFee = 10000 },          // Radiology
                new DoctorType { Id = 9, DoctorTypeCode = "009", DoctorTypeName = "Pediatrician", MedicalDeptId = 5, DeptConsultationFee = 10000 },         // Pediatrics
                new DoctorType { Id = 10, DoctorTypeCode = "010", DoctorTypeName = "Intensivist", MedicalDeptId = 6, DeptConsultationFee = 15000 },         // ICU
                new DoctorType { Id = 11, DoctorTypeCode = "011", DoctorTypeName = "Dermatologist", MedicalDeptId = 11, DeptConsultationFee = 25000 },     // Dermatology
                new DoctorType { Id = 12, DoctorTypeCode = "012", DoctorTypeName = "Psychiatrist", MedicalDeptId = 12, DeptConsultationFee = 25000 },       // Psychiatry
                new DoctorType { Id = 13, DoctorTypeCode = "013", DoctorTypeName = "Gastroenterologist", MedicalDeptId = 13, DeptConsultationFee = 50000 }, // Gastroenterology
                new DoctorType { Id = 14, DoctorTypeCode = "014", DoctorTypeName = "Ophthalmologist", MedicalDeptId = 14, DeptConsultationFee = 10000 },   // Ophthalmology
                new DoctorType { Id = 15, DoctorTypeCode = "015", DoctorTypeName = "General Surgeon", MedicalDeptId = 15, DeptConsultationFee = 10000 }    // General Surgery
            );


            builder.Entity<DoctorStatus>().HasData(
                new DoctorStatus { Id = 1, DoctorStatusName = "Free" },
                new DoctorStatus { Id = 2, DoctorStatusName = "Busy" },
                new DoctorStatus { Id = 3, DoctorStatusName = "WithPatients" }
                );

            builder.Entity<DiseaseType>().HasData(
                new DiseaseType { Id = 1, DiseaseTypeCode = "001", DiseaseTypeName = "Infectious" },
                new DiseaseType { Id = 2, DiseaseTypeCode = "002", DiseaseTypeName = "Non-Infectious" },
                new DiseaseType { Id = 3, DiseaseTypeCode = "003", DiseaseTypeName = "Genetic" },
                new DiseaseType { Id = 4, DiseaseTypeCode = "004", DiseaseTypeName = "Deficiency" },
                new DiseaseType { Id = 5, DiseaseTypeCode = "005", DiseaseTypeName = "Physiological" },
                new DiseaseType { Id = 6, DiseaseTypeCode = "006", DiseaseTypeName = "Autoimmune" },
                new DiseaseType { Id = 7, DiseaseTypeCode = "007", DiseaseTypeName = "Degenerative" },
                new DiseaseType { Id = 8, DiseaseTypeCode = "008", DiseaseTypeName = "Neoplastic" }, // Cancerous/Tumor-related
                new DiseaseType { Id = 9, DiseaseTypeCode = "009", DiseaseTypeName = "Psychiatric" },
                new DiseaseType { Id = 10, DiseaseTypeCode = "010", DiseaseTypeName = "Metabolic" },
                new DiseaseType { Id = 11, DiseaseTypeCode = "011", DiseaseTypeName = "Congenital" }, // Present from birth (distinct from purely genetic inherited traits)
                new DiseaseType { Id = 12, DiseaseTypeCode = "012", DiseaseTypeName = "Allergic / Immunological" }
            );


            builder.Entity<MedicineGenre>().HasData(
                new MedicineGenre { Id = 1, MedicineGenreCode = 1, MedicineGenreName = "Painkillers" },
                new MedicineGenre { Id = 2, MedicineGenreCode = 2, MedicineGenreName = "Antibiotics" },
                new MedicineGenre { Id = 3, MedicineGenreCode = 3, MedicineGenreName = "Antifungals" },
                new MedicineGenre { Id = 4, MedicineGenreCode = 4, MedicineGenreName = "Antivirals" },
                new MedicineGenre { Id = 5, MedicineGenreCode = 5, MedicineGenreName = "Vaccines" },
                new MedicineGenre { Id = 6, MedicineGenreCode = 6, MedicineGenreName = "Anthelmintics" },
                new MedicineGenre { Id = 7, MedicineGenreCode = 7, MedicineGenreName = "Antimalarials " }
                );

            builder.Entity<BillType>().HasData(
                new BillType { Id = 1, TypeNme = "Final" },
                new BillType { Id = 2, TypeNme = "Interim" },
                new BillType { Id = 3, TypeNme = "Advance" }
                );

            builder.Entity<VisitType>().HasData(
                new VisitType { Id = 1, TypeNme = "Consultation" },
                new VisitType { Id = 2, TypeNme = "Check-Up" },
                new VisitType { Id = 3, TypeNme = "Treatment" }
                );


            builder.Entity<Therapy>().HasData(
                new Therapy { Id = 1, TherapyName = "Dialysis", TherapyCost = 30000, MedicalDepartmentId = 1 },          // OPD / Outpatient Medical
                new Therapy { Id = 2, TherapyName = "Radiotherapy", TherapyCost = 90000, MedicalDepartmentId = 10 },     // Oncology (Corrected from Id 2)
                new Therapy { Id = 3, TherapyName = "Physical Therapy", TherapyCost = 20000, MedicalDepartmentId = 8 },  // Orthopedics (Corrected from Id 4)
                new Therapy { Id = 4, TherapyName = "Chemotherapy", TherapyCost = 70000, MedicalDepartmentId = 10 },     // Oncology (Corrected from Id 3)
                new Therapy { Id = 5, TherapyName = "Cognitive Behavioral Therapy", TherapyCost = 15000, MedicalDepartmentId = 12 }, // Psychiatry
                new Therapy { Id = 6, TherapyName = "Occupational Therapy", TherapyCost = 22000, MedicalDepartmentId = 8 }, // Orthopedics / Rehab
                new Therapy { Id = 7, TherapyName = "Speech Therapy", TherapyCost = 20000, MedicalDepartmentId = 9 },       // Neurology / Pediatrics
                new Therapy { Id = 8, TherapyName = "Cardiac Rehabilitation", TherapyCost = 25000, MedicalDepartmentId = 7 }, // Cardiology
                new Therapy { Id = 9, TherapyName = "Respiratory Therapy", TherapyCost = 35000, MedicalDepartmentId = 6 },    // ICU / Pulmonology
                new Therapy { Id = 10, TherapyName = "Phototherapy (UV Light)", TherapyCost = 18000, MedicalDepartmentId = 11 }, // Dermatology
                new Therapy { Id = 11, TherapyName = "Hydrotherapy", TherapyCost = 28000, MedicalDepartmentId = 8 },        // Orthopedics
                new Therapy { Id = 12, TherapyName = "Electroconvulsive Therapy (ECT)", TherapyCost = 65000, MedicalDepartmentId = 12 } // Psychiatry
            );


            builder.Entity<MedicalDepartment>().HasData(
                new MedicalDepartment { Id = 1, MedicalDepartmentName = "OPD" },
                new MedicalDepartment { Id = 2, MedicalDepartmentName = "OB-GYN" },
                new MedicalDepartment { Id = 3, MedicalDepartmentName = "ER" },
                new MedicalDepartment { Id = 4, MedicalDepartmentName = "Radiology" },
                new MedicalDepartment { Id = 5, MedicalDepartmentName = "Pediatrics" },
                new MedicalDepartment { Id = 6, MedicalDepartmentName = "ICU" },
                new MedicalDepartment { Id = 7, MedicalDepartmentName = "Cardiology" },
                new MedicalDepartment { Id = 8, MedicalDepartmentName = "Orthopedics" },
                new MedicalDepartment { Id = 9, MedicalDepartmentName = "Neurology" },
                new MedicalDepartment { Id = 10, MedicalDepartmentName = "Oncology" },
                new MedicalDepartment { Id = 11, MedicalDepartmentName = "Dermatology" },
                new MedicalDepartment { Id = 12, MedicalDepartmentName = "Psychiatry" },
                new MedicalDepartment { Id = 13, MedicalDepartmentName = "Gastroenterology" },
                new MedicalDepartment { Id = 14, MedicalDepartmentName = "Ophthalmology" },
                new MedicalDepartment { Id = 15, MedicalDepartmentName = "General Surgery" }
            );

            builder.Entity<Surgery>().HasData(
                new Surgery { Id = 1, SurgeryName = "Heart Transplant", SurgeryCost = 430000, MedicalDepartmentId = 7 },      // Cardiology
                new Surgery { Id = 2, SurgeryName = "Coronary Artery Bypass Graft (CABG)", SurgeryCost = 150000, MedicalDepartmentId = 7 }, // Cardiology
                new Surgery { Id = 3, SurgeryName = "Cesarean Section (C-Section)", SurgeryCost = 25000, MedicalDepartmentId = 2 },       // OB-GYN
                new Surgery { Id = 4, SurgeryName = "Hysterectomy", SurgeryCost = 35000, MedicalDepartmentId = 2 },                       // OB-GYN
                new Surgery { Id = 5, SurgeryName = "Craniotomy (Brain Surgery)", SurgeryCost = 180000, MedicalDepartmentId = 9 },         // Neurology
                new Surgery { Id = 6, SurgeryName = "Spinal Fusion", SurgeryCost = 110000, MedicalDepartmentId = 9 },                     // Neurology / Orthopedics
                new Surgery { Id = 7, SurgeryName = "Total Knee Replacement", SurgeryCost = 45000, MedicalDepartmentId = 8 },              // Orthopedics
                new Surgery { Id = 8, SurgeryName = "Total Hip Replacement", SurgeryCost = 50000, MedicalDepartmentId = 8 },               // Orthopedics
                new Surgery { Id = 9, SurgeryName = "Tumor Resection Surgery", SurgeryCost = 95000, MedicalDepartmentId = 10 },            // Oncology
                new Surgery { Id = 10, SurgeryName = "Cataract Surgery", SurgeryCost = 12000, MedicalDepartmentId = 14 },                 // Ophthalmology
                new Surgery { Id = 11, SurgeryName = "Appendectomy", SurgeryCost = 18000, MedicalDepartmentId = 15 },                     // General Surgery
                new Surgery { Id = 12, SurgeryName = "Cholecystectomy (Gallbladder Removal)", SurgeryCost = 22000, MedicalDepartmentId = 15 }, // General Surgery
                new Surgery { Id = 13, SurgeryName = "Hernia Repair", SurgeryCost = 15000, MedicalDepartmentId = 15 }                     // General Surgery
            );

            builder.Entity<Ward>().HasData(
                new Ward { Id = 1, WardName = "Maternity Ward", WardCost = 20000, MedicalDepartmentId = 2 },       // OB-GYN
                new Ward { Id = 2, WardName = "General Pediatric Ward", WardCost = 15000, MedicalDepartmentId = 5 }, // Pediatrics
                new Ward { Id = 3, WardName = "Neonatal ICU (NICU)", WardCost = 65000, MedicalDepartmentId = 5 },    // Pediatrics / ICU
                new Ward { Id = 4, WardName = "Medical ICU", WardCost = 75000, MedicalDepartmentId = 6 },            // ICU
                new Ward { Id = 5, WardName = "Surgical ICU", WardCost = 80000, MedicalDepartmentId = 6 },           // ICU
                new Ward { Id = 6, WardName = "Cardiac Care Unit (CCU)", WardCost = 70000, MedicalDepartmentId = 7 },// Cardiology
                new Ward { Id = 7, WardName = "Cardiology Step-Down Ward", WardCost = 35000, MedicalDepartmentId = 7 },// Cardiology
                new Ward { Id = 8, WardName = "Orthopedic & Rehab Ward", WardCost = 25000, MedicalDepartmentId = 8 },  // Orthopedics
                new Ward { Id = 9, WardName = "Stroke & Neurology Ward", WardCost = 40000, MedicalDepartmentId = 9 },  // Neurology
                new Ward { Id = 10, WardName = "Oncology Ward", WardCost = 45000, MedicalDepartmentId = 10 },          // Oncology
                new Ward { Id = 11, WardName = "Psychiatric Inpatient Unit", WardCost = 30000, MedicalDepartmentId = 12 }, // Psychiatry
                new Ward { Id = 12, WardName = "Post-Surgical Recovery Ward", WardCost = 30000, MedicalDepartmentId = 15 } // General Surgery
            );

            builder.Entity<VisitStatus>().HasData(
                new VisitStatus { Id = 1, VisitStatusName = "Active" },    // Patient is currently in the hospital/clinic
                new VisitStatus { Id = 2, VisitStatusName = "Completed" }, // Discharged & bills finalized (corresponds to "Old")
                new VisitStatus { Id = 3, VisitStatusName = "Cancelled" }  // Visit created by mistake or patient left before triage
            );

            builder.HasSequence<int>("DoctorCodeSequence").StartsAt(1000).IncrementsBy(1);
            builder.Entity<DoctorCode>().Property(d => d.DocCode).UseSequence("DoctorCodeSequence");

            builder.HasSequence<int>("EmployeeIdCodeSequence").StartsAt(1000).IncrementsBy(1);
            builder.Entity<EmployeeId>().Property(d => d.EmployeeIdCode).UseSequence("EmployeeIdCodeSequence");

            builder.HasSequence<int>("PatientCodeSequence").StartsAt(1000).IncrementsBy(1);
            builder.Entity<PatientCode>().Property(d => d.PatientCodeNumber).UseSequence("PatientCodeSequence");

            #endregion
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }

        #region[DbSets]
        //Disease Modals DBSets
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<DiseaseType> DiseaseTypes { get; set; }

        //Doctor Modals DBSets
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorStatus> DoctorStatuses { get; set; }
        public DbSet<DoctorType> DoctorTypes { get; set; }

        //Join Tables DBSets
        public DbSet<DiseaseTreatment> DiseaseTreatments { get; set; }
        public DbSet<DoctorPatient> DoctorPatients { get; set; }
        public DbSet<MedicineMedicineType> MedicineMedicineTypes { get; set; }
        public DbSet<MedicineTreatment> MedicineTreatments { get; set; }
        public DbSet<PatientDisease> PatientDiseases { get; set; }
        public DbSet<PatientInsuarance> PatientInsuarances { get; set; }
        public DbSet<PatientPaymentMode> PatientPaymentModes { get; set; }

        //Medicine Modals DBSets
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<MedicineStatus> MedicineStatuses { get; set; }
        public DbSet<MedicineType> MedicineTypes { get; set; }

        //Patient Modals DBSets
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientStatus> PatientStatuses { get; set; }
        public DbSet<PatientType> PatientTypes { get; set; }

        //Payment Modals DBSets
        public DbSet<Insuarance> Insuarances { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentMode> PaymentModes { get; set; }
        public DbSet<PaymentStatus> PaymentStatus { get; set; }

        //Room Modals DBSets
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomStatus> RoomStatus { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }

        //Staff Modals DBSets
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<StaffStatus> StaffStatuses { get; set; }
        public DbSet<StaffTitle> StaffTitles { get; set; }
        public DbSet<StaffType> StaffTypes { get; set; }

        //Treatment Modals DBSets
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<TreatmentStatus> TreatmentStatuses { get; set; }
        public DbSet<TreatmentType> TreatmentTypes { get; set; }

        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<LabTest> LabTests { get; set; }
        public DbSet<MedicineGenre> MedicineGenres { get; set; }

        public DbSet<DoctorCode> DoctorCodes { get; set; }
        public DbSet<EmployeeId> EmployeeIds { get; set; }
        public DbSet<StaffTitleGroup> StaffTitleGroups { get; set; }
        public DbSet<LabTestResultComposition> LabTestResultCompositions { get; set; }
        public DbSet<PatientCode> PatientCodes { get; set; }
        public DbSet<BillType> BillTypes { get; set; }
        public DbSet<PreliminaryTest> PreliminaryTests { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<VisitType> VisitTypes { get; set; }
        public DbSet<Therapy> Therapies { get; set; }
        public DbSet<MedicalDepartment> MedicalDepartments { get; set; }
        public DbSet<Surgery> Surgeries { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Bed> Beds { get; set; }


        #endregion
    }
}
