using NovaCare.Shared.Enums;

namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class BillItemDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string SourceType { get; set; } = string.Empty; // "Consultation", "Prescription", "Treatment", "LabTest"
        public int SourceItemId { get; set; } // ID of the Consultation, Prescription, Treatment, or LabTest
        public string? InsurerItemCode { get; set; } // Official NHIF/ZHIF/TPA code

        public double Quantity { get; set; } = 1d;
        public decimal UnitPrice { get; set; } = 0m;
        public decimal SubTotal => (decimal)Quantity * UnitPrice;

        // Item-Level Insurance Coverage
        public bool IsInsuranceCovered { get; set; } = false;
        public decimal InsuranceCoveragePercent { get; set; } = 0m;
        public decimal InsuranceCoveredAmount { get; set; } = 0m;
        public decimal PatientPayableAmount { get; set; } = 0m;

        // Claim Status per Item
        public ClaimStatus ClaimItemStatus { get; set; } = ClaimStatus.Not_Applicable;
        public string? RejectionReason { get; set; }

        // Foreign keys
        public int BillId { get; set; }

        // UI Helpers
        public string BillTypeName { get; set; } = string.Empty;
        public bool IsAdded { get; set; }
    }
}
