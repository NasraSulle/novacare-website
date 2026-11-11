using NovaCare.Shared.DTOs.PaymentDTOs;

namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class BillDto
    {
        // Properties
        public int Id { get; set; }
        public string BillNumber { get; set; } = string.Empty;
        public DateTime BillDate { get; set; } = DateTime.UtcNow;
        public string BillDescription { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; } = 0m;

        // Live Payment Calculations
        public decimal PaidAmount => Payments.Sum(p => p.Amount);
        public decimal BalanceDue => TotalAmount - PaidAmount;

        // Foreign keys
        public int VisitId { get; set; }
        public int BillTypeId { get; set; }
        public int PaymentStatusId { get; set; } // e.g., 1 = Unpaid, 2 = Partially Paid, 3 = Paid

        // Navigation properties 
        public VisitDto? Visit { get; set; }
        public List<PaymentDto> Payments { get; set; } = new();
        public List<BillItemDto> BillItems { get; set; } = new();

        // UI Helpers
        public string VisitName { get; set; } = string.Empty;
        public string BillTypeName { get; set; } = string.Empty;
        public string PatientFullName { get; set; } = string.Empty;
        public string PaymentStatusName { get; set; } = string.Empty;
    }
}
