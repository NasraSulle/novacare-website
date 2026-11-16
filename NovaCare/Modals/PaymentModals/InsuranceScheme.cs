using NovaCare.Modals.PaymentModals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.InsuranceModals
{
    public class InsuranceScheme
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SchemeName { get; set; } = string.Empty; // e.g., "NHIF Standard", "Toto Afya", "Jubilee Gold", "Strategis Executive"
        public string? SchemeCode { get; set; }

        // Default Coverage & Limits
        public decimal DefaultCoveragePercent { get; set; } = 100m; // e.g., 100% for NHIF, 80% for 20% copay corporate plans
        public decimal DefaultCopayPercent { get; set; } = 0m;
        public decimal? AnnualLimit { get; set; }
        public decimal? PerVisitLimit { get; set; }

        public bool IsActive { get; set; } = true;

        // Foreign Key
        [ForeignKey(nameof(Insuarance))]
        public int InsuaranceId { get; set; }

        // Navigation Properties
        public Insuarance? Insuarance { get; set; }
        public List<InsurerTariff> Tariffs { get; set; } = new();
    }
}
