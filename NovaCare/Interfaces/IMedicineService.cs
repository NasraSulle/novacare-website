using NovaCare.Shared.DTOs.DoctorDTOs;
using NovaCare.Shared.DTOs.MedicineDTOs;
using NovaCare.Shared.DTOs.ReportDTOs;
using NovaCare.Shared.DTOs.RoomDTOs;
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
        Task<MedicineDto> GetMedicineByIdAsync(int Id);
        Task<bool> UpdateMedicineAsync(MedicineDto dto, int medicineId);


        Task<int> AddTreatmentAsync(TreatmentDto dto);
        Task<bool> UpdateTreatmentAsync(TreatmentDto dto, int treatmentId);
        Task<TreatmentDto> GetTreatmentByIdAsync(int pId);
        Task<List<TreatmentDto>> GetAllTreatmentsAsync();
        Task<List<TreatmentTypeDto>> GetAllTreatmentTypeAsync();


        Task<List<SurgeryDto>> GetAllSurgeriesAsync();
        Task<int> AddSurgeryAsync(SurgeryDto surgDto);
        Task<bool> UpdateSurgeryAsync(SurgeryDto surgDto, int surgId);
        Task<SurgeryDto> GetSurgeryByNameAsync(string surgName);
        Task<SurgeryDto> GetSurgeryByIdAsync(int surgId);

        Task<List<TherapyDto>> GetAllTherapiesAsync();
        Task<int> AddTherapyAsync(TherapyDto therapyDto);
        Task<bool> UpdateTherapyAsync(TherapyDto therapyDto, int therapyId);
        Task<TherapyDto> GetTherapyByNameAsync(string therapyName);
        Task<TherapyDto> GetTherapyByIdAsync(int therapyId);

        Task<List<MedicalDepartmentDto>> GetAllMedicalDeptsAsync();


        Task<int> AddPrescriptionAsync(PrescriptionDto dto);
        Task<bool> UpdatePrescriptionAsync(PrescriptionDto dto, int prescId);
        Task<PrescriptionDto> GetPrescriptionByIdAsync(int pId);
        Task<List<PrescriptionDto>> GetAllPrescriptionsAsync();
        Task<ReportPreviewDto<PrescriptionDto>> GetAllPrescriptionsAsync(DateTime? fromDate, DateTime? toDate);


        Task<List<BloodDto>> GetAllBloodsAsync();
        Task<BloodDto> GetBloodByIdAsync(int bId);
        Task<bool> UpdateBloodAsync(BloodDto dto, int bloodId);

    }
}
