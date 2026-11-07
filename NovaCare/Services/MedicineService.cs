using Microsoft.EntityFrameworkCore;
using NovaCare.Data;
using NovaCare.Interfaces;
using NovaCare.Modals.MedicineModals;
using NovaCare.Shared.DTOs.MedicineDTOs;
using NovaCare.Shared.DTOs.PatientDTOs;
using NovaCare.Shared.DTOs.ReportDTOs;

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
                    MedicineType = medicine.MedicineType,
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
                    MedicineType = dto.MedicineType,
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
                    MedicineType = medicine.MedicineType,
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
    }
}
