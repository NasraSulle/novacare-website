using NovaCare.Modals.InsuranceModals;
using NovaCare.Modals.PatientModals;
using NovaCare.Modals.PaymentModals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.JoinTables
{
    public class PatientInsuarance
    {
        [Key]
        public int PatientInsuaranceId { get; set; }

        // Card & Policy Details
        [Required]
        public string MemberCardNumber { get; set; } = string.Empty; // Patient's actual NHIF/Jubilee/Strategis card number
        public string? PrincipalMemberName { get; set; }             // Cardholder name (if patient is a spouse/child dependent)
        public string RelationshipToPrincipal { get; set; } = "Self"; // "Self", "Spouse", "Child", "Dependent"

        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool IsPrimary { get; set; } = true;
        public bool IsActive { get; set; } = true;

        // Foreign Keys
        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }

        [ForeignKey(nameof(Insuarance))]
        public int InsuaranceId { get; set; }

        [ForeignKey(nameof(InsuranceScheme))]
        public int? InsuranceSchemeId { get; set; } // Optional specific plan (e.g., Jubilee Gold, NHIF Toto Afya)

        // Navigation Properties
        public Patient? Patient { get; set; }
        public Insuarance? Insuarance { get; set; }
        public InsuranceScheme? InsuranceScheme { get; set; }
    }
}
