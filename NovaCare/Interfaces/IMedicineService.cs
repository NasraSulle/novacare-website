using NovaCare.Shared.DTOs.DoctorDTOs;
using NovaCare.Shared.DTOs.MedicineDTOs;
using NovaCare.Shared.DTOs.ReportDTOs;
using NovaCare.Shared.DTOs.StaffDTOs;
using NovaCare.Shared.DTOs.TreatmentDTOs;

namespace NovaCare.Interfaces
{
    public interface IMedicineService
    {
        Task<ReportPreviewDto<MedicineDto>> GetAllMedicinesAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<MedicineDto>> GetAllMedicinesAsync();
        Task<List<MedicineTypeDto>> GetAllMedicineTypesAsync();
        Task<List<MedicineGenreDto>> GetAllMedicineGenresAsync();
        Task<bool> AddMedicineAsync(MedicineDto dto);
        Task<MedicineDto> GetMedicineIdByNameAsync(string name, string type, string genre);
        Task<bool> UpdateMedicineAsync(MedicineDto dto, int medicineId);

        Task<int> AddTreatmentAsync(TreatmentDto dto);
        Task<bool> UpdateTreatmentAsync(TreatmentDto dto, int treatmentId);
        Task<TreatmentDto> GetTreatmentByIdAsync(int pId);
        Task<List<TreatmentTypeDto>> GetAllTreatmentTypeAsync();
        Task<List<SurgeryDto>> GetAllSurgeriesAsync();
        Task<List<TherapyDto>> GetAllTherapiesAsync();
        Task<List<MedicalDepartmentDto>> GetAllMedicalDeptsAsync();

        Task<int> AddPrescriptionAsync(PrescriptionDto dto);
        Task<bool> UpdatePrescriptionAsync(PrescriptionDto dto, int prescId);
        Task<PrescriptionDto> GetPrescriptionByIdAsync(int pId);
    }
}
