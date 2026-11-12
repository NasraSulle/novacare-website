namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class BillItemDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string SourceType { get; set; } = string.Empty; // "Consultation", "Prescription", "Treatment", "LabTest"
        public int SourceItemId { get; set; } // ID of the Consultation, Prescription, Treatment, or LabTest
        public double Quantity { get; set; } = 1d;
        public decimal UnitPrice { get; set; } = 0m;
        public decimal SubTotal => (decimal)Quantity * UnitPrice;

        // Foreign keys
        public int BillId { get; set; }

        // UI Helpers
        public string BillTypeName { get; set; } = string.Empty;

    }
}
