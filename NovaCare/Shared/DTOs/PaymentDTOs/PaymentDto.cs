using NovaCare.Shared.DTOs.JoinDto;

namespace NovaCare.Shared.DTOs.PaymentDTOs
{
    public class PaymentDto
    {
        // Properties
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; } = 0m;

        // Foreign keys
        public int PaymentStatusId { get; set; }
        public int BillId { get; set; }
        public int PaymentModeId { get; set; }

        // Navigation properties (Omit BillDto? Bill to avoid circular JSON reference loops)
        public PaymentStatusDto? PaymentStatus { get; set; }
        public PaymentModeDto? PaymentModeDto { get; set; }

        // UI Helpers
        public string PaymentStatusName { get; set; } = string.Empty;
        public string PaymentModeName { get; set; } = string.Empty;
    }
}
