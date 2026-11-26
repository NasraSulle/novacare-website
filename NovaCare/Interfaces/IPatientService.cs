using NovaCare.Modals.PatientModals;
using NovaCare.Shared.DTOs.DoctorDTOs;
using NovaCare.Shared.DTOs.InsuranceDTOs;
using NovaCare.Shared.DTOs.JoinDto;
using NovaCare.Shared.DTOs.LabTestDTOs;
using NovaCare.Shared.DTOs.PatientDTOs;
using NovaCare.Shared.DTOs.ReportDTOs;
using NovaCare.Shared.DTOs.TreatmentDTOs;

namespace NovaCare.Interfaces
{
    public interface IPatientService
    {
        Task<ReportPreviewDto<PatientDto>> GetAllPatientsAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<PatientDto>> GetAllPatientsAsync();
        Task<List<PatientTypeDto>> GetAllPatientTypesAsync();
        Task<List<PatientStatusDto>> GetAllPatientStatusesAsync();
        Task<string> AddPatientAsync(PatientDto dto);
        Task<PatientDto> GetPatientByIdAsync(int patientId);
        Task<PatientDto> GetPatientByCodeAsync(string patientCode);
        Task<bool> UpdatePatientCodeAsync(int patientId, int code);
        Task<bool> UpdatePatientAsync(PatientDto dto, int patientId);

        // Patient Insurance Policies
        Task<int> AddPatientInsuranceAsync(PatientInsuaranceDto dto);
        Task<List<PatientInsuaranceDto>> GetPatientInsurancesAsync(int patientId);

        // Visits & Visit Insurance Authorization
        Task<ReportPreviewDto<VisitDto>> GetAllVisitsAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<VisitDto>> GetAllVisitsAsync();
        Task<int> AddVisitAsync(VisitDto dto);
        Task<bool> UpdateVisitAsync(VisitDto dto, int visitId);
        Task<VisitInsuranceAuthDto> SaveVisitInsuranceAuthAsync(VisitInsuranceAuthDto dto);
        Task<VisitInsuranceAuthDto?> GetVisitInsuranceAuthAsync(int visitId);

        Task<List<VisitTypeDto>> GetAllVisitTypesAsync();
        Task<int> AddVisitTypeAsync(VisitTypeDto dto);
        Task<bool> UpdateVisitTypeAsync(VisitTypeDto dto, int visitTypeId);

        Task<PreliminaryTestDto> AddPreliminaryTestAsync(PreliminaryTestDto dto);
        Task<bool> UpdatePreliminaryTestAsync(PreliminaryTestDto dto, int Id);

        Task<int> AddConsultationAsync(ConsultationDto dto);
        Task<bool> UpdateConsultationAsync(ConsultationDto dto, int conslId);

        Task<VisitDto> GetVisitByPCodeOrFullNameAsync(string pCodeOrFullName);
        Task<int> GetVisitIdByPatientIdAsync(int pId);
        Task<VisitDto> GetLastVisit(int pId);

        Task<List<ConsultationDto>> GetAllConsultationsAsync();
        Task<ConsultationDto> GetConsultationByIdAsync(int conId);

        Task<List<PrescriptionDto>> GetPreviousPrescriptionsAsync(int patientId);
        Task<List<LabTestDto>> GetPreviousLabTestsAsync(int patientId);
        Task<List<TreatmentDto>> GetPreviousTreatmentsAsync(int patientId);
        Task<List<DoctorDto>> GetOtherDoctorAsync(int patientId);

        Task<List<AllergyDto>> GetAllergiesAsync(int patientId);
        Task<AllergyDto> GetAllergyByNameAsync(string allergyName);
        Task<bool> AddAllergyAsync(AllergyDto dto);
        Task<ReferralDto> AddReferralLetterAsync(ReferralDto dto);
        Task<ReferralDto> GetReferralByConsultationIdAsync(int consId);
    }
}
