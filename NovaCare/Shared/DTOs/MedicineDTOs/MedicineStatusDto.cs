using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.MedicineDTOs
{
    public class MedicineStatusDto
    {
        //properties
        public int Id { get; set; }
        public string MedicineStatusName { get; set; } = string.Empty;

        //navigation properties
        public List<MedicineDto>? Medicines { get; set; }
    }
}
