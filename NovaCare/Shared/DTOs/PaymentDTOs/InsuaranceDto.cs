using NovaCare.Shared.DTOs.JoinDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.PaymentDTOs
{
    public class InsuaranceDto
    {
        //properties
        public int Id { get; set; }
        public string InsuaranceName { get; set; } = string.Empty;
        public string InsuranceNumber { get; set; } = string.Empty;
        public int PrivillageCodeNumber { get; set; }

        //navigation properties
        public List<PatientInsuaranceDto>? PatientInsuarances { get; set; }
    }
}
