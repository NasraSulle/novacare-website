using NovaCare.Modals.JoinTables;
using NovaCare.Modals.PatientModals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.InsuranceModals
{
    public class VisitInsuranceAuth
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AuthorizationNumber { get; set; } = string.Empty; // e.g., NHIF/ZHIF Auth No or TPA Pre-Auth Code

        public string CardNumber { get; set; } = string.Empty;          // Snapshot of card used during this visit
        public string? ReferralNumber { get; set; }                     // Required if NHIF referral patient
        public string? SchemeCode { get; set; }                         // e.g., NHIF scheme code returned on verification

        public decimal? ApprovedLimit { get; set; }                     // Max limit approved for this visit (if capped by TPA)
        public bool IsVerified { get; set; } = false;
        public string VerificationStatus { get; set; } = "Pending";     // "Verified", "Pending", "Failed"
        public string? VerificationRemarks { get; set; }

        public DateTime AuthorizedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }

        // Foreign Keys
        [ForeignKey(nameof(Visit))]
        public int VisitId { get; set; }

        [ForeignKey(nameof(PatientInsuarance))]
        public int PatientInsuaranceId { get; set; }

        // Navigation Properties
        public Visit? Visit { get; set; }
        public PatientInsuarance? PatientInsuarance { get; set; }
    }
}
