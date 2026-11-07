using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.PaymentDTOs
{
    public class PaymentStatusDto
    {
        //properties
        public int Id { get; set; }
        public string PaymentStatusName { get; set; } = string.Empty;

        //navigation properties
        public List<PaymentDto>? Payments { get; set; }
    }
}
