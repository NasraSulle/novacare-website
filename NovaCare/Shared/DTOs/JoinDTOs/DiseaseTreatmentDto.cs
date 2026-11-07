using NovaCare.Shared.DTOs.DiseaseDTOs;
using NovaCare.Shared.DTOs.TreatmentDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.JoinDto
{
    public class DiseaseTreatmentDto
    {
        //properties
        public int DiseaseTreatmentId { get; set; }

        //foreign keys
        public int DiseaseId { get; set; }
        public int TreatmentId { get; set; }

        //navigation properties
        public DiseaseDto? Disease { get; set; }
        public TreatmentDto? Treatment { get; set; }
    }
}
