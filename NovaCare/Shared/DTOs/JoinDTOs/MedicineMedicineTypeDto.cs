using NovaCare.Shared.DTOs.MedicineDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.JoinDto
{
    public class MedicineMedicineTypeDto
    {
        //properties
        public int MedicineMedicineTypeId { get; set; }

        //foreign keys
        public int MedicineTypeId { get; set; }
        public int MedicineId { get; set; }

        //navigation properties
        public MedicineDto? Medicine { get; set; }
        public MedicineTypeDto? MedicineType { get; set; }

    }
}
