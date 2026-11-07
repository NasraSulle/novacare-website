using NovaCare.Shared.DTOs.JoinDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.MedicineDTOs
{
    public class MedicineDto
    {
        //properties
        public int Id { get; set; }
        public byte[]? Picture { get; set; }
        public string MedicineName { get; set; } = string.Empty;
        public string MedicineGenre { get; set; } = string.Empty;
        public string MedicineType { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public double MedicineQty { get; set; } = 0d;
        public decimal BuyingPrice { get; set; } = 0m;
        public decimal SellingPrice { get; set; } = 0m;

        //foreign keys
        public int MedicineStatusId { get; set; }

        //navigation properties
        public MedicineStatusDto? MedicineStatus { get; set; }
        //public List<MedicineMedicineTypeDto>? MedicineMedicineTypes { get; set; }
        //public List<MedicineTreatmentDto>? MedicineTreatments { get; set; }

        //for display
        public string MedicineStatusName { get; set; } = string.Empty;

    }
}
