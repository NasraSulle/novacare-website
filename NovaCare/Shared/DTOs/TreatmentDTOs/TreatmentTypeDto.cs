using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.TreatmentDTOs
{
    public class TreatmentTypeDto
    {
        //properties
        public int Id { get; set; }
        public string TreatmentTypeCode { get; set; } = string.Empty;
        public string TreatmentTypeName { get; set; } = string.Empty;


        //navigation properties
        public List<TreatmentDto>? Treatments { get; set; }

    }
}
