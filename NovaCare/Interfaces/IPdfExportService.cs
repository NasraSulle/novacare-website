using NovaCare.Shared.DTOs.ReportDTOs;
using NovaCare.Shared.DTOs.StaffDTOs;

namespace NovaCare.Interfaces
{
    public interface IPdfExportService
    {
        public string GenerateStaffPreview<T>(ReportPreviewDto<T> dto, StaffDto? currentUser);
    }
}
