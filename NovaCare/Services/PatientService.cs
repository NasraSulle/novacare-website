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
using NovaCare.Shared.DTOs.PatientDTOs;
using NovaCare.Shared.DTOs.ReportDTOs;
using NovaCare.Shared.DTOs.StaffDTOs;

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
                    FisrtName = patient.FisrtName,
                    MiddleName = patient.MiddleName,
                    LastName = patient.LastName,
                    PhoneNumber = patient.PhoneNumber,
                    RegisteredDate = patient.RegisteredDate,
                    PatientStatusName = patient.PatientStatus!.PatientStatusName,
                    PatientTypeName = patient.PatientType!.PatientTypeName,
                    Gender = patient.Gender,
                    Age = patient.Age,
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
                    FisrtName = dto.FisrtName,
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
                    KinHomeAddress = dto.KinHomeAddress,
                    PatientStatusId = dto.PatientStatusId,
                    PatientTypeId = dto.PatientTypeId,
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
                    FisrtName = patient.FisrtName,
                    MiddleName = patient.MiddleName,
                    LastName = patient.LastName,
                    PhoneNumber = patient.PhoneNumber,
                    RegisteredDate = patient.RegisteredDate,
                    PatientStatusName = patient.PatientStatus!.PatientStatusName,
                    PatientTypeName = patient.PatientType!.PatientTypeName,
                    Gender = patient.Gender,
                    Age = patient.Age,
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
                    FisrtName = patient.FisrtName,
                    MiddleName = patient.MiddleName,
                    LastName = patient.LastName,
                    PhoneNumber = patient.PhoneNumber,
                    RegisteredDate = patient.RegisteredDate,
                    PatientStatusName = patient.PatientStatus!.PatientStatusName,
                    PatientTypeName = patient.PatientType!.PatientTypeName,
                    Gender = patient.Gender,
                    Age = patient.Age,
                })
                .FirstOrDefaultAsync(x => x.Id == patientId);

            return patient ?? new();
        }

        public async Task<bool> UpdatePatientAsync(PatientDto dto, int patientId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingPatient = await context.Patients.FirstOrDefaultAsync(x => x.Id == patientId);

                if (existingPatient != null)
                {
                    existingPatient.FisrtName = dto.FisrtName;
                    existingPatient.MiddleName = dto.MiddleName;
                    existingPatient.LastName = dto.LastName;
                    existingPatient.PatientCode = dto.PatientCode;
                    existingPatient.PatientPhoto = dto.PatientPhoto;
                    existingPatient.Age = dto.Age;
                    existingPatient.PhoneNumber = dto.PhoneNumber;
                    existingPatient.EmailAddress = dto.EmailAddress;
                    existingPatient.HomeAddress = dto.HomeAddress;
                    existingPatient.RegisteredDate = dto.RegisteredDate;
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
                        PatientId = x.PatientId
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
                    Bills = visit.Bills.Select(visit => new BillDto
                            {
                                Id = visit.Id,
                                BillDate = visit.BillDate,
                                BillDescription = visit.BillDescription,
                                VisitId = visit.VisitId,
                                TotalAmount = visit.TotalAmount

                            }).ToList()
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
                    existingVisit.PatientId = dto.PatientId;

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
                    TypeNme = visit.TypeNme
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
                    TypeNme = dto.TypeNme
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
                    existingVisitType.TypeNme = dto.TypeNme;

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
                    FisrtName = patient.FisrtName,
                    MiddleName = patient.MiddleName,
                    LastName = patient.LastName,
                    PhoneNumber = patient.PhoneNumber,
                    RegisteredDate = patient.RegisteredDate,
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
