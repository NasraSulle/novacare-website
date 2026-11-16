namespace NovaCare.Shared.DTOs.InsuranceDTOs
{
    public class VisitInsuranceAuthDto
    {
        public int Id { get; set; }
        public string AuthorizationNumber { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string? ReferralNumber { get; set; }
        public string? SchemeCode { get; set; }

        public decimal? ApprovedLimit { get; set; }
        public bool IsVerified { get; set; } = false;
        public string VerificationStatus { get; set; } = "Pending";
        public string? VerificationRemarks { get; set; }

        public DateTime AuthorizedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }

        // Foreign Keys
        public int VisitId { get; set; }
        public int PatientInsuaranceId { get; set; }

        // UI Helpers
        public string InsuranceProviderName { get; set; } = string.Empty;
        public string InsuranceSchemeName { get; set; } = string.Empty;
        public string PatientFullName { get; set; } = string.Empty;
    }
}
