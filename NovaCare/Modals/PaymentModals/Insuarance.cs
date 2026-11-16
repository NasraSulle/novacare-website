using NovaCare.Modals.InsuranceModals;
using NovaCare.Modals.JoinTables;
using NovaCare.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace NovaCare.Modals.PaymentModals
{
    public class Insuarance
    {
        [Key]
        public int Id { get; set; }
        public string InsuaranceName { get; set; } = string.Empty; // e.g., NHIF, ZHIF, Jubilee, Strategis, AAR, Britam
        public string InsuranceNumber { get; set; } = string.Empty; // Hospital's facility/provider code with this insurer
        public int PrivillageCodeNumber { get; set; }

        // Multi-Insurer Configuration
        public InsuranceIntegrationType IntegrationType { get; set; } = InsuranceIntegrationType.Manual;
        public bool RequiresPreAuth { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public string? PortalUrl { get; set; } // Optional API or TPA portal endpoint

        // Navigation Properties
        public List<PatientInsuarance> PatientInsuarances { get; set; } = new();
        public List<InsuranceScheme> Schemes { get; set; } = new();
    }
}
