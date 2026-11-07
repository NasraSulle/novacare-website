using NovaCare.Modals.JoinTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NovaCare.Modals.MedicineModals
{
    public class Medicine
    {
        //properties
        [Key]
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
        [ForeignKey(nameof(MedicineStatus))]
        public int MedicineStatusId { get; set; }

        //navigation properties
        public MedicineStatus? MedicineStatus { get; set; }
        //public List<MedicineMedicineType>? MedicineMedicineTypes { get; set; }
        //public List<MedicineTreatment>? MedicineTreatments { get; set; }
    }
}
