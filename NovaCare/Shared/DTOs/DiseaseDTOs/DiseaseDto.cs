using NovaCare.Shared.DTOs.JoinDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.DiseaseDTOs
{
    public class DiseaseDto
    {
        //properties
        public int Id { get; set; }
        public string DiseaseCode { get; set; } = string.Empty;
        public string DiseaseName { get; set; } = string.Empty;
        public decimal LabTestCost { get; set; } = 0m;
        public DateTime RegisteredDate { get; set; }
        //foreign keys
        public int DiseaseTypeId { get; set; }

        //navigation properties
        public DiseaseTypeDto? DiseaseType { get; set; }
        public List<DiseaseTreatmentDto>? DiseaseTreatments { get; set; }
        public List<PatientDiseaseDto>? PatientDiseases { get; set; }

        public string DiseaseTypeName { get; set; } = string.Empty;
    }
}
