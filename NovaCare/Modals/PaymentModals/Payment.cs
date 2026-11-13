using NovaCare.Modals.JoinTables;
using NovaCare.Modals.PatientModals;
using NovaCare.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NovaCare.Modals.PaymentModals
{
    public class Payment
    {
        //properties
        [Key]
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
        [ForeignKey(nameof(PaymentStatus))]
        public int PaymentStatusId { get; set; }

        //[ForeignKey(nameof(Bill))]
        //public int BillId { get; set; }

        [ForeignKey(nameof(PaymentMode))]
        public int PaymentModeId { get; set; }

        //navigation properties
        public PaymentMode? PaymentMode { get; set; }
        public Bill? Bill { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
    }
}
