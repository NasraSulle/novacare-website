using NovaCare.Modals.DoctorModals;

namespace NovaCare.Modals.TreatmentModals
{
    public class Surgery
    {
        public int Id { get; set; }
        public string SurgeryName { get; set; } = string.Empty;
        public string SurgeryCodeNumber { get; set; } = string.Empty;
        public decimal SurgeryCost { get; set; } = 0m;

        public int MedicalDepartmentId { get; set; }
        public MedicalDepartment? MedicalDepartment { get; set; }
    }
}
