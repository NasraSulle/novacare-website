using NovaCare.Shared.DTOs.MedicineDTOs;
using NovaCare.Shared.DTOs.ReportDTOs;
using NovaCare.Shared.DTOs.StaffDTOs;

namespace NovaCare.Interfaces
{
    public interface IMedicineService
    {
        Task<ReportPreviewDto<MedicineDto>> GetAllMedicinesAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<MedicineDto>> GetAllMedicinesAsync();
        Task<List<MedicineTypeDto>> GetAllMedicineTypesAsync();
        Task<List<MedicineGenreDto>> GetAllMedicineGenresAsync();
        Task<bool> AddMedicineAsync(MedicineDto dto);
        Task<MedicineDto> GetMedicineIdByNameAsync(string name);
        Task<bool> UpdateMedicineAsync(MedicineDto dto, int medicineId);
    }
}
