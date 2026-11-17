
namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class ReferralDto
    {
        public int Id { get; set; }
        public string DoctorFullName { get; set; } = string.Empty;
        public string PatientFullName { get; set; } = string.Empty;
        public string ReferredHospital { get; set; } = string.Empty;
        public string DoctorsNote { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; }

        public int ConsultationId { get; set; }

        public ConsultationDto Consultation { get; set; } = new();
    }
}
