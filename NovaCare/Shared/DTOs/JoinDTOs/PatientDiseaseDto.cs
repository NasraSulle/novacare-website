using NovaCare.Shared.DTOs.DiseaseDTOs;
using NovaCare.Shared.DTOs.PatientDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.JoinDto
{
    public class PatientDiseaseDto
    {
        //properties
        public int PatientDiseaseId { get; set; }

        //foreign keys
        public int DiseaseId { get; set; }
        public int PatientId { get; set; }

        //navigation properties
        public DiseaseDto? Disease { get; set; }
        public PatientDto? Patient { get; set; }

    }
}
