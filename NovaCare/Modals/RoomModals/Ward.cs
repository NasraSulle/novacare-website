using NovaCare.Modals.DoctorModals;

namespace NovaCare.Modals.RoomModals
{
    public class Ward
    {
        public int Id { get; set; }

        public string WardName { get; set; } = string.Empty;

        public decimal WardCost { get; set; } = 0m;

        public int MedicalDepartmentId { get; set; }
        public MedicalDepartment? MedicalDepartment { get; set; }

        public List<Bed>? Beds { get; set; }
    }
}
