using NovaCare.Shared.DTOs.DoctorDTOs;
using NovaCare.Shared.DTOs.PatientDTOs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace NovaCare.Shared.DTOs.JoinDTOs
{
    public class DoctorPatientDto
    {
        //properties
        public int DoctorPatientId { get; set; }

        //foreign keys
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        //navigation properties
        public PatientDto? Patient { get; set; }
        public DoctorDto? Doctor { get; set; }
    }
}
