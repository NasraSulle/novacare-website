using NovaCare.Modals.DiseaseModals;
using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.JoinTables;
using NovaCare.Modals.PatientModals;
using NovaCare.Modals.RoomModals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.TreatmentModals
{
    public class Treatment
    {
        [Key]
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; } // Nullable until treatment finishes
        public int TreatmentQty { get; set; } = 1;
        public decimal TreatmentCost { get; set; } = 0m;

        // Required to link Treatment to a Patient Visit, Consultation, and BillItem
        [ForeignKey(nameof(Visit))]
        public int VisitId { get; set; }

        [ForeignKey(nameof(Consultation))]
        public int? ConsultationId { get; set; } // Null = walk-in treatment; HasValue = ordered during consultation

        [ForeignKey(nameof(BillItem))]
        public int? BillItemId { get; set; } // Null = unbilled; HasValue = linked to a BillItem

        // Clinical & Location Foreign Keys
        public int? SurgeryId { get; set; }
        public int? TherapyId { get; set; }
        public int? MedicalDeptId { get; set; }
        public int? WardId { get; set; }
        public int? DoctorId { get; set; }
        public int? RoomId { get; set; }

        [ForeignKey(nameof(TreatmentStatus))]
        public int TreatmentStatusId { get; set; }

        [ForeignKey(nameof(TreatmentType))]
        public int TreatmentTypeId { get; set; }

        // Navigation Properties
        public Visit? Visit { get; set; }
        public Consultation? Consultation { get; set; }
        public BillItem? BillItem { get; set; }
        public TreatmentStatus? TreatmentStatus { get; set; }
        public TreatmentType? TreatmentType { get; set; }

        [ForeignKey(nameof(SurgeryId))]
        public Surgery? Surgery { get; set; }

        [ForeignKey(nameof(TherapyId))]
        public Therapy? Therapy { get; set; }

        [ForeignKey(nameof(MedicalDeptId))]
        public MedicalDepartment? MedicalDepartment { get; set; }

        [ForeignKey(nameof(WardId))]
        public Ward? Ward { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public Doctor? Doctor { get; set; }

        [ForeignKey(nameof(RoomId))]
        public Room? Room { get; set; }
    }
}
