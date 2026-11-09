
using NovaCare.Shared.DTOs.PatientDTOs;

namespace NovaCare.Shared.DTOs.DoctorDTOs
{
    public class MedicalDepartmentDto
    {
        public int Id { get; set; }
        public string MedicalDepartmentCode { get; set; } = string.Empty;
        public string MedicalDepartmentName { get; set; } = string.Empty;

        //navigation properties
        public List<DoctorTypeDto>? DoctorTypes { get; set; }
        public List<ConsultationDto>? Consultations { get; set; }
    }
}
