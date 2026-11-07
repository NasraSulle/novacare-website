using NovaCare.Modals.JoinTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovaCare.Modals.PaymentModals
{
    public class PaymentMode
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string PaymentModeName { get; set; } = string.Empty;

        //navigation properties
        public List<PatientPaymentMode>? PatientPaymentModes { get; set; }
    }
}
