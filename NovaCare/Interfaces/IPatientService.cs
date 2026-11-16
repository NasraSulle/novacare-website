using NovaCare.Shared.DTOs.InsuranceDTOs;
using NovaCare.Shared.DTOs.JoinDto;
using NovaCare.Shared.DTOs.PatientDTOs;
using NovaCare.Shared.DTOs.ReportDTOs;

namespace NovaCare.Interfaces
{
    public interface IPatientService
    {
        Task<ReportPreviewDto<PatientDto>> GetAllPatientsAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<PatientDto>> GetAllPatientsAsync();
        Task<List<PatientStatusDto>> GetAllPatientTypesAsync();
        Task<List<PatientTypeDto>> GetAllPatientStatusesAsync();
        Task<string> AddPatientAsync(PatientDto dto);
        Task<PatientDto> GetPatientByIdAsync(int patientId);
        Task<PatientDto> GetPatientByCodeAsync(string patientCode);
        Task<bool> UpdatePatientCodeAsync(int patientId, int code);
        Task<bool> UpdatePatientAsync(PatientDto dto, int patientId);

        // Patient Insurance Policies
        Task<int> AddPatientInsuranceAsync(PatientInsuaranceDto dto);
        Task<List<PatientInsuaranceDto>> GetPatientInsurancesAsync(int patientId);

        // Visits & Visit Insurance Authorization
        Task<List<VisitDto>> GetAllVisitsAsync();
        Task<int> AddVisitAsync(VisitDto dto);
        Task<bool> UpdateVisitAsync(VisitDto dto, int visitId);
        Task<VisitInsuranceAuthDto> SaveVisitInsuranceAuthAsync(VisitInsuranceAuthDto dto);
        Task<VisitInsuranceAuthDto?> GetVisitInsuranceAuthAsync(int visitId);

        Task<List<VisitTypeDto>> GetAllVisitTypesAsync();
        Task<int> AddVisitTypeAsync(VisitTypeDto dto);
        Task<bool> UpdateVisitTypeAsync(VisitTypeDto dto, int visitTypeId);

        Task<PreliminaryTestDto> AddPreliminaryTestAsync(PreliminaryTestDto dto);
        Task<int> AddConsultationAsync(ConsultationDto dto);
        Task<bool> UpdateConsultationAsync(ConsultationDto dto, int conslId);

        Task<VisitDto> GetVisitByPCodeOrFullNameAsync(string pCodeOrFullName);

        Task<List<ConsultationDto>> GetAllConsultationsAsync();
        Task<ConsultationDto> GetConsultationByIdAsync(int conId);
    }
}
