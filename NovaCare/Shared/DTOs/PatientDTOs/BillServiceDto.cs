namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class BillServiceDto
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public int ServiceId { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } = 0m;
        public decimal Amount { get; set; } = 0m;
        public decimal InsuranceDeduction { get; set; } = 0m;
        public decimal? VAT { get; set; }
        public decimal NetAmountDue { get; set; } = 0m;

        // Navigation properties
        public ServiceDto? Service { get; set; }
        public BillDto? Bill { get; set; }

        // UI Helpers
        public bool IsSelected { get; set; }
        public string ServiceName { get; set; } = string.Empty;
    }
}
