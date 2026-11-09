using Microsoft.EntityFrameworkCore;
using NovaCare.Components.Pages.Medicine___Pharmacy_Pages;
using NovaCare.Data;
using NovaCare.Interfaces;
using NovaCare.Modals.MedicineModals;
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
                    MedicineTypeName = medicine.MedicineType != null ? medicine.MedicineType.MedicineTypeName : string.Empty,
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

        public async Task<MedicineDto> GetMedicineIdByNameAsync(string name)
        {
            using var context = _contextFactory.CreateDbContext();
            var medicine = await context.Medicines.Where(x => x.MedicineName.ToLower().Trim() == name.ToLower().Trim())
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
                    existingMedicine.MedicineType = dto.MedicineType;
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
                    MedicineTypeName = medicine.MedicineType != null ? medicine.MedicineType.MedicineTypeName : string.Empty,
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
                    MedicineId = dto.MedicineId,
                    SurgeryId = dto.SurgeryId,
                    TherapyId = dto.TherapyId,
                    MedicalDeptId = dto.MedicalDeptId,
                    WardId = dto.WardId,
                    DoctorId = dto.DoctorId,
                    RoomId = dto.RoomId,
                    TreatmentStatusId = dto.TreatmentStatusId,
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
                    existingTreatment.StartDate = dto.StartDate;
                    existingTreatment.FinishDate = dto.FinishDate;
                    existingTreatment.TreatmentQty = dto.TreatmentQty;
                    existingTreatment.TreatmentCost = dto.TreatmentCost;
                    existingTreatment.MedicineId = dto.MedicineId;
                    existingTreatment.SurgeryId = dto.SurgeryId;
                    existingTreatment.TherapyId = dto.TherapyId;
                    existingTreatment.MedicalDeptId = dto.MedicalDeptId;
                    existingTreatment.WardId = dto.WardId;
                    existingTreatment.DoctorId = dto.DoctorId;
                    existingTreatment.RoomId = dto.RoomId;
                    existingTreatment.TreatmentStatusId = dto.TreatmentStatusId;
                    existingTreatment.TreatmentTypeId = dto.TreatmentTypeId;

                    context.Treatments.Update(existingTreatment);
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

        public async Task<List<TreatmentTypeDto>> GetAllTreatmentTypeAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var treatments = await context.TreatmentTypes
                .AsNoTracking()
                .Select(treatment => new TreatmentTypeDto
                {
                    Id = treatment.Id,
                    TreatmentTypeName = treatment.TreatmentTypeName,
                    TreatmentTypeCode = treatment.TreatmentTypeCode,

                    HasMedicineId = treatment.HasMedicineId,
                    HasSurgeryId = treatment.HasSurgeryId,
                    HasTherapyId = treatment.HasTherapyId,
                    HasMedicalDeptId = treatment.HasMedicalDeptId,
                    HasWardId = treatment.HasWardId,
                    HasDoctorId = treatment.HasDoctorId,
                    HasRoomId = treatment.HasRoomId
                })
                .ToListAsync();

            return treatments;
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
    }
}
