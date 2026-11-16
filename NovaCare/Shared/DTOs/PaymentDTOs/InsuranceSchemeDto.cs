using NovaCare.Shared.DTOs.InsuranceDTOs;

namespace NovaCare.Shared.DTOs.PaymentDTOs
{
    public class InsuranceSchemeDto
    {
        public int Id { get; set; }
        public string SchemeName { get; set; } = string.Empty;
        public string? SchemeCode { get; set; }

        // Default Coverage & Limits
        public decimal DefaultCoveragePercent { get; set; } = 100m;
        public decimal DefaultCopayPercent { get; set; } = 0m;
        public decimal? AnnualLimit { get; set; }
        public decimal? PerVisitLimit { get; set; }

        public bool IsActive { get; set; } = true;

        // Foreign Key
        public int InsuaranceId { get; set; }

        // UI Helpers
        public string InsuranceProviderName { get; set; } = string.Empty;
        public List<InsurerTariffDto> Tariffs { get; set; } = new();
    }
}
