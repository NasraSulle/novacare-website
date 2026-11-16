using NovaCare.Modals.PaymentModals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.InsuranceModals
{
    public class InsurerTariff
    {
        [Key]
        public int Id { get; set; }

        // Maps to your BillItem.SourceType & SourceItemId ("Consultation", "LabTest", "Medicine", "Therapy", "Surgery", "Ward")
        [Required]
        public string ServiceType { get; set; } = string.Empty;
        public int ServiceItemId { get; set; }

        public string InsurerItemCode { get; set; } = string.Empty; // Official NHIF/ZHIF/TPA tariff item code
        public string InsurerItemName { get; set; } = string.Empty;

        public decimal AgreedPrice { get; set; } = 0m;              // Negotiated/fixed price for this insurer
        public decimal CoveragePercent { get; set; } = 100m;        // Item-specific coverage override (e.g., 0% if excluded)
        public bool IsCovered { get; set; } = true;
        public bool RequiresPreApproval { get; set; } = false;

        // Foreign Keys
        [ForeignKey(nameof(Insuarance))]
        public int InsuaranceId { get; set; }

        [ForeignKey(nameof(InsuranceScheme))]
        public int? InsuranceSchemeId { get; set; } // Null = applies to all schemes under this insurer

        // Navigation Properties
        public Insuarance? Insuarance { get; set; }
        public InsuranceScheme? InsuranceScheme { get; set; }
    }
}
