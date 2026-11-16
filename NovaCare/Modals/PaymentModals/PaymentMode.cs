using NovaCare.Modals.PatientModals;
using System.ComponentModel.DataAnnotations;

namespace NovaCare.Modals.PaymentModals
{
    public class PaymentMode
    {
        [Key]
        public int Id { get; set; }
        public string PaymentModeName { get; set; } = string.Empty; // "Cash", "Insurance"

        // Navigation Properties
        public List<Visit> Visits { get; set; } = new();
        public List<Bill> Bills { get; set; } = new();
    }
}
