using NovaCare.Shared.DTOs.PaymentDTOs;
using NovaCare.Shared.DTOs.StaffDTOs;

namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class BillDto
    {
        // Properties
        public int Id { get; set; }
        public DateTime BillDate { get; set; } = DateTime.UtcNow;
        public string BillNumber { get; set; } = $"NCMH-BN-{DateTime.UtcNow:yyMMddHHmmss}";

        public string BillDescription { get; set; } = string.Empty;

        // Bill Totals
        public decimal TotalAmount { get; set; } = 0m;
        public decimal TotalPatientAmount { get; set; } = 0m;
        public decimal TotalInsuranceAmount { get; set; } = 0m;

        // Payments Breakdown
        public decimal PaidAmount { get; set; } = 0m;
        public decimal PatientPaidAmount { get; set; } = 0m;
        public decimal InsurancePaidAmount { get; set; } = 0m;

        // Foreign keys
        public int VisitId { get; set; }
        public int BillTypeId { get; set; }
        public int PaymentModeId { get; set; } = 1;   // 1 = Cash, 2 = Insurance
        public int PaymentStatusId { get; set; } = 1; // 1 = Unpaid, 2 = Partial Payment, 3 = Paid, 4 = Insurance Review
        public int? PatientInsuaranceId { get; set; }
        public string StaffId { get; set; } = string.Empty;

        // Navigation properties
        public StaffDto? Staff { get; set; }
        public VisitDto? Visit { get; set; }
        public List<PaymentDto> Payments { get; set; } = new();
        public List<BillItemDto> BillItems { get; set; } = new();

        // Computed Balances
        public decimal BalanceDue => TotalAmount - PaidAmount;
        public decimal PatientBalanceDue => TotalPatientAmount - PatientPaidAmount;
        public decimal InsuranceBalanceDue => TotalInsuranceAmount - InsurancePaidAmount;

        // UI Helpers
        public string VisitName { get; set; } = string.Empty;
        public string BillTypeName { get; set; } = string.Empty;
        public string PatientFullName { get; set; } = string.Empty;
        public string PaymentModeName { get; set; } = "Cash";
        public string PaymentStatusName { get; set; } = "Unpaid";
        public string StaffFullName { get; set; } = string.Empty;
        public string InsuranceProviderName { get; set; } = string.Empty;
    }
}
