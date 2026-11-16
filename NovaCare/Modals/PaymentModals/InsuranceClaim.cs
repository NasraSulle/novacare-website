using NovaCare.Modals.JoinTables;
using NovaCare.Modals.PatientModals;
using NovaCare.Modals.PaymentModals;
using NovaCare.Modals.StaffModals;
using NovaCare.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.InsuranceModals
{
    public class InsuranceClaim
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ClaimNumber { get; set; } = $"NCMH-CLM-{DateTime.UtcNow:yyMMddHHmmss}";

        public string? ExternalReferenceNumber { get; set; } // Claim ID returned by TPA portal (Smart/LCT)
        public string? FolioNumber { get; set; }             // Specific to NHIF / ZHIF monthly folio batching
        public string? AuthorizationNumber { get; set; }     // Copied from VisitInsuranceAuth

        // Financial Amounts
        public decimal ClaimedAmount { get; set; } = 0m;     // Matches Bill.TotalInsuranceAmount at submission
        public decimal ApprovedAmount { get; set; } = 0m;    // Amount approved by insurer after adjudication
        public decimal RejectedAmount { get; set; } = 0m;    // Amount denied by insurer
        public decimal SettledAmount { get; set; } = 0m;     // Actual cash paid into hospital bank account

        // Lifecycle & Dates
        public ClaimStatus Status { get; set; } = ClaimStatus.Draft;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? SubmittedDate { get; set; }
        public DateTime? AdjudicatedDate { get; set; }
        public DateTime? SettledDate { get; set; }

        // Clinical & Audit Info
        public string? PrimaryIcd10Code { get; set; }        // Required by NHIF & TPAs for claim submission
        public string? QueryOrRejectionReason { get; set; }  // Notes from insurer if queried/rejected
        public string? SubmissionPayloadJson { get; set; }   // Audit log of JSON sent to NHIF/TPA API

        // Foreign Keys
        [ForeignKey(nameof(Bill))]
        public int BillId { get; set; }

        [ForeignKey(nameof(Insuarance))]
        public int InsuaranceId { get; set; }

        [ForeignKey(nameof(PatientInsuarance))]
        public int? PatientInsuaranceId { get; set; }

        [ForeignKey(nameof(SubmittedByStaff))]
        public string? SubmittedByStaffId { get; set; }

        // Navigation Properties
        public Bill? Bill { get; set; }
        public Insuarance? Insuarance { get; set; }
        public PatientInsuarance? PatientInsuarance { get; set; }
        public Staff? SubmittedByStaff { get; set; }
    }
}
