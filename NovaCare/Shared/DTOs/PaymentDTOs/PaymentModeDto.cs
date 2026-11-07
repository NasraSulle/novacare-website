using NovaCare.Shared.DTOs.JoinDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.PaymentDTOs
{
    public class PaymentModeDto
    {
        //properties
        public int Id { get; set; }
        public string PaymentModeName { get; set; } = string.Empty;

        //navigation properties
        public List<PatientPaymentModeDto>? PatientPaymentModes { get; set; }
    }
}
