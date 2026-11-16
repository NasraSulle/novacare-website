using NovaCare.Modals.LabTestModals;
using NovaCare.Modals.TreatmentModals;
using NovaCare.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.PatientModals
{
    public class BillItem
    {
        [Key]
        public int Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string? InsurerItemCode { get; set; } // Official NHIF / ZHIF / TPA tariff code if applicable
        public string SourceType { get; set; } = string.Empty; // "Consultation", "Prescription", "Treatment", "LabTest"
        public int SourceItemId { get; set; } // ID of the Consultation, Prescription, Treatment, or LabTest

        public double Quantity { get; set; } = 1d;
        public decimal UnitPrice { get; set; } = 0m;

        [NotMapped]
        public decimal TotalAmount => UnitPrice * (decimal)Quantity;

        // Per-Item Insurance Coverage Breakdown
        public bool IsInsuranceCovered { get; set; } = false;
        public decimal InsuranceCoveragePercent { get; set; } = 0m; // e.g., 0 for Cash, 100 for full NHIF, 80 for 20% copay
        public decimal InsuranceCoveredAmount { get; set; } = 0m;
        public decimal PatientPayableAmount { get; set; } = 0m;

        // Per-Item Claim Status (Handles partial claim rejections cleanly)
        public ClaimStatus ClaimItemStatus { get; set; } = ClaimStatus.Not_Applicable;
        public string? RejectionReason { get; set; }

        // Foreign Keys
        [ForeignKey(nameof(Bill))]
        public int BillId { get; set; }

        // Navigation Properties
        public Bill? Bill { get; set; }

        // 1-to-1 Reverse Navigation Properties (Paired with BillItemId on each clinical table)
        public Consultation? Consultation { get; set; }
        public Prescription? Prescription { get; set; }
        public Treatment? Treatment { get; set; }
        public LabTest? LabTest { get; set; }
    }
}
