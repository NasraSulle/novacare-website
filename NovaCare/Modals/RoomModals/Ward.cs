using NovaCare.Modals.DoctorModals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.RoomModals
{
    public class Ward
    {

        [Key]
        public int Id { get; set; }

        public string WardName { get; set; } = string.Empty;

        public decimal WardCost { get; set; } = 0m;


        [ForeignKey(nameof(MedicalDepartment))]
        public int? MedicalDepartmentId { get; set; }
        public MedicalDepartment? MedicalDepartment { get; set; }

        public List<Bed>? Beds { get; set; }
    }
}
