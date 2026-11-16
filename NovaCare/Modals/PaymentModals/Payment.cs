using NovaCare.Modals.InsuranceModals;
using NovaCare.Modals.PatientModals;
using NovaCare.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.PaymentModals
{
    public class Payment
    {
        [Key]
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

        // Insurance Settlement Details (Used when PayType == PaymentType.Insurance)
        public string? InsurerSettlementReference { get; set; } // Bank remittance / EFT reference from NHIF/Jubilee
        public string? SettlementBatchNumber { get; set; }

        // Foreign Keys
        [ForeignKey(nameof(Bill))]
        public int BillId { get; set; }

        [ForeignKey(nameof(PaymentStatus))]
        public int PaymentStatusId { get; set; } = 3; // Defaults to 3 = Paid once transaction is recorded

        [ForeignKey(nameof(InsuranceClaim))]
        public int? InsuranceClaimId { get; set; } // Links payment to the settled claim if PayType == Insurance

        // Navigation Properties
        public Bill? Bill { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
        public InsuranceClaim? InsuranceClaim { get; set; }
    }
}
