using NovaCare.Modals.JoinTables;
using NovaCare.Modals.PatientModals;
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

        //foreign keys
        [ForeignKey(nameof(PaymentStatus))]
        public int PaymentStatusId { get; set; }

        [ForeignKey(nameof(Bill))]
        public int BillId { get; set; }

        //navigation properties
        public Bill? Bill { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
        public List<PatientPaymentMode>? PatientPaymentModes { get; set; }
    }
}
