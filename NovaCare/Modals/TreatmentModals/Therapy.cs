using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.RoomModals;

namespace NovaCare.Modals.TreatmentModals
{
    public class Therapy
    {
        public int Id { get; set; }
        public string TherapyName { get; set; } = string.Empty;
        public string TherapyCodeNumber { get; set; } = string.Empty;
        public decimal TherapyCost { get; set; } = 0m;

        public int MedicalDepartmentId { get; set; }
        public MedicalDepartment? MedicalDepartment { get; set; }
    }
}
