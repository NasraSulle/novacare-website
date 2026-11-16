using NovaCare.Shared.DTOs.JoinDto;
using NovaCare.Shared.Enums;

namespace NovaCare.Shared.DTOs.PaymentDTOs
{
    public class InsuaranceDto
    {
        // Properties
        public int Id { get; set; }
        public string InsuaranceName { get; set; } = string.Empty; // NHIF, ZHIF, Jubilee, Strategis, AAR, Britam
        public string InsuranceNumber { get; set; } = string.Empty; // Hospital facility code with this insurer
        public int PrivillageCodeNumber { get; set; }

        // Multi-Insurer Configuration
        public InsuranceIntegrationType IntegrationType { get; set; } = InsuranceIntegrationType.Manual;
        public string IntegrationTypeName => IntegrationType.ToString().Replace("_", " ");
        public bool RequiresPreAuth { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public string? PortalUrl { get; set; }

        // Navigation properties
        public List<InsuranceSchemeDto> Schemes { get; set; } = new();
        public List<PatientInsuaranceDto>? PatientInsuarances { get; set; }
    }
}
