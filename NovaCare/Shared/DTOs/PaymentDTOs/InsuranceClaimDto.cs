using NovaCare.Shared.Enums;

namespace NovaCare.Shared.DTOs.PaymentDTOs
{
    public class InsuranceClaimDto
    {
        public int Id { get; set; }
        public string ClaimNumber { get; set; } = string.Empty;
        public string? ExternalReferenceNumber { get; set; }
        public string? FolioNumber { get; set; }
        public string? AuthorizationNumber { get; set; }

        // Financial Amounts
        public decimal ClaimedAmount { get; set; } = 0m;
        public decimal ApprovedAmount { get; set; } = 0m;
        public decimal RejectedAmount { get; set; } = 0m;
        public decimal SettledAmount { get; set; } = 0m;
        public decimal PendingSettlementAmount => ApprovedAmount - SettledAmount;

        // Lifecycle & Dates
        public ClaimStatus Status { get; set; } = ClaimStatus.Draft;
        public string StatusName => Status.ToString().Replace("_", " ");
        public DateTime CreatedAt { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public DateTime? AdjudicatedDate { get; set; }
        public DateTime? SettledDate { get; set; }

        // Clinical & Audit Info
        public string? PrimaryIcd10Code { get; set; }
        public string? QueryOrRejectionReason { get; set; }

        // Foreign Keys
        public int BillId { get; set; }
        public int InsuaranceId { get; set; }
        public int? PatientInsuaranceId { get; set; }
        public string? SubmittedByStaffId { get; set; }

        // UI Helpers
        public string BillNumber { get; set; } = string.Empty;
        public string InsuranceProviderName { get; set; } = string.Empty;
        public string PatientFullName { get; set; } = string.Empty;
        public string MemberCardNumber { get; set; } = string.Empty;
        public string SubmittedByStaffName { get; set; } = string.Empty;
    }
}
