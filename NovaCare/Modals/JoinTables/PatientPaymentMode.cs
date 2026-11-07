using NovaCare.Modals.PatientModals;
using NovaCare.Modals.PaymentModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NovaCare.Modals.JoinTables
{
    public class PatientPaymentMode
    {
        //properties
        [Key]
        public int PatientPaymentModeId { get; set; }

        //foreign keys
        [ForeignKey(nameof(PaymentMode))]
        public int PaymentModeId { get; set; }
        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }

        //navigation properties
        public Patient? Patient { get; set; }
        public PaymentMode? PaymentMode { get; set; }
    }
}
