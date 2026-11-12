using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.NativeInterop;
using NovaCare.Components.Pages.PatientPages;
using NovaCare.Data;
using NovaCare.Interfaces;
using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.JoinTables;
using NovaCare.Modals.PatientModals;
using NovaCare.Modals.StaffModals;
using NovaCare.Shared.DTOs.JoinDto;
using NovaCare.Shared.DTOs.JoinDTOs;
using NovaCare.Shared.DTOs.LabTestDTOs;
using NovaCare.Shared.DTOs.PatientDTOs;
using NovaCare.Shared.DTOs.PaymentDTOs;
using NovaCare.Shared.DTOs.ReportDTOs;
using NovaCare.Shared.DTOs.StaffDTOs;
using NovaCare.Shared.DTOs.TreatmentDTOs;

namespace NovaCare.Services
{
    public class PatientService : IPatientService
    {
        private readonly IDbContextFactory<NovaCareDbContext> _contextFactory;
        public PatientService(IDbContextFactory<NovaCareDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<List<PatientDto>> GetAllPatientsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var patients = await context.Patients
                .AsNoTracking()
                .Select(patient => new PatientDto
                {
                    Id = patient.Id,
                    FisrtName = patient.FirstName,
                    MiddleName = patient.MiddleName,
                    LastName = patient.LastName,
                    PhoneNumber = patient.PhoneNumber,
                    PatientCode = patient.PatientCode,
                    RegisteredDate = patient.RegisteredDate,
                    PatientStatusName = patient.PatientStatus!.PatientStatusName,
                    PatientTypeName = patient.PatientType!.PatientTypeName,
                    Gender = patient.Gender,
                    Age = patient.Age,
                    DoctorPatients = patient.DoctorPatients == null
                        ? new List<DoctorPatientDto>()
                        : patient.DoctorPatients.Select(dp => new DoctorPatientDto
                        {
                            DoctorId = dp.DoctorId
                        }).ToList()
                })
                .ToListAsync();

            return patients;
        }


        public async Task<List<PatientTypeDto>> GetAllPatientStatusesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var patientTypes = await context.PatientTypes
                .AsNoTracking()
                .Select(title => new PatientTypeDto
                {
                    Id = title.Id,
                    PatientTypeName = title.PatientTypeName,

                })
                .ToListAsync();

            return patientTypes;
        }

        public async Task<List<PatientStatusDto>> GetAllPatientTypesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var patientStatus = await context.PatientStatuses
                .AsNoTracking()
                .Select(status => new PatientStatusDto
                {
                    Id = status.Id,
                    PatientStatusName = status.PatientStatusName,

                })
                .ToListAsync();

            return patientStatus;
        }

        public async Task<int> AddPatienCodeAsync()
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var newCode = new PatientCode();

                var result = context.PatientCodes.Add(newCode);
                await context.SaveChangesAsync();

                return result.Entity.PatientCodeNumber;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> UpdatePatientCodeAsync(int patientId, int code)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingPCodeId = await context.PatientCodes.FirstOrDefaultAsync(x => x.PatientCodeNumber == code);

                if (existingPCodeId != null)
                {
                    existingPCodeId.PatientId = patientId;

                    context.PatientCodes.Update(existingPCodeId);
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> AddPatientAsync(PatientDto dto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var resultCode = await AddPatienCodeAsync();
                var patientCodeNumber = $"PCN{resultCode}";

                var patient = new Patient
                {
                    FirstName = dto.FisrtName,
                    MiddleName = dto.MiddleName,
                    LastName = dto.LastName,
                    Age = dto.Age,
                    PhoneNumber = dto.PhoneNumber,
                    EmailAddress = dto.EmailAddress,
                    HomeAddress = dto.HomeAddress,
                    RegisteredDate = dto.RegisteredDate,
                    Gender = dto.Gender,
                    KinFullName = dto.KinFullName,
                    PatientCode = patientCodeNumber,
                    KinPhoneNumber = dto.KinPhoneNumber,
                    PatientFullName = $"{dto.FisrtName} {dto.MiddleName} {dto.LastName}",
                    KinHomeAddress = dto.KinHomeAddress,
                    PatientStatusId = dto.PatientStatusId,
                    PatientTypeId = dto.PatientTypeId
                };

                var result = context.Patients.Add(patient);
                await context.SaveChangesAsync();

                return result.Entity.PatientCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return string.Empty;
            }
        }

        public async Task<ReportPreviewDto<PatientDto>> GetAllPatientsAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var context = _contextFactory.CreateDbContext();
            var patients = await context.Patients
                .Where(x => x.RegisteredDate >= fromDate && x.RegisteredDate <= toDate)
                .AsNoTracking()
                .Select(patient => new PatientDto
                {
                    Id = patient.Id,
                    FisrtName = patient.FirstName,
                    MiddleName = patient.MiddleName,
                    LastName = patient.LastName,
                    PatientCode = patient.PatientCode,
                    PhoneNumber = patient.PhoneNumber,
                    RegisteredDate = patient.RegisteredDate,
                    PatientFullName = patient.PatientFullName,
                    PatientStatusName = patient.PatientStatus!.PatientStatusName,
                    PatientTypeName = patient.PatientType!.PatientTypeName,
                    Gender = patient.Gender,
                    Age = patient.Age,
                    DoctorPatients = patient.DoctorPatients == null
                        ? new List<DoctorPatientDto>()
                        : patient.DoctorPatients.Select(dp => new DoctorPatientDto
                        {
                            DoctorId = dp.DoctorId
                        }).ToList()
                })
                .ToListAsync();

            var result = new ReportPreviewDto<PatientDto>
            {
                FromDate = fromDate,
                ToDate = toDate,
                ReportType = "Patient",
                Data = patients
            };
            return result;
        }

        public async Task<PatientDto> GetPatientByIdAsync(int patientId)
        {
            using var context = _contextFactory.CreateDbContext();
            var patient = await context.Patients
                .AsNoTracking()
                .Select(patient => new PatientDto
                {
                    Id = patient.Id,
                    FisrtName = patient.FirstName,
                    MiddleName = patient.MiddleName,
                    LastName = patient.LastName,
                    PatientFullName = patient.PatientFullName,
                    PhoneNumber = patient.PhoneNumber,
                    PatientCode = patient.PatientCode,
                    RegisteredDate = patient.RegisteredDate,
                    PatientStatusName = patient.PatientStatus!.PatientStatusName,
                    PatientTypeName = patient.PatientType!.PatientTypeName,
                    Gender = patient.Gender,
                    Age = patient.Age,
                    DoctorPatients = patient.DoctorPatients == null
                        ? new List<DoctorPatientDto>()
                        : patient.DoctorPatients.Select(dp => new DoctorPatientDto
                        {
                            DoctorId = dp.DoctorId
                        }).ToList()
                })
                .FirstOrDefaultAsync(x => x.Id == patientId);

            return patient ?? new();
        }

        public async Task<VisitDto?> GetVisitByPCodeOrFullNameAsync(string pCodeOrFullName)
        {
            if (string.IsNullOrWhiteSpace(pCodeOrFullName))
                return null;

            await using var context = await _contextFactory.CreateDbContextAsync();

            var visit = await context.Visits
                .AsNoTracking()
                .AsSplitQuery()
                .Where(v => v.Patient != null &&
                           (v.Patient.PatientCode == pCodeOrFullName ||
                            v.Patient.PatientFullName.Contains(pCodeOrFullName)))
                .OrderByDescending(v => v.VisitDate)
                .Select(v => new VisitDto
                {
                    Id = v.Id,
                    VisitDate = v.VisitDate,
                    DischargedDate = v.DischargedDate,
                    VisitTypeId = v.VisitTypeId,
                    PatientId = v.PatientId,
                    VisitStatusId = v.VisitStatusId,


                    // UI Helpers on VisitDto
                    VisitTypeName = v.VisitType != null ? v.VisitType.TypeName : string.Empty,
                    PatientFullName = v.Patient != null ? v.Patient.PatientFullName : string.Empty,
                    VisitStatusName = v.VisitStatus != null ? v.VisitStatus.VisitStatusName : string.Empty,

                    // 1. Patient Details
                    Patient = v.Patient == null ? null : new PatientDto
                    {
                        Id = v.Patient.Id,
                        PatientCode = v.Patient.PatientCode,
                        PatientFullName = v.Patient.PatientFullName,
                        PatientTypeName = v.Patient.PatientType != null ? v.Patient.PatientType.PatientTypeName : string.Empty,
                        PatientStatusName = v.Patient.PatientStatus != null ? v.Patient.PatientStatus.PatientStatusName : string.Empty
                    },

                    // 2. Preliminary Test
                    PreliminaryTestId = v.PreliminaryTest != null ? v.PreliminaryTest.Id : null,
                    PreliminaryTest = v.PreliminaryTest == null ? null : new PreliminaryTestDto
                    {
                        Id = v.PreliminaryTest.Id,
                        VisitId = v.PreliminaryTest.VisitId,
                        PreliminaryWeight = v.PreliminaryTest.PreliminaryWeight,
                        PreliminaryHeight = v.PreliminaryTest.PreliminaryHeight,
                        PreliminaryPressure = v.PreliminaryTest.PreliminaryPressure
                    },

                    // 3. Bills & Payments
                    Bills = v.Bills.Select(bill => new BillDto
                    {
                        Id = bill.Id,
                        BillDate = bill.BillDate,
                        BillDescription = bill.BillDescription,
                        VisitId = bill.VisitId,
                        BillTypeId = bill.BillTypeId,
                        BillTypeName = bill.BillType != null ? bill.BillType.TypeNme : string.Empty,
                        TotalAmount = bill.TotalAmount,
                        Payments = bill.Payments.Select(pay => new PaymentDto
                        {
                            Id = pay.Id,
                            Amount = pay.Amount, // Verify property name matches your PaymentDto
                            PaymentDate = pay.PaymentDate
                        }).ToList()
                    }).ToList(),

                    // 4. Consultations
                    Consultations = v.Consultations.Select(c => new ConsultationDto
                    {
                        Id = c.Id,
                        Diagnosis = c.Diagnosis,
                        StartDate = c.StartDate,
                        FinishDate = c.FinishDate,
                        ConsultationCost = c.ConsultationCost,

                        // Foreign keys
                        RoomId = c.RoomId,
                        VisitId = c.VisitId,
                        DoctorId = c.DoctorId,
                        MedicalDepartmentId = c.MedicalDepartmentId,
                        BillItemId = c.BillItemId,

                        // UI Helpers
                        MedicalDepartmentName = c.MedicalDepartment != null ? c.MedicalDepartment.MedicalDepartmentName : string.Empty,
                        //DoctorFullName = c.Doctor != null ? c.Doctor.DoctorFullName : string.Empty,
                        VisitTypeName = v.VisitType != null ? v.VisitType.TypeName : string.Empty,
                        RoomName = c.Room != null ? c.Room.RoomName : string.Empty,
                        PaymentStatusName = c.BillItem == null || c.BillItem.Bill == null
                            ? "Not Billed"
                            : (c.BillItem.Bill.Payments.Sum(pay => pay.Amount) <= 0m
                                ? "Unpaid"
                                : (c.BillItem.Bill.Payments.Sum(pay => pay.Amount) < c.BillItem.Bill.TotalAmount
                                    ? "Partial"
                                    : "Fully Paid"))
                    }).ToList(),


                    // 5. Lab Tests
                    LabTests = v.LabTests.Select(l => new LabTestDto
                    {
                        Id = l.Id,
                        DiseaseName = l.DiseaseName,
                        LabTestStatusName = l.LabTestStatusName,
                        CreatedDate = l.CreatedDate,
                        TestResult = l.TestResult,
                        ResultAttachment = l.ResultAttachment,

                        RequesteeDoctor = l.RequesteeDoctor,
                        UpdaterLabTechnician = l.UpdaterLabTechnician,
                        RequesteeId = l.RequesteeId,
                        UpdaterId = l.UpdaterId,
                        LabTestCost = l.LabTestCost,

                        // Foreign keys
                        VisitId = l.VisitId,
                        //PatientId = l.Visit.PatientId,
                        ConsultationId = l.ConsultationId,
                        BillItemId = l.BillItemId,

                        // Nested Result Compositions
                        LabTestResultCompositions = l.LabTestResultCompositions.Select(comp => new LabTestResultCompositionDto
                        {
                            Id = comp.Id
                            // Add your LabTestResultCompositionDto properties here (e.g., ParameterName, ResultValue, NormalRange)
                        }).ToList(),

                        // UI Helpers
                        PatientFullName = v.Patient == null ? string.Empty :
                            (v.Patient.FirstName + " " +
                            (string.IsNullOrEmpty(v.Patient.MiddleName) ? "" : v.Patient.MiddleName + " ") +
                            v.Patient.LastName).Trim(),
                        //DiseaseTypeName = l.Disease != null && l.Disease.DiseaseType != null
                        //    ? l.Disease.DiseaseType.DiseaseTypeName
                        //    : string.Empty,
                        PaymentStatusName = l.BillItem == null || l.BillItem.Bill == null
                            ? "Not Billed"
                            : (l.BillItem.Bill.Payments.Sum(pay => pay.Amount) <= 0m
                                ? "Unpaid"
                                : (l.BillItem.Bill.Payments.Sum(pay => pay.Amount) < l.BillItem.Bill.TotalAmount
                                    ? "Partial"
                                    : "Fully Paid"))
                    }).ToList(),


                    // 6. Prescriptions
                    Prescriptions = v.Prescriptions.Select(p => new PrescriptionDto
                    {
                        Id = p.Id,
                        PrescribedDate = p.PrescribedDate,
                        Quantity = p.Quantity,
                        Dosage = p.Dosage,
                        Notes = p.Notes,
                        UnitPrice = p.UnitPrice,

                        // Foreign keys
                        VisitId = p.VisitId,
                        ConsultationId = p.ConsultationId,
                        MedicineId = p.MedicineId,
                        DoctorId = p.DoctorId,
                        BillItemId = p.BillItemId,

                        // UI Helpers
                        MedicineName = p.Medicine != null ? p.Medicine.MedicineName : string.Empty,
                        //DoctorFullName = p.Doctor != null ? p.Doctor.DoctorFullName : string.Empty,
                        PaymentStatusName = p.BillItem == null || p.BillItem.Bill == null
                            ? "Not Billed"
                            : (p.BillItem.Bill.Payments.Sum(pay => pay.Amount) <= 0m
                                ? "Unpaid"
                                : (p.BillItem.Bill.Payments.Sum(pay => pay.Amount) < p.BillItem.Bill.TotalAmount
                                    ? "Partial"
                                    : "Fully Paid"))
                    }).ToList(),



                    // 7. Treatments
                    Treatments = v.Treatments.Select(t => new TreatmentDto
                    {
                        Id = t.Id,
                        StartDate = t.StartDate,
                        FinishDate = t.FinishDate,
                        TreatmentQty = t.TreatmentQty,
                        TreatmentCost = t.TreatmentCost,
                        // TotalCost is computed automatically in your DTO

                        // Required Foreign Keys
                        VisitId = t.VisitId,
                        ConsultationId = t.ConsultationId,
                        BillItemId = t.BillItemId,

                        // Clinical & Location Foreign Keys
                        SurgeryId = t.SurgeryId,
                        TherapyId = t.TherapyId,
                        MedicalDeptId = t.MedicalDeptId,
                        WardId = t.WardId,
                        DoctorId = t.DoctorId,
                        RoomId = t.RoomId,
                        TreatmentStatusId = t.TreatmentStatusId,
                        TreatmentTypeId = t.TreatmentTypeId,

                        // UI Helpers
                        TreatmentStatusName = t.TreatmentStatus != null ? t.TreatmentStatus.TreatmentStatusName : string.Empty,
                        TreatmentTypeName = t.TreatmentType != null ? t.TreatmentType.TreatmentTypeName : string.Empty,
                        SurgeryName = t.Surgery != null ? t.Surgery.SurgeryName : string.Empty,
                        TherapyName = t.Therapy != null ? t.Therapy.TherapyName : string.Empty,
                        MedicalDeptName = t.MedicalDepartment != null ? t.MedicalDepartment.MedicalDepartmentName : string.Empty,
                        WardName = t.Ward != null ? t.Ward.WardName : string.Empty,
                        //DoctorName = t.Doctor != null ? t.Doctor.DoctorFullName : string.Empty,
                        RoomName = t.Room != null ? t.Room.RoomName : string.Empty,
                        PaymentStatusName = t.BillItem == null || t.BillItem.Bill == null
                            ? "Not Billed"
                            : (t.BillItem.Bill.Payments.Sum(pay => pay.Amount) <= 0m
                                ? "Unpaid"
                                : (t.BillItem.Bill.Payments.Sum(pay => pay.Amount) < t.BillItem.Bill.TotalAmount
                                    ? "Partial"
                                    : "Fully Paid"))
                    }).ToList()

                }).FirstOrDefaultAsync();
            return visit;
        }

        /*
                 public async Task<VisitDto?> GetVisitByPCodeOrFullNameAsync(string pCodeOrFullName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(pCodeOrFullName))
                    return null;

                await using var context = await _contextFactory.CreateDbContextAsync();

                var visit = await context.Visits
                    .AsNoTracking()
                    .AsSplitQuery()
                    .Where(v => v.Patient != null &&
                               (v.Patient.PatientCode == pCodeOrFullName ||
                                v.Patient.PatientFullName.Contains(pCodeOrFullName)))
                    .OrderByDescending(v => v.VisitDate)
                    .Select(v => new VisitDto
                    {
                        Id = v.Id,
                        VisitDate = v.VisitDate,
                        DischargedDate = v.DischargedDate,
                        VisitTypeId = v.VisitTypeId,
                        PatientId = v.PatientId,
                        VisitStatusId = v.VisitStatusId,


                        // UI Helpers on VisitDto
                        VisitTypeName = v.VisitType != null ? v.VisitType.TypeName : string.Empty,
                        PatientFullName = v.Patient != null ? v.Patient.PatientFullName : string.Empty,
                        VisitStatusName = v.VisitStatus != null ? v.VisitStatus.VisitStatusName : string.Empty,

                        // 1. Patient Details
                        Patient = v.Patient == null ? null : new PatientDto
                        {
                            Id = v.Patient.Id,
                            PatientCode = v.Patient.PatientCode,
                            PatientFullName = v.Patient.PatientFullName,
                            PatientTypeName = v.Patient.PatientType != null ? v.Patient.PatientType.PatientTypeName : string.Empty,
                            PatientStatusName = v.Patient.PatientStatus != null ? v.Patient.PatientStatus.PatientStatusName : string.Empty
                        },

                        // 2. Preliminary Test
                        PreliminaryTestId = v.PreliminaryTest != null ? v.PreliminaryTest.Id : null,
                        PreliminaryTest = v.PreliminaryTest == null ? null : new PreliminaryTestDto
                        {
                            Id = v.PreliminaryTest.Id,
                            VisitId = v.PreliminaryTest.VisitId,
                            PreliminaryWeight = v.PreliminaryTest.PreliminaryWeight,
                            PreliminaryHeight = v.PreliminaryTest.PreliminaryHeight,
                            PreliminaryPressure = v.PreliminaryTest.PreliminaryPressure
                        },

                        // 3. Bills & Payments
                        Bills = v.Bills.Select(bill => new BillDto
                        {
                            Id = bill.Id,
                            BillDate = bill.BillDate,
                            BillDescription = bill.BillDescription,
                            VisitId = bill.VisitId,
                            BillTypeId = bill.BillTypeId,
                            BillTypeName = bill.BillType != null ? bill.BillType.TypeNme : string.Empty,
                            TotalAmount = bill.TotalAmount,
                            Payments = bill.Payments.Select(pay => new PaymentDto
                            {
                                Id = pay.Id,
                                Amount = pay.Amount, // Verify property name matches your PaymentDto
                                PaymentDate = pay.PaymentDate
                            }).ToList()
                        }).ToList(),

                        // 4. Consultations
                        Consultations = v.Consultations.Select(c => new ConsultationDto
                        {
                            Id = c.Id,
                            Diagnosis = c.Diagnosis,
                            StartDate = c.StartDate,
                            FinishDate = c.FinishDate,
                            ConsultationCost = c.ConsultationCost,

                            // Foreign keys
                            RoomId = c.RoomId,
                            VisitId = c.VisitId,
                            DoctorId = c.DoctorId,
                            MedicalDepartmentId = c.MedicalDepartmentId,
                            BillItemId = c.BillItemId,

                            // UI Helpers
                            MedicalDepartmentName = c.MedicalDepartment != null ? c.MedicalDepartment.MedicalDepartmentName : string.Empty,
                            //DoctorFullName = c.Doctor != null ? c.Doctor.DoctorFullName : string.Empty,
                            VisitTypeName = v.VisitType != null ? v.VisitType.TypeName : string.Empty,
                            RoomName = c.Room != null ? c.Room.RoomName : string.Empty,
                            PaymentStatusName = c.BillItem == null || c.BillItem.Bill == null
                                ? "Not Billed"
                                : (c.BillItem.Bill.Payments.Sum(pay => pay.Amount) <= 0m
                                    ? "Unpaid"
                                    : (c.BillItem.Bill.Payments.Sum(pay => pay.Amount) < c.BillItem.Bill.TotalAmount
                                        ? "Partial"
                                        : "Fully Paid"))
                        }).ToList(),


                        // 5. Lab Tests
                        LabTests = v.LabTests.Select(l => new LabTestDto
                        {
                            Id = l.Id,
                            DiseaseName = l.DiseaseName,
                            LabTestStatusName = l.LabTestStatusName,
                            CreatedDate = l.CreatedDate,
                            TestResult = l.TestResult,
                            ResultAttachment = l.ResultAttachment,

                            RequesteeDoctor = l.RequesteeDoctor,
                            UpdaterLabTechnician = l.UpdaterLabTechnician,
                            RequesteeId = l.RequesteeId,
                            UpdaterId = l.UpdaterId,
                            LabTestCost = l.LabTestCost,

                            // Foreign keys
                            VisitId = l.VisitId,
                            //PatientId = l.Visit.PatientId,
                            ConsultationId = l.ConsultationId,
                            BillItemId = l.BillItemId,

                            // Nested Result Compositions
                            LabTestResultCompositions = l.LabTestResultCompositions.Select(comp => new LabTestResultCompositionDto
                            {
                                Id = comp.Id
                                // Add your LabTestResultCompositionDto properties here (e.g., ParameterName, ResultValue, NormalRange)
                            }).ToList(),

                            // UI Helpers
                            PatientFullName = v.Patient == null ? string.Empty :
                                (v.Patient.FirstName + " " +
                                (string.IsNullOrEmpty(v.Patient.MiddleName) ? "" : v.Patient.MiddleName + " ") +
                                v.Patient.LastName).Trim(),
                            //DiseaseTypeName = l.Disease != null && l.Disease.DiseaseType != null
                            //    ? l.Disease.DiseaseType.DiseaseTypeName
                            //    : string.Empty,
                            PaymentStatusName = l.BillItem == null || l.BillItem.Bill == null
                                ? "Not Billed"
                                : (l.BillItem.Bill.Payments.Sum(pay => pay.Amount) <= 0m
                                    ? "Unpaid"
                                    : (l.BillItem.Bill.Payments.Sum(pay => pay.Amount) < l.BillItem.Bill.TotalAmount
                                        ? "Partial"
                                        : "Fully Paid"))
                        }).ToList(),


                        // 6. Prescriptions
                        Prescriptions = v.Prescriptions.Select(p => new PrescriptionDto
                        {
                            Id = p.Id,
                            PrescribedDate = p.PrescribedDate,
                            Quantity = p.Quantity,
                            Dosage = p.Dosage,
                            Notes = p.Notes,
                            UnitPrice = p.UnitPrice,

                            // Foreign keys
                            VisitId = p.VisitId,
                            ConsultationId = p.ConsultationId,
                            MedicineId = p.MedicineId,
                            DoctorId = p.DoctorId,
                            BillItemId = p.BillItemId,

                            // UI Helpers
                            MedicineName = p.Medicine != null ? p.Medicine.MedicineName : string.Empty,
                            //DoctorFullName = p.Doctor != null ? p.Doctor.DoctorFullName : string.Empty,
                            PaymentStatusName = p.BillItem == null || p.BillItem.Bill == null
                                ? "Not Billed"
                                : (p.BillItem.Bill.Payments.Sum(pay => pay.Amount) <= 0m
                                    ? "Unpaid"
                                    : (p.BillItem.Bill.Payments.Sum(pay => pay.Amount) < p.BillItem.Bill.TotalAmount
                                        ? "Partial"
                                        : "Fully Paid"))
                        }).ToList(),



                        // 7. Treatments
                        Treatments = v.Treatments.Select(t => new TreatmentDto
                        {
                            Id = t.Id,
                            StartDate = t.StartDate,
                            FinishDate = t.FinishDate,
                            TreatmentQty = t.TreatmentQty,
                            TreatmentCost = t.TreatmentCost,
                            // TotalCost is computed automatically in your DTO

                            // Required Foreign Keys
                            VisitId = t.VisitId,
                            ConsultationId = t.ConsultationId,
                            BillItemId = t.BillItemId,

                            // Clinical & Location Foreign Keys
                            SurgeryId = t.SurgeryId,
                            TherapyId = t.TherapyId,
                            MedicalDeptId = t.MedicalDeptId,
                            WardId = t.WardId,
                            DoctorId = t.DoctorId,
                            RoomId = t.RoomId,
                            TreatmentStatusId = t.TreatmentStatusId,
                            TreatmentTypeId = t.TreatmentTypeId,

                            // UI Helpers
                            TreatmentStatusName = t.TreatmentStatus != null ? t.TreatmentStatus.TreatmentStatusName : string.Empty,
                            TreatmentTypeName = t.TreatmentType != null ? t.TreatmentType.TreatmentTypeName : string.Empty,
                            SurgeryName = t.Surgery != null ? t.Surgery.SurgeryName : string.Empty,
                            TherapyName = t.Therapy != null ? t.Therapy.TherapyName : string.Empty,
                            MedicalDeptName = t.MedicalDepartment != null ? t.MedicalDepartment.MedicalDepartmentName : string.Empty,
                            WardName = t.Ward != null ? t.Ward.WardName : string.Empty,
                            //DoctorName = t.Doctor != null ? t.Doctor.DoctorFullName : string.Empty,
                            RoomName = t.Room != null ? t.Room.RoomName : string.Empty,
                            PaymentStatusName = t.BillItem == null || t.BillItem.Bill == null
                                ? "Not Billed"
                                : (t.BillItem.Bill.Payments.Sum(pay => pay.Amount) <= 0m
                                    ? "Unpaid"
                                    : (t.BillItem.Bill.Payments.Sum(pay => pay.Amount) < t.BillItem.Bill.TotalAmount
                                        ? "Partial"
                                        : "Fully Paid"))
                        }).ToList()

                    }).FirstOrDefaultAsync();
                return visit;
            }
            catch(Exception ex)
            {
                var message = $"{ex.Message}";
                return new();
            }
        }
         */

        public async Task<bool> UpdatePatientAsync(PatientDto dto, int patientId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingPatient = await context.Patients.FirstOrDefaultAsync(x => x.Id == patientId);

                if (existingPatient != null)
                {
                    existingPatient.FirstName = dto.FisrtName;
                    existingPatient.MiddleName = dto.MiddleName;
                    existingPatient.LastName = dto.LastName;
                    existingPatient.PatientCode = dto.PatientCode;
                    existingPatient.PatientPhoto = dto.PatientPhoto;
                    existingPatient.Age = dto.Age;
                    existingPatient.PhoneNumber = dto.PhoneNumber;
                    existingPatient.EmailAddress = dto.EmailAddress;
                    existingPatient.HomeAddress = dto.HomeAddress;
                    existingPatient.RegisteredDate = dto.RegisteredDate;
                    existingPatient.PatientFullName = $"{dto.FisrtName} {dto.MiddleName} {dto.LastName}";
                    existingPatient.Gender = dto.Gender;
                    existingPatient.KinFullName = dto.KinFullName;
                    existingPatient.KinPhoneNumber = dto.KinPhoneNumber;
                    existingPatient.KinHomeAddress = dto.KinHomeAddress;
                    existingPatient.PatientStatusId = dto.PatientStatusId;
                    existingPatient.PatientTypeId = dto.PatientTypeId;

                    existingPatient.PatientDiseases = dto.PatientDiseases?.Select(x => new PatientDisease 
                    {
                        DiseaseId = x.DiseaseId,
                        PatientId = x.PatientId
                    }).ToList();

                    existingPatient.DoctorPatients = dto.DoctorPatients?.Select(x => new DoctorPatient
                    {
                        DoctorId = x.DoctorId,
                        PatientId = x.PatientId
                    }).ToList();

                    existingPatient.PatientInsuarances = dto.PatientInsuarances?.Select(x => new PatientInsuarance
                    {
                        InsuaranceId = x.InsuaranceId,
                        PatientId = x.PatientId
                    }).ToList();

                    existingPatient.PatientPaymentModes = dto.PatientPaymentModes?.Select(x => new PatientPaymentMode
                    {
                        PaymentModeId = x.PaymentModeId,
                        PatientId = x.PatientId
                    }).ToList();

                    existingPatient.Visits = dto.Visits?.Select(x => new Visit
                    {
                        VisitTypeId = x.VisitTypeId,
                        PatientId = x.PatientId,
                        DischargedDate = x.DischargedDate
                    }).ToList();

                    context.Patients.Update(existingPatient);
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<VisitDto>> GetAllVisitsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var visits = await context.Visits
                .AsNoTracking()
                .Select(visit => new VisitDto
                {
                    Id = visit.Id,
                    VisitDate = visit.VisitDate,
                    VisitTypeId = visit.VisitTypeId,
                    PatientId = visit.PatientId,
                    DischargedDate = visit.DischargedDate,
                    Bills = visit.Bills.Select(bill => new BillDto
                    {
                        Id = bill.Id,
                        BillDate = bill.BillDate,
                        BillDescription = bill.BillDescription,
                        VisitId = bill.VisitId,
                        TotalAmount = bill.TotalAmount
                    }).ToList(),

                    Patient = visit.Patient == null ? null : new PatientDto
                    {
                        Id = visit.Patient.Id,
                        PatientTypeName = visit.Patient.PatientType!.PatientTypeName,
                        PatientFullName = visit.Patient.PatientFullName,
                        PatientStatusName = visit.Patient.PatientStatus!.PatientStatusName
                    }

                }).ToListAsync();

            return visits;
        }

        public async Task<int> AddVisitAsync(VisitDto dto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var visit = new Visit
                {
                    VisitDate = dto.VisitDate,
                    VisitTypeId = dto.VisitTypeId,
                    PatientId = dto.PatientId,
                    VisitStatusId = 1,
                };

                var result = context.Visits.Add(visit);
                await context.SaveChangesAsync();

                return result.Entity.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return 0;
            }
        }

        public async Task<bool> UpdateVisitAsync(VisitDto dto, int visitId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingVisit = await context.Visits.FirstOrDefaultAsync(x => x.Id == visitId);

                if (existingVisit != null)
                {
                    existingVisit.VisitTypeId = dto.VisitTypeId;
                    existingVisit.PatientId = dto.PatientId;
                    existingVisit.DischargedDate = dto.DischargedDate;

                    existingVisit.VisitStatusId = dto.VisitStatusId;

                    context.Visits.Update(existingVisit);
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<VisitTypeDto>> GetAllVisitTypesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var visits = await context.VisitTypes
                .AsNoTracking()
                .Select(visit => new VisitTypeDto
                {
                    Id = visit.Id,
                    TypeName = visit.TypeName
                }).ToListAsync();

            return visits;
        }

        public async Task<int> AddVisitTypeAsync(VisitTypeDto dto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var visitType = new VisitType
                {
                    TypeName = dto.TypeName
                };

                var result = context.VisitTypes.Add(visitType);
                await context.SaveChangesAsync();

                return result.Entity.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return 0;
            }
        }

        public async Task<bool> UpdateVisitTypeAsync(VisitTypeDto dto, int visitTypeId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingVisitType = await context.VisitTypes.FirstOrDefaultAsync(x => x.Id == visitTypeId);

                if (existingVisitType != null)
                {
                    existingVisitType.TypeName = dto.TypeName;

                    context.VisitTypes.Update(existingVisitType);
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<PatientDto> GetPatientByCodeAsync(string patientCode)
        {
            using var context = _contextFactory.CreateDbContext();

            var patient = await context.Patients
                .AsNoTracking()
                .Where(p => p.PatientCode == patientCode)
                .Select(patient => new PatientDto
                {
                    Id = patient.Id,
                    FisrtName = patient.FirstName,
                    MiddleName = patient.MiddleName,
                    LastName = patient.LastName,
                    PhoneNumber = patient.PhoneNumber,
                    RegisteredDate = patient.RegisteredDate,
                    PatientFullName = patient.PatientFullName,
                    PatientStatusName = patient.PatientStatus!.PatientStatusName,
                    PatientTypeName = patient.PatientType!.PatientTypeName,
                    Gender = patient.Gender,
                    Age = patient.Age,
                    PatientCode = patient.PatientCode
                })
                .FirstOrDefaultAsync();

            return patient ?? new();
        }

        public async Task<PreliminaryTestDto> AddPreliminaryTestAsync(PreliminaryTestDto dto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var preliminaryTest = new PreliminaryTest
                {
                    PreliminaryWeight = dto.PreliminaryWeight,
                    PreliminaryHeight = dto.PreliminaryHeight,
                    PreliminaryPressure = dto.PreliminaryPressure,
                    VisitId = dto.VisitId,
                };

                var result = context.PreliminaryTests.Add(preliminaryTest);
                await context.SaveChangesAsync();

                var preliminaryTestReturned = new PreliminaryTestDto
                {
                    Id = dto.Id,
                    PreliminaryWeight = dto.PreliminaryWeight,
                    PreliminaryHeight = dto.PreliminaryHeight,
                    VisitId = dto.VisitId,
                    PreliminaryPressure = dto.PreliminaryPressure,
                };

                return preliminaryTestReturned;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return new();
            }
        }

        public async Task<int> AddConsultationAsync(ConsultationDto dto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var visit = new Consultation
                {
                    Diagnosis = dto.Diagnosis,
                    StartDate = dto.StartDate,
                    FinishDate = dto.FinishDate,
                    ConsultationCost = dto.ConsultationCost,
                    VisitId = dto.VisitId,
                    RoomId = dto.RoomId,
                    DoctorId = dto.DoctorId,
                    MedicalDepartmentId = dto.MedicalDepartmentId,
                };

                var result = context.Consultations.Add(visit);
                await context.SaveChangesAsync();

                return result.Entity.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return 0;
            }
        }

        public async Task<bool> UpdateConsultationAsync(ConsultationDto dto, int conslId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingConsultation = await context.Consultations.FirstOrDefaultAsync(x => x.Id == conslId);

                if (existingConsultation != null)
                {
                    existingConsultation.Diagnosis = dto.Diagnosis;
                    existingConsultation.StartDate = dto.StartDate;
                    existingConsultation.FinishDate = dto.FinishDate;
                    existingConsultation.ConsultationCost = dto.ConsultationCost;
                    existingConsultation.VisitId = dto.VisitId;
                    existingConsultation.RoomId = dto.RoomId;
                    existingConsultation.DoctorId = dto.DoctorId;
                    existingConsultation.MedicalDepartmentId = dto.MedicalDepartmentId;

                    context.Consultations.Update(existingConsultation);
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
