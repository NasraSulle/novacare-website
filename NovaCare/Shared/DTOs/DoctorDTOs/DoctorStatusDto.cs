using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace NovaCare.Shared.DTOs.DoctorDTOs
{
    public class DoctorStatusDto
    {
        //properties
        public int Id { get; set; }
        public string DoctorStatusName { get; set; } = string.Empty;

        //navigation properties
        public List<DoctorDto>? Doctors { get; set; }
    }
}
