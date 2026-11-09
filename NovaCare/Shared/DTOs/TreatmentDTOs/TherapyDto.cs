using NovaCare.Modals.DoctorModals;

namespace NovaCare.Shared.DTOs.TreatmentDTOs
{
    public class TherapyDto
    {
        public int Id { get; set; }
        public string TherapyName { get; set; } = string.Empty;
        public string TherapyCodeNumber { get; set; } = string.Empty;
        public decimal TherapyCost { get; set; } = 0m;

        public int MedicalDepartmentId { get; set; }
        public MedicalDepartment? MedicalDepartment { get; set; }
        public string MedicalDepartmentumber { get; set; } = string.Empty;
    }
}
