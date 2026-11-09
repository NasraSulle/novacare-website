using NovaCare.Modals.MedicineModals;
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

        public int? MedicineGenreId { get; set; }

        //navigation properties
        public MedicineGenre? MedicineGenre { get; set; }
        public List<Medicine>? Medicines { get; set; }
        public string MedicineGenreName { get; set; } = string.Empty;
    }
}
