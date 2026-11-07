using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovaCare.Modals.PaymentModals
{
    public class PaymentStatus
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string PaymentStatusName { get; set; } = string.Empty;

        //navigation properties
        public List<Payment>? Payments { get; set; }
    }
}
