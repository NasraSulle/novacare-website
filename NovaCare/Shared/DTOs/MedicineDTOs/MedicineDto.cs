using NovaCare.Modals.MedicineModals;
using NovaCare.Shared.DTOs.JoinDto;
using NovaCare.Shared.DTOs.PatientDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DateTime CreatedDate { get; set; }
        public double MedicineQty { get; set; } = 0d;
        public decimal BuyingPrice { get; set; } = 0m;
        public decimal SellingPrice { get; set; } = 0m;

        //foreign keys
        public int MedicineStatusId { get; set; }
        public int? MedicineTypeId { get; set; } 

        //navigation properties
        public MedicineStatus? MedicineStatus { get; set; }
        public MedicineType? MedicineType { get; set; }

        //for display
        public string MedicineStatusName { get; set; } = string.Empty;
        public string MedicineTypeName { get; set; } = string.Empty;

        public int? BillId { get; set; }
        public BillDto? Bill { get; set; }

    }
}
