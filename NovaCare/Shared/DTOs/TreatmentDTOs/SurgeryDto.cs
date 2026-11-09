using NovaCare.Shared.DTOs.DoctorDTOs;

namespace NovaCare.Shared.DTOs.TreatmentDTOs
{
    public class SurgeryDto
    {
        public int Id { get; set; }
        public string SurgeryName { get; set; } = string.Empty;
        public string SurgeryCodeNumber { get; set; } = string.Empty;
        public decimal SurgeryCost { get; set; } = 0m;


        public int MedicalDepartmentId { get; set; }
        public MedicalDepartmentDto? MedicalDepartment { get; set; }
        public string MedicalDepartmentName { get; set; } = string.Empty;
    }
}
