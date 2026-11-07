using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class PatientTypeDto
    {
        //properties
        public int Id { get; set; }
        public string PatientTypeCode { get; set; } = string.Empty;
        public string PatientTypeName { get; set; } = string.Empty;

        //navigation properties
        public List<PatientDto>? Patients { get; set; }

    }
}
