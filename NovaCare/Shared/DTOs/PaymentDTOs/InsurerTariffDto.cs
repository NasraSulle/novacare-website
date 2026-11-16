namespace NovaCare.Shared.DTOs.InsuranceDTOs
{
    public class InsurerTariffDto
    {
        public int Id { get; set; }

        // Maps to BillItem.SourceType & SourceItemId
        public string ServiceType { get; set; } = string.Empty; // "Consultation", "LabTest", "Medicine", "Therapy", "Surgery", "Ward"
        public int ServiceItemId { get; set; }

        public string InsurerItemCode { get; set; } = string.Empty; // NHIF/ZHIF/TPA code
        public string InsurerItemName { get; set; } = string.Empty;

        public decimal AgreedPrice { get; set; } = 0m;
        public decimal CoveragePercent { get; set; } = 100m;
        public bool IsCovered { get; set; } = true;
        public bool RequiresPreApproval { get; set; } = false;

        // Foreign Keys
        public int InsuaranceId { get; set; }
        public int? InsuranceSchemeId { get; set; }

        // UI Helpers
        public string InsuranceProviderName { get; set; } = string.Empty;
        public string InsuranceSchemeName { get; set; } = string.Empty;
        public string StandardHospitalItemName { get; set; } = string.Empty;
        public decimal StandardCashPrice { get; set; } = 0m;
    }
}
