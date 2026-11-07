using NovaCare.Shared.DTOs.JoinDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.MedicineDTOs
{
    public class MedicineTypeDto
    {
        //properties
        public int Id { get; set; }
        public int MedicineTypeCode { get; set; }
        public string MedicineTypeName { get; set; } = string.Empty;

        //navigation properties
        public List<MedicineMedicineTypeDto>? MedicineMedicineTypes { get; set; }
    }
}
