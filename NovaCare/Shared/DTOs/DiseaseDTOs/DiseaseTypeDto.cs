using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.DiseaseDTOs
{
    public class DiseaseTypeDto
    {
        //properties
        public int Id { get; set; }
        public string DiseaseTypeCode { get; set; } = string.Empty;
        public string DiseaseTypeName { get; set; } = string.Empty;


        //navigation properties
        public List<DiseaseDto>? Diseases { get; set; }
        public int DiseasesCount { get; set; }
    }
}
