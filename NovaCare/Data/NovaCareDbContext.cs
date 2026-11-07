using Microsoft.AspNetCore.Identity;
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
                new TreatmentType { Id = 1, TreatmentTypeCode = "001", TreatmentTypeName = "Medication" },
                new TreatmentType { Id = 2, TreatmentTypeCode = "002", TreatmentTypeName = "Surgery" },
                new TreatmentType { Id = 3, TreatmentTypeCode = "003", TreatmentTypeName = "Therapy" },
                new TreatmentType { Id = 4, TreatmentTypeCode = "004", TreatmentTypeName = "Implantation" }
                );

            builder.Entity<TreatmentStatus>().HasData(
                new TreatmentStatus { Id = 1, TreatmentStatusName = "OnGoing" },
                new TreatmentStatus { Id = 2, TreatmentStatusName = "Completed" }
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
                new StaffTitle { Id = 3, StaffTypeId = 1, StaffTitleName = "Director", TitleGroupId =1 },
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
                new RoomType { Id = 4, RoomTypeCode = "004", RoomTypeName = "Radiology" }
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
                new DoctorType { Id = 1, DoctorTypeCode = "001", DoctorTypeName = "MD" },
                new DoctorType { Id = 2, DoctorTypeCode = "002", DoctorTypeName = "OB-GYN" },
                new DoctorType { Id = 3, DoctorTypeCode = "003", DoctorTypeName = "Cardiologist" },
                new DoctorType { Id = 4, DoctorTypeCode = "004", DoctorTypeName = "Neurologist" },
                new DoctorType { Id = 5, DoctorTypeCode = "005", DoctorTypeName = "Oncologist" },
                new DoctorType { Id = 6, DoctorTypeCode = "006", DoctorTypeName = "Orthopedic" }
                );

            builder.Entity<DoctorStatus>().HasData(
                new DoctorStatus { Id = 1, DoctorStatusName = "Free" },
                new DoctorStatus { Id = 2, DoctorStatusName = "Busy" },
                new DoctorStatus { Id = 3, DoctorStatusName = "WithPatients" }
                );

            builder.Entity<DiseaseType>().HasData(
                new DiseaseType { Id = 1, DiseaseTypeCode = "001", DiseaseTypeName = "Infectious" },
                new DiseaseType { Id = 2, DiseaseTypeCode = "002", DiseaseTypeName = "Non-Infectious" },
                new DiseaseType { Id = 3, DiseaseTypeCode = "003", DiseaseTypeName = "Genetic " },
                new DiseaseType { Id = 4, DiseaseTypeCode = "004", DiseaseTypeName = "Deficiency" },
                new DiseaseType { Id = 5, DiseaseTypeCode = "005", DiseaseTypeName = "Physiological " }
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
                new VisitType { Id = 7, TypeNme = "Check-Up " }
                );

            builder.Entity<Service>().HasData(
                new Service { Id = 1, ServiceName = "Check-Up" },
                new Service { Id = 2, ServiceName = "Dialysis" },
                new Service { Id = 3, ServiceName = "Hospitalized" },
                new Service { Id = 4, ServiceName = "Medicine" },
                new Service { Id = 5, ServiceName = "Vaccines" },
                new Service { Id = 6, ServiceName = "Therapy" },
                new Service { Id = 7, ServiceName = "Consultation" }
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
        public DbSet<BillService> BillServices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<PreliminaryTest> PreliminaryTests { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<VisitType> VisitTypes { get; set; }


        #endregion
    }
}
