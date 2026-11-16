using Microsoft.EntityFrameworkCore;
using NovaCare.Components.Pages.Medicine___Pharmacy_Pages;
using NovaCare.Data;
using NovaCare.Interfaces;
using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.MedicineModals;
using NovaCare.Modals.PatientModals;
using NovaCare.Modals.TreatmentModals;
using NovaCare.Shared.DTOs.DoctorDTOs;
using NovaCare.Shared.DTOs.MedicineDTOs;
using NovaCare.Shared.DTOs.PatientDTOs;
using NovaCare.Shared.DTOs.ReportDTOs;
using NovaCare.Shared.DTOs.TreatmentDTOs;

namespace NovaCare.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IDbContextFactory<NovaCareDbContext> _contextFactory;
        public MedicineService(IDbContextFactory<NovaCareDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<List<MedicineDto>> GetAllMedicinesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var medicines = await context.Medicines
                .AsNoTracking()
                .Select(medicine => new MedicineDto
                {
                    Id = medicine.Id,
                    Picture = medicine.Picture,
                    MedicineName = medicine.MedicineName,
                    MedicineGenre = medicine.MedicineGenre,
                    MedicineGenreId = medicine.MedicineGenreId,
                    MedicineType = medicine.MedicineType,
                    MedicineTypeId = medicine.MedicineTypeId,
                    CreatedDate = medicine.CreatedDate,
                    MedicineQty = medicine.MedicineQty,
                    BuyingPrice = medicine.BuyingPrice,
                    SellingPrice = medicine.SellingPrice,
                    MedicineStatusName = medicine.MedicineStatus!.MedicineStatusName,
                })
                .ToListAsync();

            return medicines;
        }
        public async Task<List<MedicineGenreDto>> GetAllMedicineGenresAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var medicineTypes = await context.MedicineGenres
                .AsNoTracking()
                .Select(type => new MedicineGenreDto
                {
                    Id = type.Id,
                    MedicineGenreName = type.MedicineGenreName,

                })
                .ToListAsync();

            return medicineTypes;
        }

        public async Task<List<MedicineTypeDto>> GetAllMedicineTypesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var medicineTypes = await context.MedicineTypes
                .AsNoTracking()
                .Select(type => new MedicineTypeDto
                {
                    Id = type.Id,
                    MedicineTypeName = type.MedicineTypeName,
                    MedicineGenreId = type.MedicineGenreId,
                    MedicineGenreName = type.MedicineGenre != null ? type.MedicineGenre.MedicineGenreName : string.Empty

                })
                .ToListAsync();

            return medicineTypes;
        }

        public async Task<bool> AddMedicineAsync(MedicineDto dto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var medicine = new Medicine  
                {
                    Picture = dto.Picture,
                    MedicineName = dto.MedicineName,
                    MedicineGenre = dto.MedicineGenre,
                    MedicineGenreId = dto.MedicineGenreId,
                    MedicineType = dto.MedicineType,
                    MedicineTypeId = dto.MedicineTypeId,
                    CreatedDate = dto.CreatedDate,
                    MedicineQty = dto.MedicineQty,
                    BuyingPrice = dto.BuyingPrice,
                    SellingPrice = dto.SellingPrice,
                    MedicineStatusId = 1,
                };

                context.Medicines.Add(medicine);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return false;
            }
        }

        public async Task<MedicineDto> GetMedicineIdByNameAsync(string name, string type, string genre)
        {
            using var context = _contextFactory.CreateDbContext();
            var medicine = await context.Medicines.Where(x => x.MedicineName.ToLower().Trim() == name.ToLower().Trim() && x.MedicineType.ToLower().Trim() == type.ToLower().Trim() && x.MedicineGenre.ToLower().Trim() == genre.ToLower().Trim())
                        .Select(med => new MedicineDto
                        {
                            Id = med.Id,
                            MedicineQty = med.MedicineQty
                        }).FirstOrDefaultAsync();

            return medicine ?? new();
        }

        public async Task<bool> UpdateMedicineAsync(MedicineDto dto, int medicineId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var existingMedicine = await context.Medicines.FirstOrDefaultAsync(x => x.Id == medicineId);

                //remember price difference later

                if(existingMedicine != null)
                {
                    existingMedicine.Picture = dto.Picture;
                    existingMedicine.MedicineName = dto.MedicineName;
                    existingMedicine.MedicineGenre = dto.MedicineGenre;
                    existingMedicine.MedicineGenreId = dto.MedicineGenreId;
                    existingMedicine.MedicineType = dto.MedicineType;
                    existingMedicine.MedicineTypeId = dto.MedicineTypeId;
                    existingMedicine.MedicineQty = dto.MedicineQty;
                    existingMedicine.BuyingPrice = dto.BuyingPrice;
                    existingMedicine.SellingPrice = dto.SellingPrice;
                    existingMedicine.MedicineStatusId = existingMedicine.MedicineQty > 0 ? 1 : 2;

                    context.Medicines.Update(existingMedicine);
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return false;
            }
        }

        public async Task<ReportPreviewDto<MedicineDto>> GetAllMedicinesAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var context = _contextFactory.CreateDbContext();
            var medicines = await context.Medicines
                .Where(x => x.CreatedDate >= fromDate && x.CreatedDate <= toDate)
                .AsNoTracking()
                .Select(medicine => new MedicineDto
                {
                    Id = medicine.Id,
                    Picture = medicine.Picture,
                    MedicineName = medicine.MedicineName,
                    MedicineGenre = medicine.MedicineGenre,
                    MedicineGenreId = medicine.MedicineGenreId,
                    MedicineType = medicine.MedicineType,
                    MedicineTypeId = medicine.MedicineTypeId,
                    CreatedDate = medicine.CreatedDate,
                    MedicineQty = medicine.MedicineQty,
                    BuyingPrice = medicine.BuyingPrice,
                    SellingPrice = medicine.SellingPrice,
                    MedicineStatusName = medicine.MedicineStatus!.MedicineStatusName,
                })
                .ToListAsync();

            var result = new ReportPreviewDto<MedicineDto>
            {
                FromDate = fromDate,
                ToDate = toDate,
                ReportType = "Medicine",
                Data = medicines
            };
            return result;
        }

        public async Task<int> AddTreatmentAsync(TreatmentDto dto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var treatment = new Treatment
                {
                    StartDate = dto.StartDate,
                    FinishDate = dto.FinishDate,
                    TreatmentQty = dto.TreatmentQty,
                    TreatmentCost = dto.TreatmentCost,
                    SurgeryId = dto.SurgeryId,
                    TherapyId = dto.TherapyId,
                    MedicalDeptId = dto.MedicalDeptId,
                    VisitId = dto.VisitId,
                    ConsultationId = dto.ConsultationId,
                    WardId = dto.WardId,
                    DoctorId = dto.DoctorId,
                    RoomId = dto.RoomId,
                    TreatmentStatusId = 1,
                    TreatmentTypeId = dto.TreatmentTypeId,
                };

                var result = context.Treatments.Add(treatment);
                await context.SaveChangesAsync();

                return result.Entity.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return 0;
            }
        }

        public async Task<bool> UpdateTreatmentAsync(TreatmentDto dto, int treatmentId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var existingTreatment = await context.Treatments.FirstOrDefaultAsync(x => x.Id == treatmentId);

                if (existingTreatment != null)
                {
                    existingTreatment.StartDate = dto.StartDate ?? existingTreatment.StartDate;
                    existingTreatment.FinishDate = dto.FinishDate ?? existingTreatment.FinishDate;

                    if (dto.TreatmentQty > 0)
                        existingTreatment.TreatmentQty = dto.TreatmentQty;

                    if (dto.TreatmentCost > 0)
                        existingTreatment.TreatmentCost = dto.TreatmentCost;

                    existingTreatment.SurgeryId = dto.SurgeryId ?? existingTreatment.SurgeryId;
                    existingTreatment.TherapyId = dto.TherapyId ?? existingTreatment.TherapyId;
                    existingTreatment.MedicalDeptId = dto.MedicalDeptId ?? existingTreatment.MedicalDeptId;
                    existingTreatment.WardId = dto.WardId ?? existingTreatment.WardId;
                    existingTreatment.DoctorId = dto.DoctorId ?? existingTreatment.DoctorId;
                    existingTreatment.RoomId = dto.RoomId ?? existingTreatment.RoomId;
                    existingTreatment.BillItemId = dto.BillItemId ?? existingTreatment.BillItemId;
                    existingTreatment.ConsultationId = dto.ConsultationId ?? existingTreatment.ConsultationId;

                    if (dto.TreatmentStatusId > 0)
                        existingTreatment.TreatmentStatusId = dto.TreatmentStatusId;

                    if (dto.TreatmentTypeId > 0)
                        existingTreatment.TreatmentTypeId = dto.TreatmentTypeId;

                    if (dto.VisitId > 0)
                        existingTreatment.VisitId = dto.VisitId;

                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return false;
            }
        }


        public async Task<List<TreatmentDto>> GetAllTreatmentsAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            var treatments = await context.Treatments
                .AsNoTracking()
                .OrderByDescending(t => t.StartDate)
                .Select(t => new TreatmentDto
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

                    // UI Display Helpers
                    TreatmentStatusName = t.TreatmentStatus != null ? t.TreatmentStatus.TreatmentStatusName : string.Empty,
                    TreatmentTypeName = t.TreatmentType != null ? t.TreatmentType.TreatmentTypeName : string.Empty,
                    SurgeryName = t.Surgery != null ? t.Surgery.SurgeryName : string.Empty,
                    TherapyName = t.Therapy != null ? t.Therapy.TherapyName : string.Empty,
                    MedicalDeptName = t.MedicalDepartment != null ? t.MedicalDepartment.MedicalDepartmentName : string.Empty,
                    WardName = t.Ward != null ? t.Ward.WardName : string.Empty,
                    DoctorName = t.Doctor != null && t.Doctor.Staff != null
                        ? $"{t.Doctor.Staff.FisrtName} {t.Doctor.Staff.MiddleName} {t.Doctor.Staff.LastName}".Replace("  ", " ").Trim()
                        : string.Empty,
                    RoomName = t.Room != null ? t.Room.RoomName : string.Empty,
                    PaymentStatusName = (t.BillItem != null && t.BillItem.Bill != null && t.BillItem.Bill.PaymentStatus != null)
                        ? t.BillItem.Bill.PaymentStatus.PaymentStatusName
                        : "Unpaid",
                    IsReadyForService =
                        (t.BillItem != null && t.BillItem.Bill != null && t.BillItem.Bill.PaymentStatusId == 3)
                        ||
                        (t.BillItem != null && t.BillItem.Bill != null &&
                         t.BillItem.Bill.PaymentModeId == 2 &&
                         t.BillItem.Bill.PatientPaidAmount >= t.BillItem.Bill.TotalPatientAmount)
                })
                .ToListAsync();

            return treatments ?? new();
        }


        public async Task<List<SurgeryDto>> GetAllSurgeriesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var surgeries = await context.Surgeries
                .AsNoTracking()
                .Select(surgery => new SurgeryDto
                {
                    Id = surgery.Id,
                    SurgeryName = surgery.SurgeryName,
                    SurgeryCost = surgery.SurgeryCost,
                    MedicalDepartmentId = surgery.MedicalDepartmentId,
                    MedicalDepartmentName = surgery.MedicalDepartment != null ? surgery.MedicalDepartment.MedicalDepartmentName : string.Empty,
                    SurgeryCodeNumber = surgery.SurgeryCodeNumber
                })
                .ToListAsync();

            return surgeries;
        }

        public async Task<List<TherapyDto>> GetAllTherapiesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var therapies = await context.Therapies
                .AsNoTracking()
                .Select(therapy => new TherapyDto
                {
                    Id = therapy.Id,
                    TherapyName = therapy.TherapyName,
                    TherapyCodeNumber = therapy.TherapyCodeNumber,
                    TherapyCost = therapy.TherapyCost,
                    MedicalDepartmentumber = therapy.MedicalDepartment != null ? therapy.MedicalDepartment.MedicalDepartmentName : string.Empty,
                    MedicalDepartmentId = therapy.MedicalDepartmentId
                })
                .ToListAsync();

            return therapies;
        }

        public async Task<List<MedicalDepartmentDto>> GetAllMedicalDeptsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var depts = await context.MedicalDepartments
                .AsNoTracking()
                .Select(dept => new MedicalDepartmentDto
                {
                    Id = dept.Id,
                    MedicalDepartmentCode = dept.MedicalDepartmentCode,
                    MedicalDepartmentName = dept.MedicalDepartmentName
                })
                .ToListAsync();

            return depts;
        }

        public async Task<int> AddPrescriptionAsync(PrescriptionDto dto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var prescription = new Prescription
                {
                    PrescribedDate = dto.PrescribedDate,
                    Quantity = dto.Quantity,
                    Dosage = dto.Dosage,
                    Notes = dto.Notes,
                    UnitPrice = dto.UnitPrice,
                    VisitId = dto.VisitId,
                    ConsultationId = dto.ConsultationId,
                    DoctorId = dto.DoctorId,
                    MedicineId = dto.MedicineId,
                    IsDispensed = false,
                    BillItemId = dto.BillItemId,
                };

                var result = context.Prescriptions.Add(prescription);
                await context.SaveChangesAsync();

                return result.Entity.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return 0;
            }
        }

        public async Task<bool> UpdatePrescriptionAsync(PrescriptionDto dto, int prescId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var existingPresc = await context.Prescriptions.FirstOrDefaultAsync(x => x.Id == prescId);

                if (existingPresc == null)
                    return false;

                if (dto.PrescribedDate != default)
                    existingPresc.PrescribedDate = dto.PrescribedDate;

                existingPresc.Quantity = dto.Quantity;
                existingPresc.Dosage = dto.Dosage;
                existingPresc.Notes = dto.Notes;
                existingPresc.IsDispensed = dto.IsDispensed;

                if (dto.UnitPrice > 0)
                    existingPresc.UnitPrice = dto.UnitPrice;

                if (dto.VisitId > 0)
                    existingPresc.VisitId = dto.VisitId;

                existingPresc.ConsultationId = dto.ConsultationId ?? existingPresc.ConsultationId;
                existingPresc.MedicineId = dto.MedicineId > 0 ? dto.MedicineId : existingPresc.MedicineId;
                existingPresc.DoctorId = dto.DoctorId ?? existingPresc.DoctorId;
                existingPresc.BillItemId = dto.BillItemId ?? existingPresc.BillItemId;

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating prescription: {ex}");
                return false;
            }
        }


        public async Task<PrescriptionDto> GetPrescriptionByIdAsync(int pId)
        {
            using var context = _contextFactory.CreateDbContext();
            var presc = await context.Prescriptions.Where(x => x.Id == pId)
                        .Select(med => new PrescriptionDto
                        {
                            Id = med.Id,
                            Quantity = med.Quantity,
                            Dosage = med.Dosage,
                            Notes = med.Notes,
                            UnitPrice = med.UnitPrice,
                            IsDispensed = med.IsDispensed,
                            VisitId = med.VisitId,
                            ConsultationId = med.ConsultationId,
                            MedicineId = med.MedicineId,
                            DoctorId = med.DoctorId,
                            BillItemId = med.BillItemId,
                            MedicineName = med.Medicine != null ? med.Medicine.MedicineName : string.Empty,
                            DoctorFullName = med.Doctor != null ? $"{med.Doctor.Staff!.FisrtName} {med.Doctor.Staff!.MiddleName} {med.Doctor.Staff!.LastName}" : string.Empty,
                            PrescribedDate = med.PrescribedDate
                        }).FirstOrDefaultAsync();

            return presc ?? new();
        }

        public async Task<TreatmentDto> GetTreatmentByIdAsync(int pId)
        {
            using var context = _contextFactory.CreateDbContext();
            var presc = await context.Treatments.Where(x => x.Id == pId)
                        .Select(med => new TreatmentDto
                        {
                            Id = med.Id,
                            StartDate = med.StartDate,
                            FinishDate = med.FinishDate,
                            TreatmentQty = med.TreatmentQty,
                            TreatmentCost = med.TreatmentCost,
                            VisitId = med.VisitId,
                            ConsultationId = med.ConsultationId,
                            BillItemId = med.BillItemId,
                            DoctorId = med.DoctorId,
                            SurgeryId = med.SurgeryId,
                            TherapyId = med.TherapyId,
                            MedicalDeptId = med.MedicalDeptId,
                            WardId = med.WardId,
                            RoomId = med.RoomId,
                            TreatmentStatusId = med.TreatmentStatusId,
                            TreatmentTypeId = med.TreatmentTypeId,


                            TreatmentStatusName = med.TreatmentStatus != null ? med.TreatmentStatus.TreatmentStatusName : string.Empty,
                            TreatmentTypeName = med.TreatmentType != null ? med.TreatmentType.TreatmentTypeName : string.Empty,
                            SurgeryName = med.Surgery != null ? med.Surgery.SurgeryName : string.Empty,
                            TherapyName = med.Therapy != null ? med.Therapy.TherapyName : string.Empty,
                            MedicalDeptName = med.MedicalDepartment != null ? med.MedicalDepartment.MedicalDepartmentName : string.Empty,
                            WardName = med.Ward != null ? med.Ward.WardName : string.Empty,
                            PaymentStatusName = med.TreatmentStatus != null ? med.TreatmentStatus.TreatmentStatusName : string.Empty,
                            RoomName = med.Room != null ? med.Room.RoomName : string.Empty,
                            DoctorName = med.Doctor != null ? $"{med.Doctor.Staff!.FisrtName} {med.Doctor.Staff!.MiddleName} {med.Doctor.Staff!.LastName}" : string.Empty
                        }).FirstOrDefaultAsync();

            return presc ?? new();
        }

        public async Task<List<PrescriptionDto>> GetAllPrescriptionsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var presc = await context.Prescriptions
                .AsNoTracking()
                .OrderByDescending(med => med.PrescribedDate)
                .Select(med => new PrescriptionDto
                {
                    Id = med.Id,
                    Quantity = med.Quantity,
                    Dosage = med.Dosage,
                    IsDispensed = med.IsDispensed,
                    Notes = med.Notes,
                    UnitPrice = med.UnitPrice,
                    VisitId = med.VisitId,
                    ConsultationId = med.ConsultationId,
                    MedicineId = med.MedicineId,
                    DoctorId = med.DoctorId,
                    BillItemId = med.BillItemId,
                    MedicineName = med.Medicine != null ? med.Medicine.MedicineName : string.Empty,
                    DoctorFullName = med.Doctor != null && med.Doctor.Staff != null
                        ? $"{med.Doctor.Staff.FisrtName} {med.Doctor.Staff.MiddleName} {med.Doctor.Staff.LastName}".Replace("  ", " ").Trim()
                        : string.Empty,
                    PrescribedDate = med.PrescribedDate,
                    PaymentStatusName = (med.BillItem != null && med.BillItem.Bill != null && med.BillItem.Bill.PaymentStatus != null)
                        ? med.BillItem.Bill.PaymentStatus.PaymentStatusName
                        : "Unpaid",
                    IsReadyForService =
                        (med.BillItem != null && med.BillItem.Bill != null && med.BillItem.Bill.PaymentStatusId == 3)
                        ||
                        (med.BillItem != null && med.BillItem.Bill != null &&
                         med.BillItem.Bill.PaymentModeId == 2 &&
                         med.BillItem.Bill.PatientPaidAmount >= med.BillItem.Bill.TotalPatientAmount)
                }).ToListAsync();

            return presc ?? new();
        }



        public async  Task<ReportPreviewDto<PrescriptionDto>> GetAllPrescriptionsAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var context = _contextFactory.CreateDbContext();
            var presc = await context.Prescriptions
                        .Where(x => x.PrescribedDate >= fromDate && x.PrescribedDate <= toDate)
                        .Select(med => new PrescriptionDto
                        {
                            Id = med.Id,
                            Quantity = med.Quantity,
                            Dosage = med.Dosage,
                            Notes = med.Notes,
                            UnitPrice = med.UnitPrice,
                            VisitId = med.VisitId,
                            ConsultationId = med.ConsultationId,
                            MedicineId = med.MedicineId,
                            DoctorId = med.DoctorId,
                            BillItemId = med.BillItemId,
                            MedicineName = med.Medicine != null ? med.Medicine.MedicineName : string.Empty,
                            DoctorFullName = med.Doctor != null ? $"{med.Doctor.Staff!.FisrtName} {med.Doctor.Staff!.MiddleName} {med.Doctor.Staff!.LastName}" : string.Empty,
                            PrescribedDate = med.PrescribedDate
                        }).ToListAsync();

            var result = new ReportPreviewDto<PrescriptionDto>
            {
                FromDate = fromDate,
                ToDate = toDate,
                ReportType = "Medicine",
                Data = presc
            };
            return result;
        }

        public async Task<List<TreatmentTypeDto>> GetAllTreatmentTypeAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var tTypes = await context.TreatmentTypes
                .AsNoTracking()
                .Select(treatmentType => new TreatmentTypeDto
                {
                    Id = treatmentType.Id,
                    TreatmentTypeName = treatmentType.TreatmentTypeName,
                    HasSurgeryId = treatmentType.HasSurgeryId,
                    HasTherapyId = treatmentType.HasTherapyId,
                    HasMedicalDeptId = treatmentType.HasMedicalDeptId,
                    HasWardId = treatmentType.HasWardId,
                    HasDoctorId = treatmentType.HasDoctorId,
                    HasRoomId = treatmentType.HasRoomId,
                    TreatmentTypeCode = treatmentType.TreatmentTypeCode
                })
                .ToListAsync();

            return tTypes;
        }

        public async Task<MedicineDto> GetMedicineByIdAsync(int Id)
        {
            using var context = _contextFactory.CreateDbContext();
            var medicine = await context.Medicines.Where(x => x.Id == Id)
                        .Select(medicine => new MedicineDto
                        {
                            Id = medicine.Id,
                            Picture = medicine.Picture,
                            MedicineName = medicine.MedicineName,
                            MedicineGenre = medicine.MedicineGenre,
                            MedicineGenreId = medicine.MedicineGenreId,
                            MedicineType = medicine.MedicineType,
                            MedicineTypeId = medicine.MedicineTypeId,
                            CreatedDate = medicine.CreatedDate,
                            MedicineQty = medicine.MedicineQty,
                            BuyingPrice = medicine.BuyingPrice,
                            SellingPrice = medicine.SellingPrice,
                            MedicineStatusName = medicine.MedicineStatus!.MedicineStatusName
                        }).FirstOrDefaultAsync();

            return medicine ?? new();
        }
    }
}
