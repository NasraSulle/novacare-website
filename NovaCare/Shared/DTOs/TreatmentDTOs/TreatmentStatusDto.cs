using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.TreatmentDTOs
{
    public class TreatmentStatusDto
    {
        //properties
        public int Id { get; set; }
        public string TreatmentStatusName { get; set; } = string.Empty;

        //navigation properties
        public List<TreatmentDto>? Treatments { get; set; }
    }
}
