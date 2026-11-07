using NovaCare.Shared.DTOs.LabTestDTOs;
using NovaCare.Shared.DTOs.ReportDTOs;

namespace NovaCare.Interfaces
{
    public interface ILabTestService
    {
        Task<ReportPreviewDto<LabTestDto>> GetAllLabTestsAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<LabTestDto>> GetAllLabTestsAsync();
        Task<LabTestDto> GetLabTestByIdAsync(int testId);
        Task<bool> AddLabTestAsync(LabTestDto dto);
        Task<bool> UpdateLabTestAsync(LabTestDto dto, int medicineId);

        Task<List<LabTestResultCompositionDto>> GetAllLabTestResultCompositionsAsync();
        Task<bool> AddLabTestResultCompositionAsync(LabTestResultCompositionDto dto);
        Task<bool> UpdateLabTestResultCompositionAsync(LabTestResultCompositionDto dto, int compositionId);
    }
}
