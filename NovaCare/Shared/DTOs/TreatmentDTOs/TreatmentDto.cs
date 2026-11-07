using NovaCare.Shared.DTOs.JoinDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.TreatmentDTOs
{
    public class TreatmentDto
    {
        //properties
        public int Id { get; set; }
        public string TreatmentCode { get; set; } = string.Empty;
        public string TreatmentName { get; set; } = string.Empty;


        //foreign keys
        public int TreatmentStatusId { get; set; }
        public int TreatmentTypeId { get; set; }

        //navigation properties
        public TreatmentStatusDto? TreatmentStatus { get; set; }
        public TreatmentTypeDto? TreatmentType { get; set; }
        public List<DiseaseTreatmentDto>? DiseaseTreatments { get; set; }
        public List<MedicineTreatmentDto>? MedicineTreatments { get; set; }

    }
}
