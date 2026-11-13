using NovaCare.Modals.PatientModals;
using NovaCare.Modals.PaymentModals;
using NovaCare.Shared.DTOs.JoinDto;
using NovaCare.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Shared.DTOs.PaymentDTOs
{
    public class PaymentDto
    {
        // Properties
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; } = 0m;

        public PaymentType PayType { get; set; }
        public string? CashReferenceNumber { get; set; }
        public string? CashReceivedBy { get; set; }
        public string? ChequeNumber { get; set; }
        public BankType? ChequeBank { get; set; }
        public string? ChequeAccName { get; set; }
        public string? ChequeAccNumber { get; set; }
        public string? WireReferenceNo { get; set; }
        public DateTime? WireTransferDate { get; set; }
        public string? WireSourceAcc { get; set; }
        public BankType? WireSourceBank { get; set; }
        public string? CardHolder { get; set; }
        public string? CardNumber { get; set; }
        public CardType? CardOfType { get; set; }
        public string? MNONumber { get; set; }
        public string? MNOReference { get; set; }
        public string? MNOOperator { get; set; }

        //foreign keys
        public int PaymentStatusId { get; set; }
        //public int BillId { get; set; }
        public int PaymentModeId { get; set; }

        //navigation properties
        public PaymentMode? PaymentMode { get; set; }
        public Bill? Bill { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }

        // UI Helpers
        public string PaymentStatusName { get; set; } = string.Empty;
        public string PaymentModeName { get; set; } = string.Empty;
        public string BillTypeName { get; set; } = string.Empty;
        public int BillId { get; set; }

        public decimal ConfirmedAmount { get; set; } = 0m;

    }
}
