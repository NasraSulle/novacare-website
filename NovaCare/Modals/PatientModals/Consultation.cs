using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.LabTestModals;
using NovaCare.Modals.RoomModals;
using NovaCare.Modals.TreatmentModals;
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
        public DateTime? FinishDate { get; set; } // Nullable until the doctor finishes the consultation
        public decimal ConsultationCost { get; set; } = 0m;

        // Foreign keys
        [ForeignKey(nameof(Visit))]
        public int VisitId { get; set; }

        [ForeignKey(nameof(Room))]
        public int? RoomId { get; set; }

        [ForeignKey(nameof(Doctor))]
        public int? DoctorId { get; set; }

        [ForeignKey(nameof(MedicalDepartment))]
        public int? MedicalDepartmentId { get; set; }

        [ForeignKey(nameof(BillItem))]
        public int? BillItemId { get; set; } // Null = unbilled; HasValue = linked to a BillItem

        // Navigation properties
        public MedicalDepartment? MedicalDepartment { get; set; }
        public Room? Room { get; set; }
        public Doctor? Doctor { get; set; }
        public Visit? Visit { get; set; }
        public BillItem? BillItem { get; set; }

        // Orders placed during this specific consultation
        public List<LabTest> LabTests { get; set; } = new();
        public List<Prescription> Prescriptions { get; set; } = new();
        public List<Treatment> Treatments { get; set; } = new();
    }
}
