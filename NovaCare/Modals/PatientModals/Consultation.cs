using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.RoomModals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.PatientModals
{
    public class Consultation
    {
        [Key]
        public int Id { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public decimal ConsultationCost { get; set; } = 0m;

        // Foreign keys
        [ForeignKey(nameof(Visit))]
        public int VisitId { get; set; }

        [ForeignKey(nameof(Room))]
        public int RoomId { get; set; }

        [ForeignKey(nameof(Doctor))]
        public int? DoctorId { get; set; }

        [ForeignKey(nameof(MedicalDepartment))]
        public int? MedicalDepartmentId { get; set; }

        // Navigation properties
        public MedicalDepartment? MedicalDepartment { get; set; }

        public Room? Room { get; set; }
        public Doctor? Doctor { get; set; }
        public Visit? Visit { get; set; }

        [ForeignKey(nameof(Bill))]
        public int? BillId { get; set; }
        public Bill? Bill { get; set; }
    }
}
