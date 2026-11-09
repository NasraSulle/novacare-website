using NovaCare.Modals.PatientModals;
using System.ComponentModel.DataAnnotations;

namespace NovaCare.Modals.DoctorModals
{
    public class MedicalDepartment
    {
        [Key]
        public int Id { get; set; }
        public string MedicalDepartmentCode { get; set; } = string.Empty;
        public string MedicalDepartmentName { get; set; } = string.Empty;

        //navigation properties
        public List<DoctorType>? DoctorTypes { get; set; }
        public List<Consultation>? Consultations { get; set; }
    }
}
