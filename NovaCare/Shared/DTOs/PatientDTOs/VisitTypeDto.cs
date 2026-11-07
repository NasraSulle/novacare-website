
namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class VisitTypeDto
    {
        public int Id { get; set; }
        public string TypeNme { get; set; } = string.Empty;
        public List<VisitDto>? Visits { get; set; }
    }
}
