using NovaCare.Shared.DTOs.MedicineDTOs;
using NovaCare.Shared.DTOs.TreatmentDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.JoinDto
{
    public class MedicineTreatmentDto
    {
        //properties
        public int MedicineTreatmentId { get; set; }

        //foreign keys
        public int MedicineId { get; set; }
        public int TreatmentId { get; set; }

        //navigation properties
        public MedicineDto? Medicine { get; set; }
        public TreatmentDto? Treatment { get; set; }
    }
}
