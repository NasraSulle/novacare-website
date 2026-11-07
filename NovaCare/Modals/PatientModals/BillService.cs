using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.PatientModals
{
    public class BillService
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Bill))]
        public int BillId { get; set; }

        [ForeignKey(nameof(Service))]
        public int ServiceId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; } = 0m;

        public decimal Amount { get; set; } = 0m;

        public decimal InsuranceDeduction { get; set; } = 0m;

        public decimal? VAT { get; set; }

        public decimal NetAmountDue { get; set; } = 0m;

        // Navigation properties
        public Service? Service { get; set; }
        public Bill? Bill { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; }
    }
}
