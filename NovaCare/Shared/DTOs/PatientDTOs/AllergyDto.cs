using NovaCare.Modals.PatientModals;

namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class AllergyDto
    {
        public int Id { get; set; }
        public string AllergyName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int PatientId { get; set; }
        public PatientDto? Patient { get; set; }
    }
}
