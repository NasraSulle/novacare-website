using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace NovaCare.Shared.DTOs.DoctorDTOs
{
    public class DoctorTypeDto
    {
        //properties
        public int Id { get; set; }
        public string DoctorTypeCode { get; set; } = string.Empty;
        public string DoctorTypeName { get; set; } = string.Empty;

        //navigation properties
        public List<DoctorDto>? Doctors { get; set; }
    }
}
