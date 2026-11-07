using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NovaCare.Components.Pages.Configurations;
using NovaCare.Data;
using NovaCare.Interfaces;
using NovaCare.Modals.DiseaseModals;
using NovaCare.Modals.StaffModals;
using NovaCare.Shared.DTOs.DiseaseDTOs;
using NovaCare.Shared.DTOs.RoomDTOs;
using NovaCare.Shared.DTOs.StaffDTOs;
using static MudBlazor.Icons.Material;

namespace NovaCare.Services
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IDbContextFactory<NovaCareDbContext> _contextFactory;
        public DiseaseService(IDbContextFactory<NovaCareDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<bool> AddDiseaseAsync(DiseaseDto roomDto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                if (roomDto == null) return false;

                var newDisease = new Disease
                {
                    DiseaseName = roomDto.DiseaseName,
                    DiseaseTypeId = roomDto.DiseaseTypeId,
                    LabTestCost = roomDto.LabTestCost,
                    DiseaseCode = roomDto.DiseaseCode
                };

                var result = context.Diseases.Add(newDisease);
                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<DiseaseDto>> GetAllDiseasesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var diseases = await context.Diseases
                .AsNoTracking()
                .Select(type => new DiseaseDto
                {
                    Id = type.Id,
                    DiseaseCode = type.DiseaseCode,
                    LabTestCost = type.LabTestCost,
                    DiseaseName = type.DiseaseName,
                    DiseaseTypeId = type.DiseaseTypeId,
                    DiseaseTypeName = type.DiseaseType != null ? type.DiseaseType.DiseaseTypeName : string.Empty
                })
                .ToListAsync();

            return diseases ?? new();
        }

        public async Task<List<DiseaseTypeDto>> GetAllDiseaseTypesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var diseases = await context.DiseaseTypes
                .AsNoTracking()
                .Select(type => new DiseaseTypeDto
                {
                    Id = type.Id,
                    DiseaseTypeCode = type.DiseaseTypeCode,
                    DiseaseTypeName = type.DiseaseTypeName,
                    DiseasesCount = type.Diseases != null ? type.Diseases.Count : 0
                })
                .ToListAsync();

            return diseases ?? new();
        }

        public async Task<DiseaseDto> GetDiseaseByIdAsync(int diseaseId)
        {
            using var context = _contextFactory.CreateDbContext();
            var existingType = await context.Diseases
                .AsNoTracking()
                .Select(type => new DiseaseDto
                {
                    Id = type.Id,
                    DiseaseCode = type.DiseaseCode,
                    DiseaseName = type.DiseaseName,
                    DiseaseTypeId = type.DiseaseTypeId,
                    LabTestCost = type.LabTestCost,
                    DiseaseTypeName = type.DiseaseType != null ? type.DiseaseType.DiseaseTypeName : string.Empty
                })
                .FirstOrDefaultAsync(x => x.Id == diseaseId);

            return existingType ?? new();
        }

        public async Task<DiseaseDto> GetDiseaseByNameAsync(string diseaseName)
        {
            using var context = _contextFactory.CreateDbContext();
            var existingType = await context.Diseases
                .AsNoTracking()
                .Select(type => new DiseaseDto
                {
                    Id = type.Id,
                    DiseaseCode = type.DiseaseCode,
                    DiseaseName = type.DiseaseName,
                    LabTestCost = type.LabTestCost,
                    DiseaseTypeId = type.DiseaseTypeId,
                    DiseaseTypeName = type.DiseaseType != null ? type.DiseaseType.DiseaseTypeName : string.Empty
                })
                .FirstOrDefaultAsync(x => x.DiseaseName == diseaseName);

            return existingType ?? new();
        }

        public async Task<bool> UpdateDiseaseAsync(DiseaseDto roomDto, int romId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingDisease = await context.Diseases.FirstOrDefaultAsync(x => x.Id == romId);

                if (existingDisease != null)
                {
                    existingDisease.DiseaseCode = roomDto.DiseaseCode;
                    existingDisease.DiseaseName = roomDto.DiseaseName;
                    existingDisease.DiseaseTypeId = roomDto.DiseaseTypeId;
                    existingDisease.LabTestCost = roomDto.LabTestCost;

                    context.Diseases.Update(existingDisease);
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
