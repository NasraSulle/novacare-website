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

        // Live Payment Calculations (Null-safe)
        public decimal PaidAmount => Payments?.Sum(p => p.Amount) ?? 0m;
        public decimal BalanceDue => TotalAmount - PaidAmount;

        // Computed Payment Status (1 = Unpaid, 2 = Partially Paid, 3 = Fully Paid)
        public int PaymentStatusId
        {
            get
            {
                if (PaidAmount <= 0m)
                    return 1;

                if (PaidAmount < TotalAmount)
                    return 2;

                return 3;
            }
        }

        public string PaymentStatusName => PaymentStatusId switch
        {
            1 => "Unpaid",
            2 => "Partial",
            3 => "Fully Paid",
            _ => "Unpaid"
        };

        // Foreign keys
        public int VisitId { get; set; }
        public int BillTypeId { get; set; }

        // Navigation properties 
        public VisitDto? Visit { get; set; }
        public List<PaymentDto> Payments { get; set; } = new();
        public List<BillItemDto> BillItems { get; set; } = new();

        // UI Helpers
        public string VisitName { get; set; } = string.Empty;
        public string BillTypeName { get; set; } = string.Empty;
        public string PatientFullName { get; set; } = string.Empty;
    }
}
