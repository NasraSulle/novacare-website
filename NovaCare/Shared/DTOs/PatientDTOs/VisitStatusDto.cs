using NovaCare.Modals.PatientModals;

namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class VisitStatusDto
    {
        public int Id { get; set; }
        public string VisitStatusName { get; set; } = string.Empty;

        public List<VisitDto>? Visits { get; set; }
    }
}
