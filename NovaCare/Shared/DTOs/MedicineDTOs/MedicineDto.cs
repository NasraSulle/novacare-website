using NovaCare.Shared.DTOs.JoinDto;

namespace NovaCare.Shared.DTOs.MedicineDTOs
{
    public class MedicineDto
    {
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
        public int MedicineStatusId { get; set; }
        //public int? MedicineTypeId { get; set; }

        // Navigation properties
        public MedicineStatusDto? MedicineStatus { get; set; }
        //public MedicineTypeDto? MedicineType { get; set; }

        // UI Helpers
        public string MedicineStatusName { get; set; } = string.Empty;
        //public string MedicineTypeName { get; set; } = string.Empty;
    }
}
