using NovaCare.Shared.Enums;

namespace NovaCare.Shared.DTOs.PaymentDTOs
{
    public class PaymentDto
    {
        // Properties
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public decimal Amount { get; set; } = 0m;

        public PaymentType PayType { get; set; }

        // Cash Details
        public string? CashReferenceNumber { get; set; }
        public string? CashReceivedBy { get; set; }

        // Cheque Details
        public string? ChequeNumber { get; set; }
        public BankType? ChequeBank { get; set; }
        public string? ChequeAccName { get; set; }
        public string? ChequeAccNumber { get; set; }

        // Wire / Bank Transfer Details
        public string? WireReferenceNo { get; set; }
        public DateTime? WireTransferDate { get; set; }
        public string? WireSourceAcc { get; set; }
        public BankType? WireSourceBank { get; set; }

        // Card Details
        public string? CardHolder { get; set; }
        public string? CardNumber { get; set; }
        public CardType? CardOfType { get; set; }

        // Mobile Money (MNO) Details
        public string? MNONumber { get; set; }
        public string? MNOReference { get; set; }
        public string? MNOOperator { get; set; }

        // Insurance Settlement Details (When PayType == PaymentType.Insurance)
        public string? InsurerSettlementReference { get; set; }
        public string? SettlementBatchNumber { get; set; }

        // Foreign keys
        public int BillId { get; set; }
        public int PaymentStatusId { get; set; } = 3; // 3 = Paid
        public int? InsuranceClaimId { get; set; }

        // UI Helpers
        public string PaymentStatusName { get; set; } = string.Empty;
        public string PayTypeName => PayType.ToString().Replace("_", " ");
        public string BillNumber { get; set; } = string.Empty;
        public string BillTypeName { get; set; } = string.Empty;
        public decimal ConfirmedAmount { get; set; } = 0m;
    }
}
