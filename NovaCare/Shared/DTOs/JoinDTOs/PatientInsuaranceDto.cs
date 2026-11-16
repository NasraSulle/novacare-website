namespace NovaCare.Shared.DTOs.JoinDto
{
    public class PatientInsuaranceDto
    {
        public int PatientInsuaranceId { get; set; }

        // Card & Policy Details
        public string MemberCardNumber { get; set; } = string.Empty;
        public string? PrincipalMemberName { get; set; }
        public string RelationshipToPrincipal { get; set; } = "Self";

        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool IsPrimary { get; set; } = true;
        public bool IsActive { get; set; } = true;

        // Foreign Keys
        public int PatientId { get; set; }
        public int InsuaranceId { get; set; }
        public int? InsuranceSchemeId { get; set; }

        // UI Helpers
        public string PatientFullName { get; set; } = string.Empty;
        public string InsuranceProviderName { get; set; } = string.Empty; // e.g., "NHIF", "Jubilee"
        public string InsuranceSchemeName { get; set; } = string.Empty;   // e.g., "Toto Afya", "Jubilee Gold"
        public decimal DefaultCoveragePercent { get; set; } = 100m;
        public bool IsExpired => ValidTo.HasValue && ValidTo.Value.Date < DateTime.UtcNow.Date;
    }
}
