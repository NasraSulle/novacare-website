using NovaCare.Shared.DTOs.JoinDto;
using NovaCare.Shared.DTOs.PatientDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.PaymentDTOs
{
    public class PaymentDto
    {
        //properties
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; } = 0m;

        //foreign keys
        public int PaymentStatusId { get; set; }
        public int BillId { get; set; }

        //navigation properties
        public BillDto? Bill { get; set; }
        public PaymentStatusDto? PaymentStatus { get; set; }
        public List<PatientPaymentModeDto>? PatientPaymentModes { get; set; }
    }
}
