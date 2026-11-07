using NovaCare.Shared.DTOs.PatientDTOs;
using NovaCare.Shared.DTOs.PaymentDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.JoinDto
{
    public class PatientPaymentModeDto
    {
        //properties
        public int PatientPaymentModeId { get; set; }

        //foreign keys
        public int PaymentModeId { get; set; }
        public int PatientId { get; set; }

        //navigation properties
        public PatientDto? Patient { get; set; }
        public PaymentModeDto? PaymentMode { get; set; }
    }
}
