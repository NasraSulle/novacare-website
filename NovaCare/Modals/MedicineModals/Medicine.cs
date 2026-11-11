using NovaCare.Modals.TreatmentModals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.MedicineModals
{
    public class Medicine
    {
        [Key]
        public int Id { get; set; }
        public byte[]? Picture { get; set; }
        public string MedicineName { get; set; } = string.Empty;
        public string MedicineGenre { get; set; } = string.Empty;
        public int? MedicineGenreId { get; set; }
        public string MedicineType { get; set; } = string.Empty;
        public int? MedicineTypeId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public double MedicineQty { get; set; } = 0d; // Available stock on shelf
        public decimal BuyingPrice { get; set; } = 0m;
        public decimal SellingPrice { get; set; } = 0m;

        // Foreign keys
        [ForeignKey(nameof(MedicineStatus))]
        public int MedicineStatusId { get; set; }

        //[ForeignKey(nameof(MedicineType))]
        //public int? MedicineTypeId { get; set; }

        // Navigation properties
        public MedicineStatus? MedicineStatus { get; set; }
        //public MedicineType? MedicineType { get; set; }

        // Reverse navigation (1 medicine can be prescribed across many visits)
        public List<Prescription> Prescriptions { get; set; } = new();
    }
}
