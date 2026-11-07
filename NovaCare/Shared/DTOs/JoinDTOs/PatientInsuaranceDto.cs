using NovaCare.Shared.DTOs.PatientDTOs;
using NovaCare.Shared.DTOs.PaymentDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.JoinDto
{
    public class PatientInsuaranceDto
    {
        //properties
        public int PatientInsuaranceId { get; set; }

        //foreign keys
        public int PatientId { get; set; }
        public int InsuaranceId { get; set; }

        //navigation properties
        public PatientDto? Patient { get; set; }
        public InsuaranceDto? Insuarance { get; set; }


    }
}
