using NovaCare.Shared.DTOs.DoctorDTOs;

namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class ConsultationDto
    {
        public int Id { get; set; }
        public string Diagnosis { get; set; } = string.Empty;

        // Foreign
        public int VisitId { get; set; }
        public int? DoctorId { get; set; }
        public int? ServiceId { get; set; }

        // Navigation properties
        public DoctorDto? Doctor { get; set; }
        public VisitDto? Visit { get; set; }
        public ServiceDto? Service { get; set; }

        public string ServiceName { get; set; } = string.Empty;
    }
}
