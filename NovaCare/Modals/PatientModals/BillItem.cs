using NovaCare.Modals.LabTestModals;
using NovaCare.Modals.TreatmentModals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.PatientModals
{
    public class BillItem
    {
        [Key]
        public int Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string SourceType { get; set; } = string.Empty; // "Consultation", "Prescription", "Treatment", "LabTest"
        public int SourceItemId { get; set; } // ID of the Consultation, Prescription, Treatment, or LabTest
        public double Quantity { get; set; } = 1d;
        public decimal UnitPrice { get; set; } = 0m;

        // Foreign keys
        [ForeignKey(nameof(Bill))]
        public int BillId { get; set; }

        // Navigation properties
        public Bill? Bill { get; set; }

        // 1-to-1 Reverse Navigation Properties (Paired with BillItemId on each clinical table)
        public Consultation? Consultation { get; set; }
        public Prescription? Prescription { get; set; }
        public Treatment? Treatment { get; set; }
        public LabTest? LabTest { get; set; }
    }
}
