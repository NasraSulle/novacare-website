using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.NativeInterop;
using NovaCare.Components.Pages.PatientPages;
using NovaCare.Data;
using NovaCare.Interfaces;
using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.InsuranceModals;
using NovaCare.Modals.JoinTables;
using NovaCare.Modals.PatientModals;
using NovaCare.Modals.StaffModals;
using NovaCare.Shared.DTOs.DoctorDTOs;
using NovaCare.Shared.DTOs.InsuranceDTOs;
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
                .AsSplitQuery()
                .Select(patient => new PatientDto
                {
                    Id = patient.Id,
                    FisrtName = patient.FirstName,
                    MiddleName = patient.MiddleName,
                    LastName = patient.LastName,
                    PatientFullName = !string.IsNullOrWhiteSpace(patient.PatientFullName)
                        ? patient.PatientFullName
                        : $"{patient.FirstName} {patient.MiddleName} {patient.LastName}".Replace("  ", " ").Trim(),
                    PhoneNumber = patient.PhoneNumber,
                    PatientCode = patient.PatientCode,
                    RegisteredDate = patient.RegisteredDate,
                    PatientStatusId = patient.PatientStatusId,
                    PatientTypeId = patient.PatientTypeId,
                    PatientStatusName = patient.PatientStatus != null ? patient.PatientStatus.PatientStatusName : string.Empty,
                    PatientTypeName = patient.PatientType != null ? patient.PatientType.PatientTypeName : string.Empty,
                    Gender = patient.Gender,
                    Age = patient.Age,
                    DoctorPatients = patient.DoctorPatients
                        .Select(dp => new DoctorPatientDto
                        {
                            DoctorId = dp.DoctorId
                        }).ToList(),

                    Visits = patient.Visits
                        .Select(v => new VisitDto
                        {
                            Id = v.Id,
                            VisitStatusId = v.VisitStatusId,

                            // UI Helpers
                            VisitStatusName = v.VisitStatus != null ? v.VisitStatus.VisitStatusName : string.Empty,
                            PreliminaryTestId = v.PreliminaryTest != null ? v.PreliminaryTest.Id : null,

                            
                            // 2. Preliminary Test (Vitals)
                            PreliminaryTest = v.PreliminaryTest == null ? null : new PreliminaryTestDto
                            {
                                Id = v.PreliminaryTest.Id,
                                PreliminaryPressure = v.PreliminaryTest.PreliminaryPressure,
                                PreliminaryHeight = v.PreliminaryTest.PreliminaryHeight,
                                PreliminaryWeight = v.PreliminaryTest.PreliminaryWeight,
                                VisitId = v.PreliminaryTest.VisitId,
                            },

                            
                            // 4. Consultations
                            Consultations = v.Consultations.Select(c => new ConsultationDto
                            {
                                Id = c.Id,
                                DoctorId = c.DoctorId,
                                IsReadyForService =
                                                (c.BillItem != null && c.BillItem.Bill != null && c.BillItem.Bill.PaymentStatusId == 3)
                                                ||
                                                (c.BillItem != null && c.BillItem.Bill != null &&
                                                 c.BillItem.Bill.PaymentModeId == 2 &&
                                                 c.BillItem.Bill.PatientPaidAmount >= c.BillItem.Bill.TotalPatientAmount)
                                //DoctorName = c.Doctor != null ? c.Doctor.FullName : string.Empty,
                            }).ToList(),

                            // 5. Lab Tests
                            LabTests = v.LabTests.Select(l => new LabTestDto
                            {
                                Id = l.Id,
                                IsReadyForService =
                                                (l.BillItem != null && l.BillItem.Bill != null && l.BillItem.Bill.PaymentStatusId == 3)
                                                ||
                                                (l.BillItem != null && l.BillItem.Bill != null &&
                                                 l.BillItem.Bill.PaymentModeId == 2 &&
                                                 l.BillItem.Bill.PatientPaidAmount >= l.BillItem.Bill.TotalPatientAmount),
                            }).ToList(),
                            // 6. Prescriptions
                            Prescriptions = v.Prescriptions.Select(p => new PrescriptionDto
                            {
                                Id = p.Id,
                                IsReadyForService =
                                                (p.BillItem != null && p.BillItem.Bill != null && p.BillItem.Bill.PaymentStatusId == 3)
                                                ||
                                                (p.BillItem != null && p.BillItem.Bill != null &&
                                                 p.BillItem.Bill.PaymentModeId == 2 &&
                                                 p.BillItem.Bill.PatientPaidAmount >= p.BillItem.Bill.TotalPatientAmount),
                            }).ToList(),
                            // 7. Treatments
                            Treatments = v.Treatments.Select(t => new TreatmentDto
                            {
                                Id = t.Id,
                                IsReadyForService =
                                                (t.BillItem != null && t.BillItem.Bill != null && t.BillItem.Bill.PaymentStatusId == 3)
                                                ||
                                                (t.BillItem != null && t.BillItem.Bill != null &&
                                                 t.BillItem.Bill.PaymentModeId == 2 &&
                                                 t.BillItem.Bill.PatientPaidAmount >= t.BillItem.Bill.TotalPatientAmount),
                            }).ToList()
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
                .AsSplitQuery()
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
                .AsSplitQuery()
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
                    RegisteredDate = dto.RegisteredDate == default ? DateTime.UtcNow : dto.RegisteredDate,
                    Gender = dto.Gender,
                    KinFullName = dto.KinFullName,
                    PatientCode = patientCodeNumber,
                    KinPhoneNumber = dto.KinPhoneNumber,
                    PatientFullName = $"{dto.FisrtName} {dto.MiddleName} {dto.LastName}".Replace("  ", " ").Trim(),
                    KinHomeAddress = dto.KinHomeAddress,
                    PatientStatusId = dto.PatientStatusId,
                    PatientTypeId = dto.PatientTypeId,
                    PatientInsuarances = dto.PatientInsuarances?
                        .Where(pi => pi.InsuaranceId > 0 && !string.IsNullOrWhiteSpace(pi.MemberCardNumber))
                        .Select(pi => new PatientInsuarance
                        {
                            InsuaranceId = pi.InsuaranceId,
                            InsuranceSchemeId = pi.InsuranceSchemeId,
                            MemberCardNumber = pi.MemberCardNumber,
                            PrincipalMemberName = pi.PrincipalMemberName,
                            RelationshipToPrincipal = string.IsNullOrWhiteSpace(pi.RelationshipToPrincipal) ? "Self" : pi.RelationshipToPrincipal,
                            ValidFrom = pi.ValidFrom,
                            ValidTo = pi.ValidTo,
                            IsPrimary = pi.IsPrimary,
                            IsActive = true
                        }).ToList() ?? new List<PatientInsuarance>()
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
                .AsSplitQuery()
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
                .AsSplitQuery() // Prevents Cartesian explosion across Bills, LabTests, Prescriptions, Treatments, etc.
                .Where(x => x.Id == patientId)
                .Select(patient => new PatientDto
                {
                    Id = patient.Id,
                    FisrtName = patient.FirstName,
                    MiddleName = patient.MiddleName,
                    LastName = patient.LastName,
                    PatientFullName = !string.IsNullOrWhiteSpace(patient.PatientFullName)
                        ? patient.PatientFullName
                        : $"{patient.FirstName} {patient.MiddleName} {patient.LastName}".Replace("  ", " ").Trim(),
                    PhoneNumber = patient.PhoneNumber,
                    EmailAddress = patient.EmailAddress,
                    HomeAddress = patient.HomeAddress,
                    PatientPhoto = patient.PatientPhoto,
                    PatientCode = patient.PatientCode,
                    RegisteredDate = patient.RegisteredDate,
                    Gender = patient.Gender,
                    Age = patient.Age,

                    // First of Kin
                    KinFullName = patient.KinFullName,
                    KinPhoneNumber = patient.KinPhoneNumber,
                    KinHomeAddress = patient.KinHomeAddress,

                    // Status & Type
                    PatientStatusId = patient.PatientStatusId,
                    PatientTypeId = patient.PatientTypeId,
                    PatientStatusName = patient.PatientStatus != null ? patient.PatientStatus.PatientStatusName : string.Empty,
                    PatientTypeName = patient.PatientType != null ? patient.PatientType.PatientTypeName : string.Empty,

                    // Doctors
                    DoctorPatients = patient.DoctorPatients
                        .Select(dp => new DoctorPatientDto
                        {
                            DoctorId = dp.DoctorId,
                        }).ToList(),

                    // Registered Insurance Policies (Fetched ONCE here instead of inside every visit)
                    PatientInsuarances = patient.PatientInsuarances
                        .Select(pi => new PatientInsuaranceDto
                        {
                            PatientInsuaranceId = pi.PatientInsuaranceId,
                            PatientId = pi.PatientId,
                            InsuaranceId = pi.InsuaranceId,
                            InsuranceSchemeId = pi.InsuranceSchemeId,
                            MemberCardNumber = pi.MemberCardNumber,
                            PrincipalMemberName = pi.PrincipalMemberName,
                            RelationshipToPrincipal = pi.RelationshipToPrincipal,
                            ValidFrom = pi.ValidFrom,
                            ValidTo = pi.ValidTo,
                            IsPrimary = pi.IsPrimary,
                            IsActive = pi.IsActive,
                            InsuranceProviderName = pi.Insuarance != null ? pi.Insuarance.InsuaranceName : string.Empty,
                            InsuranceSchemeName = pi.InsuranceScheme != null ? pi.InsuranceScheme.SchemeName : string.Empty,
                            DefaultCoveragePercent = pi.InsuranceScheme != null ? pi.InsuranceScheme.DefaultCoveragePercent : 100m
                        }).ToList(),

                    // Visits
                    Visits = patient.Visits
                        .Select(v => new VisitDto
                        {
                            Id = v.Id,
                            VisitDate = v.VisitDate,
                            DischargedDate = v.DischargedDate,
                            VisitTypeId = v.VisitTypeId,
                            PatientId = v.PatientId,
                            VisitStatusId = v.VisitStatusId,

                            // Payment Mode & Insurance Info
                            PaymentModeId = v.PaymentModeId,
                            PatientInsuaranceId = v.PatientInsuaranceId,
                            RegisteredPaymentMode = v.PaymentMode != null ? v.PaymentMode.PaymentModeName : "Cash",
                            InsuranceProviderName = (v.PatientInsuarance != null && v.PatientInsuarance.Insuarance != null)
                                ? v.PatientInsuarance.Insuarance.InsuaranceName
                                : string.Empty,
                            InsuranceSchemeName = (v.PatientInsuarance != null && v.PatientInsuarance.InsuranceScheme != null)
                                ? v.PatientInsuarance.InsuranceScheme.SchemeName
                                : string.Empty,
                            MemberCardNumber = v.PatientInsuarance != null ? v.PatientInsuarance.MemberCardNumber : string.Empty,
                            AuthorizationNumber = v.VisitInsuranceAuth != null ? v.VisitInsuranceAuth.AuthorizationNumber : string.Empty,

                            // UI Helpers
                            VisitTypeName = v.VisitType != null ? v.VisitType.TypeName : string.Empty,
                            PatientFullName = v.Patient != null ? v.Patient.PatientFullName : string.Empty,
                            VisitStatusName = v.VisitStatus != null ? v.VisitStatus.VisitStatusName : string.Empty,
                            PreliminaryTestId = v.PreliminaryTest != null ? v.PreliminaryTest.Id : null,

                            // 2. Preliminary Test (Vitals)
                            PreliminaryTest = v.PreliminaryTest == null ? null : new PreliminaryTestDto
                            {
                                Id = v.PreliminaryTest.Id,
                                PreliminaryPressure = v.PreliminaryTest.PreliminaryPressure,
                                PreliminaryHeight = v.PreliminaryTest.PreliminaryHeight,
                                PreliminaryWeight = v.PreliminaryTest.PreliminaryWeight,
                                VisitId = v.PreliminaryTest.VisitId,
                            },

                            // 3. Bills
                            Bills = v.Bills.Select(b => new BillDto
                            {
                                Id = b.Id,
                                BillDate = b.BillDate,
                                BillNumber = b.BillNumber,
                                BillDescription = b.BillDescription,
                                TotalAmount = b.TotalAmount,
                                TotalPatientAmount = b.TotalPatientAmount,
                                TotalInsuranceAmount = b.TotalInsuranceAmount,
                                PatientPaidAmount = b.PatientPaidAmount,
                                InsurancePaidAmount = b.InsurancePaidAmount,
                                PaidAmount = b.PatientPaidAmount + b.InsurancePaidAmount,
                                VisitId = b.VisitId,
                                BillTypeId = b.BillTypeId,
                                PaymentModeId = b.PaymentModeId,
                                PaymentStatusId = b.PaymentStatusId,
                                PatientInsuaranceId = b.PatientInsuaranceId,
                                StaffId = b.StaffId,
                                BillTypeName = b.BillType != null ? b.BillType.TypeNme : string.Empty,
                                PaymentModeName = b.PaymentMode != null ? b.PaymentMode.PaymentModeName : "Cash",
                                PaymentStatusName = b.PaymentStatus != null ? b.PaymentStatus.PaymentStatusName : "Unpaid",
                                StaffFullName = b.Staff != null ? b.Staff.FisrtName : string.Empty,
                                BillItems = b.BillItems.Select(bi => new BillItemDto
                                {
                                    Id = bi.Id,
                                    BillId = bi.BillId,
                                    ItemName = bi.ItemName,
                                    SourceType = bi.SourceType,
                                    SourceItemId = bi.SourceItemId,
                                    InsurerItemCode = bi.InsurerItemCode,
                                    Quantity = bi.Quantity,
                                    UnitPrice = bi.UnitPrice,
                                    IsInsuranceCovered = bi.IsInsuranceCovered,
                                    InsuranceCoveragePercent = bi.InsuranceCoveragePercent,
                                    InsuranceCoveredAmount = bi.InsuranceCoveredAmount,
                                    PatientPayableAmount = bi.PatientPayableAmount,
                                    ClaimItemStatus = bi.ClaimItemStatus,
                                    RejectionReason = bi.RejectionReason
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
                                VisitId = c.VisitId,
                                RoomId = c.RoomId,
                                DoctorId = c.DoctorId,
                                DoctorStaffId = c.Doctor != null ? c.Doctor.StaffId : null,
                                MedicalDepartmentId = c.MedicalDepartmentId,
                                BillItemId = c.BillItemId,
                                MedicalDepartmentName = c.MedicalDepartment != null ? c.MedicalDepartment.MedicalDepartmentName : string.Empty,
                                RoomName = c.Room != null ? c.Room.RoomName : string.Empty,
                                PaymentStatusName = (c.BillItem != null && c.BillItem.Bill != null && c.BillItem.Bill.PaymentStatus != null)
                                    ? c.BillItem.Bill.PaymentStatus.PaymentStatusName
                                    : "Unbilled"
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
                                VisitId = l.VisitId,
                                ConsultationId = l.ConsultationId,
                                BillItemId = l.BillItemId,
                                PaymentStatusName = (l.BillItem != null && l.BillItem.Bill != null && l.BillItem.Bill.PaymentStatus != null)
                                    ? l.BillItem.Bill.PaymentStatus.PaymentStatusName
                                    : "Unbilled"
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
                                VisitId = p.VisitId,
                                ConsultationId = p.ConsultationId,
                                MedicineId = p.MedicineId,
                                DoctorId = p.DoctorId,
                                BillItemId = p.BillItemId,
                                MedicineName = p.Medicine != null ? p.Medicine.MedicineName : string.Empty,
                                PaymentStatusName = (p.BillItem != null && p.BillItem.Bill != null && p.BillItem.Bill.PaymentStatus != null)
                                    ? p.BillItem.Bill.PaymentStatus.PaymentStatusName
                                    : "Unbilled"
                            }).ToList(),

                            // 7. Treatments
                            Treatments = v.Treatments.Select(t => new TreatmentDto
                            {
                                Id = t.Id,
                                StartDate = t.StartDate,
                                FinishDate = t.FinishDate,
                                TreatmentQty = t.TreatmentQty,
                                TreatmentCost = t.TreatmentCost,
                                VisitId = t.VisitId,
                                ConsultationId = t.ConsultationId,
                                BillItemId = t.BillItemId,
                                SurgeryId = t.SurgeryId,
                                TherapyId = t.TherapyId,
                                MedicalDeptId = t.MedicalDeptId,
                                WardId = t.WardId,
                                DoctorId = t.DoctorId,
                                RoomId = t.RoomId,
                                TreatmentStatusId = t.TreatmentStatusId,
                                TreatmentTypeId = t.TreatmentTypeId,
                                TreatmentStatusName = t.TreatmentStatus != null ? t.TreatmentStatus.TreatmentStatusName : string.Empty,
                                TreatmentTypeName = t.TreatmentType != null ? t.TreatmentType.TreatmentTypeName : string.Empty,
                                SurgeryName = t.Surgery != null ? t.Surgery.SurgeryName : string.Empty,
                                TherapyName = t.Therapy != null ? t.Therapy.TherapyName : string.Empty,
                                MedicalDeptName = t.MedicalDepartment != null ? t.MedicalDepartment.MedicalDepartmentName : string.Empty,
                                WardName = t.Ward != null ? t.Ward.WardName : string.Empty,
                                RoomName = t.Room != null ? t.Room.RoomName : string.Empty,
                                PaymentStatusName = (t.BillItem != null && t.BillItem.Bill != null && t.BillItem.Bill.PaymentStatus != null)
                                    ? t.BillItem.Bill.PaymentStatus.PaymentStatusName
                                    : "Unbilled"
                            }).ToList()
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            if (patient == null)
                return new();

            // Populate visit.Patient in memory (0 extra SQL queries, preserves exact structure)
            var visitPatientRef = new PatientDto
            {
                Id = patient.Id,
                FisrtName = patient.FisrtName,
                MiddleName = patient.MiddleName,
                LastName = patient.LastName,
                PatientCode = patient.PatientCode,
                PatientPhoto = patient.PatientPhoto,
                Age = patient.Age,
                PhoneNumber = patient.PhoneNumber,
                EmailAddress = patient.EmailAddress,
                HomeAddress = patient.HomeAddress,
                RegisteredDate = patient.RegisteredDate,
                Gender = patient.Gender,
                KinFullName = patient.KinFullName,
                KinPhoneNumber = patient.KinPhoneNumber,
                KinHomeAddress = patient.KinHomeAddress,
                PatientStatusId = patient.PatientStatusId,
                PatientTypeId = patient.PatientTypeId,
                PatientFullName = patient.PatientFullName,
                PatientStatusName = patient.PatientStatusName,
                PatientTypeName = patient.PatientTypeName,
                PatientInsuarances = patient.PatientInsuarances.Where(pi => pi.IsActive).ToList()
            };

            foreach (var visit in patient.Visits)
            {
                visit.Patient = visitPatientRef;
            }

            return patient;
        }



        public async Task<VisitDto?> GetVisitByPCodeOrFullNameAsync(string pCodeOrFullName)
        {
            if (string.IsNullOrWhiteSpace(pCodeOrFullName))
                return null;

            var searchTerm = pCodeOrFullName.Trim();

            using var context = _contextFactory.CreateDbContext();

            // STEP 1: Fast, lightweight lookup to get ONLY the Visit Id (no heavy joins or photos)
            // Try exact PatientCode match first (uses index on PatientCode)
            var targetVisitId = await context.Visits
                .AsNoTracking()
                .Where(v => v.VisitStatusId == 1 &&
                            v.Patient != null &&
                            v.Patient.PatientCode == searchTerm)
                .OrderByDescending(v => v.VisitDate)
                .Select(v => (int?)v.Id)
                .FirstOrDefaultAsync();

            // Fallback to FullName search if no exact PatientCode matched
            if (targetVisitId == null)
            {
                targetVisitId = await context.Visits
                    .AsNoTracking()
                    .Where(v => v.VisitStatusId == 1 &&
                                v.Patient != null &&
                                EF.Functions.Like(v.Patient.PatientFullName, $"%{searchTerm}%"))
                    .OrderByDescending(v => v.VisitDate)
                    .Select(v => (int?)v.Id)
                    .FirstOrDefaultAsync();
            }

            if (targetVisitId == null)
                return null;

            // STEP 2: Fetch full VisitDto by Primary Key (instant clustered index seek across all split queries)
            var visit = await context.Visits
                .AsNoTracking()
                .AsSplitQuery()
                .Where(v => v.Id == targetVisitId.Value)
                .Select(v => new VisitDto
                {
                    Id = v.Id,
                    VisitDate = v.VisitDate,
                    DischargedDate = v.DischargedDate,
                    VisitTypeId = v.VisitTypeId,
                    PatientId = v.PatientId,
                    VisitStatusId = v.VisitStatusId,

                    // Payment Mode & Insurance Info
                    PaymentModeId = v.PaymentModeId,
                    PatientInsuaranceId = v.PatientInsuaranceId,
                    RegisteredPaymentMode = v.PaymentMode != null ? v.PaymentMode.PaymentModeName : "Cash",
                    InsuranceProviderName = (v.PatientInsuarance != null && v.PatientInsuarance.Insuarance != null)
                        ? v.PatientInsuarance.Insuarance.InsuaranceName
                        : string.Empty,
                    InsuranceSchemeName = (v.PatientInsuarance != null && v.PatientInsuarance.InsuranceScheme != null)
                        ? v.PatientInsuarance.InsuranceScheme.SchemeName
                        : string.Empty,
                    MemberCardNumber = v.PatientInsuarance != null ? v.PatientInsuarance.MemberCardNumber : string.Empty,
                    AuthorizationNumber = v.VisitInsuranceAuth != null ? v.VisitInsuranceAuth.AuthorizationNumber : string.Empty,

                    // UI Helpers
                    VisitTypeName = v.VisitType != null ? v.VisitType.TypeName : string.Empty,
                    PatientFullName = v.Patient != null ? v.Patient.PatientFullName : string.Empty,
                    VisitStatusName = v.VisitStatus != null ? v.VisitStatus.VisitStatusName : string.Empty,
                    PreliminaryTestId = v.PreliminaryTest != null ? v.PreliminaryTest.Id : null,

                    // 1. Patient
                    Patient = v.Patient == null ? null : new PatientDto
                    {
                        Id = v.Patient.Id,
                        FisrtName = v.Patient.FirstName,
                        MiddleName = v.Patient.MiddleName,
                        LastName = v.Patient.LastName,
                        PatientCode = v.Patient.PatientCode,
                        PatientPhoto = v.Patient.PatientPhoto,
                        Age = v.Patient.Age,
                        PhoneNumber = v.Patient.PhoneNumber,
                        EmailAddress = v.Patient.EmailAddress,
                        HomeAddress = v.Patient.HomeAddress,
                        RegisteredDate = v.Patient.RegisteredDate,
                        Gender = v.Patient.Gender,
                        KinFullName = v.Patient.KinFullName,
                        KinPhoneNumber = v.Patient.KinPhoneNumber,
                        KinHomeAddress = v.Patient.KinHomeAddress,
                        PatientStatusId = v.Patient.PatientStatusId,
                        PatientTypeId = v.Patient.PatientTypeId,
                        PatientFullName = v.Patient.PatientFullName,
                        PatientStatusName = v.Patient.PatientStatus != null ? v.Patient.PatientStatus.PatientStatusName : string.Empty,
                        PatientTypeName = v.Patient.PatientType != null ? v.Patient.PatientType.PatientTypeName : string.Empty,
                        PatientInsuarances = v.Patient.PatientInsuarances
                            .Where(pi => pi.IsActive)
                            .Select(pi => new PatientInsuaranceDto
                            {
                                PatientInsuaranceId = pi.PatientInsuaranceId,
                                PatientId = pi.PatientId,
                                InsuaranceId = pi.InsuaranceId,
                                InsuranceSchemeId = pi.InsuranceSchemeId,
                                MemberCardNumber = pi.MemberCardNumber,
                                PrincipalMemberName = pi.PrincipalMemberName,
                                RelationshipToPrincipal = pi.RelationshipToPrincipal,
                                IsPrimary = pi.IsPrimary,
                                IsActive = pi.IsActive,
                                InsuranceProviderName = pi.Insuarance != null ? pi.Insuarance.InsuaranceName : string.Empty,
                                InsuranceSchemeName = pi.InsuranceScheme != null ? pi.InsuranceScheme.SchemeName : string.Empty,
                                DefaultCoveragePercent = pi.InsuranceScheme != null ? pi.InsuranceScheme.DefaultCoveragePercent : 100m
                            }).ToList()
                    },

                    // 2. Preliminary Test (Vitals)
                    PreliminaryTest = v.PreliminaryTest == null ? null : new PreliminaryTestDto
                    {
                        Id = v.PreliminaryTest.Id,
                        //HeartRate = v.PreliminaryTest.HeartRate,
                        //RespiratoryRate = v.PreliminaryTest.RespiratoryRate,
                        //Temperature = v.PreliminaryTest.Temperature,
                        PreliminaryPressure = v.PreliminaryTest.PreliminaryPressure,
                        //OxygenSaturation = v.PreliminaryTest.OxygenSaturation,
                        PreliminaryHeight = v.PreliminaryTest.PreliminaryHeight,
                        PreliminaryWeight = v.PreliminaryTest.PreliminaryWeight,
                        VisitId = v.PreliminaryTest.VisitId,
                    },

                    // 3. Bills
                    Bills = v.Bills.Select(b => new BillDto
                    {
                        Id = b.Id,
                        BillDate = b.BillDate,
                        BillNumber = b.BillNumber,
                        BillDescription = b.BillDescription,
                        TotalAmount = b.TotalAmount,
                        TotalPatientAmount = b.TotalPatientAmount,
                        TotalInsuranceAmount = b.TotalInsuranceAmount,
                        PatientPaidAmount = b.PatientPaidAmount,
                        InsurancePaidAmount = b.InsurancePaidAmount,
                        PaidAmount = b.PatientPaidAmount + b.InsurancePaidAmount,
                        VisitId = b.VisitId,
                        BillTypeId = b.BillTypeId,
                        PaymentModeId = b.PaymentModeId,
                        PaymentStatusId = b.PaymentStatusId,
                        PatientInsuaranceId = b.PatientInsuaranceId,
                        StaffId = b.StaffId,
                        BillTypeName = b.BillType != null ? b.BillType.TypeNme : string.Empty,
                        PaymentModeName = b.PaymentMode != null ? b.PaymentMode.PaymentModeName : "Cash",
                        PaymentStatusName = b.PaymentStatus != null ? b.PaymentStatus.PaymentStatusName : "Unpaid",
                        StaffFullName = b.Staff != null ? b.Staff.FisrtName : string.Empty, //full name letter
                        BillItems = b.BillItems.Select(bi => new BillItemDto
                        {
                            Id = bi.Id,
                            BillId = bi.BillId,
                            ItemName = bi.ItemName,
                            SourceType = bi.SourceType,
                            SourceItemId = bi.SourceItemId,
                            InsurerItemCode = bi.InsurerItemCode,
                            Quantity = bi.Quantity,
                            UnitPrice = bi.UnitPrice,
                            IsInsuranceCovered = bi.IsInsuranceCovered,
                            InsuranceCoveragePercent = bi.InsuranceCoveragePercent,
                            InsuranceCoveredAmount = bi.InsuranceCoveredAmount,
                            PatientPayableAmount = bi.PatientPayableAmount,
                            ClaimItemStatus = bi.ClaimItemStatus,
                            RejectionReason = bi.RejectionReason
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
                        VisitId = c.VisitId,
                        RoomId = c.RoomId,
                        DoctorId = c.DoctorId,
                        MedicalDepartmentId = c.MedicalDepartmentId,
                        BillItemId = c.BillItemId,
                        MedicalDepartmentName = c.MedicalDepartment != null ? c.MedicalDepartment.MedicalDepartmentName : string.Empty,
                        RoomName = c.Room != null ? c.Room.RoomName : string.Empty,
                        //DoctorName = c.Doctor != null ? c.Doctor.FullName : string.Empty,
                        PaymentStatusName = (c.BillItem != null && c.BillItem.Bill != null && c.BillItem.Bill.PaymentStatus != null)
                            ? c.BillItem.Bill.PaymentStatus.PaymentStatusName
                            : "Unbilled"
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
                        VisitId = l.VisitId,
                        ConsultationId = l.ConsultationId,
                        BillItemId = l.BillItemId,
                        PaymentStatusName = (l.BillItem != null && l.BillItem.Bill != null && l.BillItem.Bill.PaymentStatus != null)
                            ? l.BillItem.Bill.PaymentStatus.PaymentStatusName
                            : "Unbilled"
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
                        VisitId = p.VisitId,
                        ConsultationId = p.ConsultationId,
                        MedicineId = p.MedicineId,
                        DoctorId = p.DoctorId,
                        BillItemId = p.BillItemId,
                        MedicineName = p.Medicine != null ? p.Medicine.MedicineName : string.Empty,
                        //DoctorName = p.Doctor != null ? p.Doctor.FullName : string.Empty,
                        PaymentStatusName = (p.BillItem != null && p.BillItem.Bill != null && p.BillItem.Bill.PaymentStatus != null)
                            ? p.BillItem.Bill.PaymentStatus.PaymentStatusName
                            : "Unbilled"
                    }).ToList(),

                    // 7. Treatments
                    Treatments = v.Treatments.Select(t => new TreatmentDto
                    {
                        Id = t.Id,
                        StartDate = t.StartDate,
                        FinishDate = t.FinishDate,
                        TreatmentQty = t.TreatmentQty,
                        TreatmentCost = t.TreatmentCost,
                        VisitId = t.VisitId,
                        ConsultationId = t.ConsultationId,
                        BillItemId = t.BillItemId,
                        SurgeryId = t.SurgeryId,
                        TherapyId = t.TherapyId,
                        MedicalDeptId = t.MedicalDeptId,
                        WardId = t.WardId,
                        DoctorId = t.DoctorId,
                        RoomId = t.RoomId,
                        TreatmentStatusId = t.TreatmentStatusId,
                        TreatmentTypeId = t.TreatmentTypeId,
                        TreatmentStatusName = t.TreatmentStatus != null ? t.TreatmentStatus.TreatmentStatusName : string.Empty,
                        TreatmentTypeName = t.TreatmentType != null ? t.TreatmentType.TreatmentTypeName : string.Empty,
                        SurgeryName = t.Surgery != null ? t.Surgery.SurgeryName : string.Empty,
                        TherapyName = t.Therapy != null ? t.Therapy.TherapyName : string.Empty,
                        MedicalDeptName = t.MedicalDepartment != null ? t.MedicalDepartment.MedicalDepartmentName : string.Empty,
                        WardName = t.Ward != null ? t.Ward.WardName : string.Empty,
                        //DoctorName = t.Doctor != null ? t.Doctor.FullName : string.Empty,
                        RoomName = t.Room != null ? t.Room.RoomName : string.Empty,
                        PaymentStatusName = (t.BillItem != null && t.BillItem.Bill != null && t.BillItem.Bill.PaymentStatus != null)
                            ? t.BillItem.Bill.PaymentStatus.PaymentStatusName
                            : "Unbilled"
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return visit;
        }


        public async Task<bool> UpdatePatientAsync(PatientDto dto, int patientId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingPatient = await context.Patients
                    .Include(p => p.PatientDiseases)
                    .Include(p => p.DoctorPatients)
                    .Include(p => p.PatientInsuarances)
                    .FirstOrDefaultAsync(x => x.Id == patientId);

                if (existingPatient == null)
                    return false;

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
                existingPatient.PatientFullName = $"{dto.FisrtName} {dto.MiddleName} {dto.LastName}".Replace("  ", " ").Trim();
                existingPatient.Gender = dto.Gender;
                existingPatient.KinFullName = dto.KinFullName;
                existingPatient.KinPhoneNumber = dto.KinPhoneNumber;
                existingPatient.KinHomeAddress = dto.KinHomeAddress;
                existingPatient.PatientStatusId = dto.PatientStatusId;
                existingPatient.PatientTypeId = dto.PatientTypeId;

                if (dto.PatientDiseases != null)
                {
                    existingPatient.PatientDiseases.Clear();
                    foreach (var d in dto.PatientDiseases)
                    {
                        existingPatient.PatientDiseases.Add(new PatientDisease
                        {
                            DiseaseId = d.DiseaseId,
                            PatientId = patientId
                        });
                    }
                }

                if (dto.DoctorPatients != null)
                {
                    existingPatient.DoctorPatients.Clear();
                    foreach (var dp in dto.DoctorPatients)
                    {
                        existingPatient.DoctorPatients.Add(new DoctorPatient
                        {
                            DoctorId = dp.DoctorId,
                            PatientId = patientId
                        });
                    }
                }

                // Safely update existing insurance cards & add new ones without breaking historical Visit/Bill FKs
                if (dto.PatientInsuarances != null)
                {
                    var incomingIds = dto.PatientInsuarances
                        .Where(x => x.PatientInsuaranceId > 0)
                        .Select(x => x.PatientInsuaranceId)
                        .ToHashSet();

                    // Deactivate removed policies instead of hard-deleting them (preserves historical bill links)
                    foreach (var oldPolicy in existingPatient.PatientInsuarances.Where(pi => !incomingIds.Contains(pi.PatientInsuaranceId)))
                    {
                        oldPolicy.IsActive = false;
                    }

                    foreach (var piDto in dto.PatientInsuarances.Where(pi => pi.InsuaranceId > 0 && !string.IsNullOrWhiteSpace(pi.MemberCardNumber)))
                    {
                        var existingPolicy = existingPatient.PatientInsuarances
                            .FirstOrDefault(pi => pi.PatientInsuaranceId == piDto.PatientInsuaranceId && piDto.PatientInsuaranceId > 0);

                        if (existingPolicy != null)
                        {
                            existingPolicy.InsuaranceId = piDto.InsuaranceId;
                            existingPolicy.InsuranceSchemeId = piDto.InsuranceSchemeId;
                            existingPolicy.MemberCardNumber = piDto.MemberCardNumber;
                            existingPolicy.PrincipalMemberName = piDto.PrincipalMemberName;
                            existingPolicy.RelationshipToPrincipal = string.IsNullOrWhiteSpace(piDto.RelationshipToPrincipal) ? "Self" : piDto.RelationshipToPrincipal;
                            existingPolicy.ValidFrom = piDto.ValidFrom;
                            existingPolicy.ValidTo = piDto.ValidTo;
                            existingPolicy.IsPrimary = piDto.IsPrimary;
                            existingPolicy.IsActive = piDto.IsActive;
                        }
                        else
                        {
                            existingPatient.PatientInsuarances.Add(new PatientInsuarance
                            {
                                PatientId = patientId,
                                InsuaranceId = piDto.InsuaranceId,
                                InsuranceSchemeId = piDto.InsuranceSchemeId,
                                MemberCardNumber = piDto.MemberCardNumber,
                                PrincipalMemberName = piDto.PrincipalMemberName,
                                RelationshipToPrincipal = string.IsNullOrWhiteSpace(piDto.RelationshipToPrincipal) ? "Self" : piDto.RelationshipToPrincipal,
                                ValidFrom = piDto.ValidFrom,
                                ValidTo = piDto.ValidTo,
                                IsPrimary = piDto.IsPrimary,
                                IsActive = true
                            });
                        }
                    }
                }

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating patient: {ex}");
                return false;
            }
        }


        public async Task<List<VisitDto>> GetAllVisitsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var visits = await context.Visits
                .AsNoTracking()
                .AsSplitQuery()
                //.OrderByDescending(v => v.VisitDate)
                .Select(visit => new VisitDto
                {
                    Id = visit.Id,
                    VisitDate = visit.VisitDate,
                    VisitTypeId = visit.VisitTypeId,
                    VisitTypeName = visit.VisitType != null ? visit.VisitType.TypeName : string.Empty,
                    PatientId = visit.PatientId,
                    PatientFullName = visit.Patient != null ? visit.Patient.PatientFullName : string.Empty,
                    VisitStatusId = visit.VisitStatusId,
                    VisitStatusName = visit.VisitStatus != null ? visit.VisitStatus.VisitStatusName : string.Empty,
                    DischargedDate = visit.DischargedDate,

                    // Payment Mode & Insurance Details
                    PaymentModeId = visit.PaymentModeId,
                    PatientInsuaranceId = visit.PatientInsuaranceId,
                    RegisteredPaymentMode = visit.PaymentMode != null ? visit.PaymentMode.PaymentModeName : "Cash",
                    InsuranceProviderName = (visit.PatientInsuarance != null && visit.PatientInsuarance.Insuarance != null)
                        ? visit.PatientInsuarance.Insuarance.InsuaranceName
                        : string.Empty,
                    InsuranceSchemeName = (visit.PatientInsuarance != null && visit.PatientInsuarance.InsuranceScheme != null)
                        ? visit.PatientInsuarance.InsuranceScheme.SchemeName
                        : string.Empty,
                    MemberCardNumber = visit.PatientInsuarance != null ? visit.PatientInsuarance.MemberCardNumber : string.Empty,
                    AuthorizationNumber = visit.VisitInsuranceAuth != null ? visit.VisitInsuranceAuth.AuthorizationNumber : string.Empty,

                    Bills = visit.Bills.Select(bill => new BillDto
                    {
                        Id = bill.Id,
                        BillDate = bill.BillDate,
                        BillNumber = bill.BillNumber,
                        BillDescription = bill.BillDescription,
                        VisitId = bill.VisitId,
                        TotalAmount = bill.TotalAmount,
                        TotalPatientAmount = bill.TotalPatientAmount,
                        TotalInsuranceAmount = bill.TotalInsuranceAmount,
                        PaymentModeId = bill.PaymentModeId,
                        PaymentModeName = bill.PaymentMode != null ? bill.PaymentMode.PaymentModeName : "Cash",
                        PaymentStatusId = bill.PaymentStatusId,
                        PaymentStatusName = bill.PaymentStatus != null ? bill.PaymentStatus.PaymentStatusName : "Unpaid"
                    }).ToList(),

                    Patient = visit.Patient == null ? null : new PatientDto
                    {
                        Id = visit.Patient.Id,
                        PatientCode = visit.Patient.PatientCode,
                        PatientTypeName = visit.Patient.PatientType != null ? visit.Patient.PatientType.PatientTypeName : string.Empty,
                        PatientFullName = visit.Patient.PatientFullName,
                        PatientStatusName = visit.Patient.PatientStatus != null ? visit.Patient.PatientStatus.PatientStatusName : string.Empty
                    }
                }).ToListAsync();

            return visits;
        }

        public async Task<int> AddVisitAsync(VisitDto dto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                int effectivePaymentModeId = dto.PaymentModeId > 0 ? dto.PaymentModeId : 1; // Default 1 = Cash

                var visit = new Visit
                {
                    VisitDate = dto.VisitDate == default ? DateTime.UtcNow : dto.VisitDate,
                    VisitTypeId = dto.VisitTypeId,
                    PatientId = dto.PatientId,
                    FullName = dto.PatientFullName,
                    VisitStatusId = 1, // 1 = Active
                    PaymentModeId = effectivePaymentModeId,
                    PatientInsuaranceId = effectivePaymentModeId == 2 ? dto.PatientInsuaranceId : null
                };

                var result = context.Visits.Add(visit);
                await context.SaveChangesAsync();

                return result.Entity.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding visit: {ex}");
                return 0;
            }
        }


        public async Task<bool> UpdateVisitAsync(VisitDto dto, int visitId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingVisit = await context.Visits.FirstOrDefaultAsync(x => x.Id == visitId);

                if (existingVisit == null)
                    return false;

                existingVisit.VisitTypeId = dto.VisitTypeId;
                existingVisit.PatientId = dto.PatientId;
                existingVisit.DischargedDate = dto.DischargedDate;
                existingVisit.VisitStatusId = dto.VisitStatusId;
                existingVisit.FullName = dto.PatientFullName;

                if (dto.PaymentModeId > 0)
                {
                    existingVisit.PaymentModeId = dto.PaymentModeId;
                    existingVisit.PatientInsuaranceId = dto.PaymentModeId == 2
                        ? dto.PatientInsuaranceId
                        : null;
                }

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating visit: {ex}");
                return false;
            }
        }


        public async Task<List<VisitTypeDto>> GetAllVisitTypesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var visits = await context.VisitTypes
                .AsNoTracking()
                .AsSplitQuery()
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
                .AsSplitQuery()
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

                dto.Id = preliminaryTest.Id;
                return dto;
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

                if (existingConsultation == null)
                    return false;

                existingConsultation.Diagnosis = dto.Diagnosis;

                if (dto.StartDate != default)
                    existingConsultation.StartDate = dto.StartDate;

                existingConsultation.FinishDate = dto.FinishDate ?? existingConsultation.FinishDate;

                if (dto.ConsultationCost > 0)
                    existingConsultation.ConsultationCost = dto.ConsultationCost;

                if (dto.VisitId > 0)
                    existingConsultation.VisitId = dto.VisitId;

                existingConsultation.RoomId = dto.RoomId ?? existingConsultation.RoomId;
                existingConsultation.DoctorId = dto.DoctorId ?? existingConsultation.DoctorId;
                existingConsultation.MedicalDepartmentId = dto.MedicalDepartmentId ?? existingConsultation.MedicalDepartmentId;
                existingConsultation.BillItemId = dto.BillItemId ?? existingConsultation.BillItemId;

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating consultation: {ex}");
                return false;
            }
        }


        public async Task<ConsultationDto> GetConsultationByIdAsync(int conId)
        {
            using var context = _contextFactory.CreateDbContext();

            var cons = await context.Consultations
                .AsNoTracking()
                .AsSplitQuery()
                .Where(p => p.Id == conId)
                .Select(consDto => new ConsultationDto
                {
                    Id = consDto.Id,
                    Diagnosis = consDto.Diagnosis,
                    StartDate = consDto.StartDate,
                    FinishDate = consDto.FinishDate,
                    ConsultationCost = consDto.ConsultationCost,
                    VisitId = consDto.VisitId,
                    RoomId = consDto.RoomId,
                    BillItemId = consDto.BillItemId,
                    DoctorId = consDto.DoctorId,
                    MedicalDepartmentId = consDto.MedicalDepartmentId,
                    MedicalDepartmentName = consDto.MedicalDepartment != null ? consDto.MedicalDepartment.MedicalDepartmentName : string.Empty,
                    DoctorFullName = consDto.Doctor != null ? consDto.Doctor.Staff!.FisrtName : string.Empty,
                    VisitTypeName = consDto.Visit != null ? consDto.Visit.VisitType!.TypeName : string.Empty,
                    RoomName = consDto.Room != null ? consDto.Room.RoomName : string.Empty,
                    PaymentStatusName = consDto.Visit != null ? consDto.Visit.FullName : string.Empty,
                })
                .FirstOrDefaultAsync();

            return cons ?? new();
        }

        public async Task<List<ConsultationDto>> GetAllConsultationsAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            var cons = await context.Consultations
                .AsNoTracking()
                .AsSplitQuery()
                //.OrderByDescending(c => c.StartDate)
                .Select(consDto => new ConsultationDto
                {
                    Id = consDto.Id,
                    Diagnosis = consDto.Diagnosis,
                    StartDate = consDto.StartDate,
                    FinishDate = consDto.FinishDate,
                    ConsultationCost = consDto.ConsultationCost,
                    VisitId = consDto.VisitId,
                    RoomId = consDto.RoomId,
                    BillItemId = consDto.BillItemId,
                    DoctorId = consDto.DoctorId,
                    MedicalDepartmentId = consDto.MedicalDepartmentId,
                    MedicalDepartmentName = consDto.MedicalDepartment != null ? consDto.MedicalDepartment.MedicalDepartmentName : string.Empty,
                    DoctorFullName = consDto.Doctor != null && consDto.Doctor.Staff != null
                        ? $"{consDto.Doctor.Staff.FisrtName} {consDto.Doctor.Staff.MiddleName} {consDto.Doctor.Staff.LastName}".Replace("  ", " ").Trim()
                        : string.Empty,
                    VisitTypeName = consDto.Visit != null && consDto.Visit.VisitType != null ? consDto.Visit.VisitType.TypeName : string.Empty,
                    RoomName = consDto.Room != null ? consDto.Room.RoomName : string.Empty,
                    PaymentStatusName = (consDto.BillItem != null && consDto.BillItem.Bill != null && consDto.BillItem.Bill.PaymentStatus != null)
                        ? consDto.BillItem.Bill.PaymentStatus.PaymentStatusName
                        : "Unpaid",
                    IsReadyForService =
                        (consDto.BillItem != null && consDto.BillItem.Bill != null && consDto.BillItem.Bill.PaymentStatusId == 3)
                        ||
                        (consDto.BillItem != null && consDto.BillItem.Bill != null &&
                         consDto.BillItem.Bill.PaymentModeId == 2 &&
                         consDto.BillItem.Bill.PatientPaidAmount >= consDto.BillItem.Bill.TotalPatientAmount)
                })
                .ToListAsync();

            return cons ?? new();
        }


        public async Task<int> AddPatientInsuranceAsync(PatientInsuaranceDto dto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                if (dto.IsPrimary)
                {
                    var existingPrimaries = await context.PatientInsuarances
                        .Where(pi => pi.PatientId == dto.PatientId && pi.IsPrimary)
                        .ToListAsync();

                    foreach (var p in existingPrimaries)
                    {
                        p.IsPrimary = false;
                    }
                }

                var policy = new PatientInsuarance
                {
                    PatientId = dto.PatientId,
                    InsuaranceId = dto.InsuaranceId,
                    InsuranceSchemeId = dto.InsuranceSchemeId,
                    MemberCardNumber = dto.MemberCardNumber,
                    PrincipalMemberName = dto.PrincipalMemberName,
                    RelationshipToPrincipal = string.IsNullOrWhiteSpace(dto.RelationshipToPrincipal) ? "Self" : dto.RelationshipToPrincipal,
                    ValidFrom = dto.ValidFrom,
                    ValidTo = dto.ValidTo,
                    IsPrimary = dto.IsPrimary,
                    IsActive = true
                };

                context.PatientInsuarances.Add(policy);
                await context.SaveChangesAsync();

                return policy.PatientInsuaranceId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding patient insurance: {ex}");
                return 0;
            }
        }


        public async Task<List<PatientInsuaranceDto>> GetPatientInsurancesAsync(int patientId)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.PatientInsuarances
                .AsNoTracking()
                .AsSplitQuery()
                .Where(pi => pi.PatientId == patientId && pi.IsActive)
                //.OrderByDescending(pi => pi.IsPrimary)
                .Select(pi => new PatientInsuaranceDto
                {
                    PatientInsuaranceId = pi.PatientInsuaranceId,
                    PatientId = pi.PatientId,
                    InsuaranceId = pi.InsuaranceId,
                    InsuranceSchemeId = pi.InsuranceSchemeId,
                    MemberCardNumber = pi.MemberCardNumber,
                    PrincipalMemberName = pi.PrincipalMemberName,
                    RelationshipToPrincipal = pi.RelationshipToPrincipal,
                    ValidFrom = pi.ValidFrom,
                    ValidTo = pi.ValidTo,
                    IsPrimary = pi.IsPrimary,
                    IsActive = pi.IsActive,
                    PatientFullName = pi.Patient != null ? pi.Patient.PatientFullName : string.Empty,
                    InsuranceProviderName = pi.Insuarance != null ? pi.Insuarance.InsuaranceName : string.Empty,
                    InsuranceSchemeName = pi.InsuranceScheme != null ? pi.InsuranceScheme.SchemeName : string.Empty,
                    DefaultCoveragePercent = pi.InsuranceScheme != null ? pi.InsuranceScheme.DefaultCoveragePercent : 100m
                })
                .ToListAsync();
        }


        public async Task<VisitInsuranceAuthDto> SaveVisitInsuranceAuthAsync(VisitInsuranceAuthDto dto)
        {
            using var context = _contextFactory.CreateDbContext();

            var auth = await context.VisitInsuranceAuths
                .FirstOrDefaultAsync(a => a.VisitId == dto.VisitId);

            if (auth == null)
            {
                auth = new VisitInsuranceAuth
                {
                    VisitId = dto.VisitId,
                    AuthorizedAt = DateTime.UtcNow
                };
                context.VisitInsuranceAuths.Add(auth);
            }

            auth.PatientInsuaranceId = dto.PatientInsuaranceId;
            auth.AuthorizationNumber = dto.AuthorizationNumber;
            auth.CardNumber = dto.CardNumber;
            auth.ReferralNumber = dto.ReferralNumber;
            auth.SchemeCode = dto.SchemeCode;
            auth.ApprovedLimit = dto.ApprovedLimit;
            auth.IsVerified = dto.IsVerified;
            auth.VerificationStatus = string.IsNullOrWhiteSpace(dto.VerificationStatus)
                ? (dto.IsVerified ? "Verified" : "Pending")
                : dto.VerificationStatus;
            auth.VerificationRemarks = dto.VerificationRemarks;
            auth.ExpiresAt = dto.ExpiresAt;

            // Keep the parent Visit aligned with this insurance policy
            var visit = await context.Visits.FirstOrDefaultAsync(v => v.Id == dto.VisitId);
            if (visit != null)
            {
                visit.PaymentModeId = 2; // 2 = Insurance
                visit.PatientInsuaranceId = dto.PatientInsuaranceId;
            }

            await context.SaveChangesAsync();

            dto.Id = auth.Id;
            return dto;
        }


        public async Task<VisitInsuranceAuthDto?> GetVisitInsuranceAuthAsync(int visitId)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.VisitInsuranceAuths
                .AsNoTracking()
                .AsSplitQuery()
                .Where(a => a.VisitId == visitId)
                .Select(a => new VisitInsuranceAuthDto
                {
                    Id = a.Id,
                    VisitId = a.VisitId,
                    PatientInsuaranceId = a.PatientInsuaranceId,
                    AuthorizationNumber = a.AuthorizationNumber,
                    CardNumber = a.CardNumber,
                    ReferralNumber = a.ReferralNumber,
                    SchemeCode = a.SchemeCode,
                    ApprovedLimit = a.ApprovedLimit,
                    IsVerified = a.IsVerified,
                    VerificationStatus = a.VerificationStatus,
                    VerificationRemarks = a.VerificationRemarks,
                    AuthorizedAt = a.AuthorizedAt,
                    ExpiresAt = a.ExpiresAt,
                    PatientFullName = a.Visit != null && a.Visit.Patient != null
                        ? a.Visit.Patient.PatientFullName
                        : string.Empty,
                    InsuranceProviderName = a.PatientInsuarance != null && a.PatientInsuarance.Insuarance != null
                        ? a.PatientInsuarance.Insuarance.InsuaranceName
                        : string.Empty,
                    InsuranceSchemeName = a.PatientInsuarance != null && a.PatientInsuarance.InsuranceScheme != null
                        ? a.PatientInsuarance.InsuranceScheme.SchemeName
                        : string.Empty
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdatePreliminaryTestAsync(PreliminaryTestDto dto, int Id)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingVital = await context.PreliminaryTests.FirstOrDefaultAsync(x => x.Id == Id);

                if (existingVital == null)
                    return false;

                existingVital.PreliminaryPressure = dto.PreliminaryPressure;
                existingVital.PreliminaryHeight = dto.PreliminaryHeight;
                existingVital.PreliminaryWeight = dto.PreliminaryWeight;

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating visit: {ex}");
                return false;
            }
        }

        public async Task<int> GetVisitIdByPatientIdAsync(int pId)
        {
            using var context = _contextFactory.CreateDbContext();

            var visit = await context.Visits
                .AsNoTracking()
                .AsSplitQuery()
                .Where(v => v.PatientId == pId)
                .Select(v => new VisitDto
                {
                    Id = v.Id,
               
                }).FirstOrDefaultAsync();
            return visit.Id;
        }

        public async Task<List<PrescriptionDto>> GetPreviousPrescriptionsAsync(int patientId)
        {
            using var context = _contextFactory.CreateDbContext();

            var prescriptions = await context.Prescriptions
                .AsNoTracking()
                .AsSplitQuery()
                .Where(p => p.Visit.PatientId == patientId)
                //.OrderByDescending(p => p.PrescribedDate)
                .Select(p => new PrescriptionDto
                {
                    Id = p.Id,
                    PrescribedDate = p.PrescribedDate,
                    Quantity = p.Quantity,
                    Dosage = p.Dosage,
                    IsDispensed = p.IsDispensed,
                    ConsultationId = p.ConsultationId,
                    Notes = p.Notes,
                    MedicineName = p.Medicine != null ? p.Medicine.MedicineName : string.Empty,
                    IsReadyForService =
                            (p.BillItem != null && p.BillItem.Bill != null && p.BillItem.Bill.PaymentStatusId == 3)
                            ||
                            (p.BillItem != null && p.BillItem.Bill != null &&
                             p.BillItem.Bill.PaymentModeId == 2 &&
                             p.BillItem.Bill.PatientPaidAmount >= p.BillItem.Bill.TotalPatientAmount)
                })
                .ToListAsync();

            return prescriptions;
        }


        public async Task<List<LabTestDto>> GetPreviousLabTestsAsync(int patientId)
        {
            using var context = _contextFactory.CreateDbContext();

            var tests = await context.LabTests
                .AsNoTracking()
                .AsSplitQuery()
                .Where(p => p.Visit.PatientId == patientId)
                //.OrderByDescending(p => p.CreatedDate)
                .Select(p => new LabTestDto
                {
                    Id = p.Id,
                    CreatedDate = p.CreatedDate,
                    ConsultationId = p.ConsultationId,
                    DiseaseName = p.DiseaseName,
                    TestResult = p.TestResult,
                    IsReadyForService =
                            (p.BillItem != null && p.BillItem.Bill != null && p.BillItem.Bill.PaymentStatusId == 3)
                            ||
                            (p.BillItem != null && p.BillItem.Bill != null &&
                             p.BillItem.Bill.PaymentModeId == 2 &&
                             p.BillItem.Bill.PatientPaidAmount >= p.BillItem.Bill.TotalPatientAmount)
                })
                .ToListAsync();

            return tests;
        }

        public async Task<List<TreatmentDto>> GetPreviousTreatmentsAsync(int patientId)
        {

            using var context = _contextFactory.CreateDbContext();

            var treats = await context.Treatments
                .AsNoTracking()
                .AsSplitQuery()
                .Where(p => p.Visit.PatientId == patientId)
                //.OrderByDescending(p => p.StartDate)
                .Select(p => new TreatmentDto
                {
                    Id = p.Id,
                    StartDate = p.StartDate,
                    ConsultationId = p.ConsultationId,
                    FinishDate = p.FinishDate,
                    TreatmentTypeName = p.TreatmentType != null ? p.TreatmentType.TreatmentTypeName : string.Empty,
                    SurgeryName = p.Surgery != null ? p.Surgery.SurgeryName : string.Empty,
                    TherapyName = p.Therapy != null ? p.Therapy.TherapyName : string.Empty,
                    IsReadyForService =
                            (p.BillItem != null && p.BillItem.Bill != null && p.BillItem.Bill.PaymentStatusId == 3)
                            ||
                            (p.BillItem != null && p.BillItem.Bill != null &&
                             p.BillItem.Bill.PaymentModeId == 2 &&
                             p.BillItem.Bill.PatientPaidAmount >= p.BillItem.Bill.TotalPatientAmount)
                })
                .ToListAsync();

            return treats;
        }

        public async Task<List<DoctorDto>> GetOtherDoctorAsync(int patientId)
        {
            using var context = _contextFactory.CreateDbContext();

            var treats = await context.Doctors
                .AsNoTracking()
                .AsSplitQuery()
                .Where(p => p.DoctorPatients.Any(dp => dp.PatientId == patientId))
                .Select(p => new DoctorDto
                {
                    DoctorFullName = $"{p.Staff!.FisrtName} {p.Staff!.MiddleName} {p.Staff!.LastName}",
                })
                .ToListAsync();

            return treats;
        }

        public async Task<List<AllergyDto>> GetAllergiesAsync(int patientId)
        {
            using var context = _contextFactory.CreateDbContext();

            var treats = await context.Allergies
                .AsNoTracking()
                .AsSplitQuery()
                .Where(p => p.PatientId == patientId)
                .Select(p => new AllergyDto
                {
                    AllergyName = p.AllergyName,
                    Description = p.Description
                })
                .ToListAsync();

            return treats;
        }

        public async Task<bool> AddAllergyAsync(AllergyDto dto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var newAllergy = new Allergy
                {
                    PatientId = dto.PatientId,
                    AllergyName = dto.AllergyName,
                    Description = dto.Description
                };

                var result = context.Allergies.Add(newAllergy);
                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<AllergyDto> GetAllergyByNameAsync(string allergyName)
        {
            using var context = _contextFactory.CreateDbContext();

            var allergy = await context.Allergies
                .AsNoTracking()
                .AsSplitQuery()
                .Where(p => p.AllergyName.ToLower().Trim() == allergyName.ToLower().Trim())
                .Select(p => new AllergyDto
                {
                    Id = p.Id,
                    AllergyName = p.AllergyName,
                    PatientId = p.PatientId,
                    Description = p.Description
                })
                .FirstOrDefaultAsync();

            return allergy ?? new();
        }

        public async Task<ReferralDto> AddReferralLetterAsync(ReferralDto dto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var newAllergy = new Referral
                {
                    DoctorFullName = dto.DoctorFullName,
                    PatientFullName = dto.PatientFullName,
                    IssuedDate = DateTime.UtcNow,
                    ConsultationId = dto.ConsultationId,
                    DoctorsNote = dto.DoctorsNote
                };

                var result = context.Referrals.Add(newAllergy);
                await context.SaveChangesAsync();

                dto.Id = result.Entity.Id;
                return dto;
            }
            catch
            {
                return new();
            }
        }

        public async Task<ReferralDto> GetReferralByConsultationIdAsync(int consId)
        {
            using var context = _contextFactory.CreateDbContext();

            var referral = await context.Referrals
                .AsNoTracking()
                .AsSplitQuery()
                .Where(p => p.ConsultationId == consId)
                .Select(p => new ReferralDto
                {
                    Id = p.Id,
                    DoctorFullName = p.DoctorFullName,
                    PatientFullName = p.PatientFullName,
                    IssuedDate = p.IssuedDate,
                    ConsultationId = p.ConsultationId,
                    DoctorsNote = p.DoctorsNote
                })
                .FirstOrDefaultAsync();

            return referral ?? new();
        }
    }
}
