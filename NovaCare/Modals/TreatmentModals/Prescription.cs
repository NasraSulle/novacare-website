using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.MedicineModals;
using NovaCare.Modals.PatientModals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.TreatmentModals
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }
        public DateTime PrescribedDate { get; set; } = DateTime.UtcNow;
        public double Quantity { get; set; } = 1d;          // Quantity prescribed by doctor
        public double DispensedQuantity { get; set; } = 0d; // Actual quantity dispensed by pharmacy
        public bool IsDispensed { get; set; } = false;
        public DateTime? DispensedDate { get; set; }

        public string Dosage { get; set; } = string.Empty;  // e.g., "1x3 for 5 days"
        public string Notes { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; } = 0m;        // Snapshot of Medicine.SellingPrice (or InsurerTariff price)

        // Foreign keys
        [ForeignKey(nameof(Visit))]
        public int VisitId { get; set; }

        [ForeignKey(nameof(Consultation))]
        public int? ConsultationId { get; set; } // Null = walk-in pharmacy; HasValue = ordered during consultation

        [ForeignKey(nameof(Medicine))]
        public int MedicineId { get; set; }

        [ForeignKey(nameof(Doctor))]
        public int? DoctorId { get; set; }

        [ForeignKey(nameof(BillItem))]
        public int? BillItemId { get; set; } // Null = unbilled; HasValue = added to a Bill

        // Navigation properties
        public Visit? Visit { get; set; }
        public Consultation? Consultation { get; set; }
        public Medicine? Medicine { get; set; }
        public Doctor? Doctor { get; set; }
        public BillItem? BillItem { get; set; }
    }
}
