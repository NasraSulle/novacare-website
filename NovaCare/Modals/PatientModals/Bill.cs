using NovaCare.Modals.InsuranceModals;
using NovaCare.Modals.JoinTables;
using NovaCare.Modals.PaymentModals;
using NovaCare.Modals.StaffModals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.PatientModals
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public DateTime BillDate { get; set; } = DateTime.UtcNow;
        public string BillNumber { get; set; } = $"NCMH-BN-{DateTime.UtcNow:yyMMddHHmmss}";
        public string BillDescription { get; set; } = string.Empty;

        // Financial Totals (Rolled up from BillItems)
        public decimal TotalAmount { get; set; } = 0m;          // Sum of all BillItem.TotalAmount
        public decimal TotalPatientAmount { get; set; } = 0m;   // Sum of all BillItem.PatientPayableAmount
        public decimal TotalInsuranceAmount { get; set; } = 0m; // Sum of all BillItem.InsuranceCoveredAmount

        // Payment Tracking (Separated so Patient Copays and Insurer Settlements don't mix)
        public decimal PatientPaidAmount { get; set; } = 0m;
        public decimal InsurancePaidAmount { get; set; } = 0m;

        [NotMapped]
        public decimal TotalPaidAmount => PatientPaidAmount + InsurancePaidAmount;

        [NotMapped]
        public decimal PatientBalanceDue => TotalPatientAmount - PatientPaidAmount;

        [NotMapped]
        public decimal InsuranceBalanceDue => TotalInsuranceAmount - InsurancePaidAmount;

        [NotMapped]
        public decimal BalanceDue => TotalAmount - TotalPaidAmount;

        // Foreign Keys
        [ForeignKey(nameof(Visit))]
        public int VisitId { get; set; }

        [ForeignKey(nameof(BillType))]
        public int BillTypeId { get; set; }

        // Actual billing mode for this invoice (1 = Cash, 2 = Insurance)
        [ForeignKey(nameof(PaymentMode))]
        public int PaymentModeId { get; set; }

        [ForeignKey(nameof(PaymentStatus))]
        public int PaymentStatusId { get; set; } = 1; // Defaults to 1 = Unpaid

        // Optional: Specific insurance policy used if this bill is billed to insurance
        [ForeignKey(nameof(PatientInsuarance))]
        public int? PatientInsuaranceId { get; set; }

        [ForeignKey(nameof(Staff))]
        public string StaffId { get; set; } = string.Empty;

        // Single Navigation Properties
        public BillType? BillType { get; set; }
        public PaymentMode? PaymentMode { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
        public PatientInsuarance? PatientInsuarance { get; set; }
        public Staff? Staff { get; set; }
        public Visit? Visit { get; set; }
        public InsuranceClaim? InsuranceClaim { get; set; }

        // Collection Navigation Properties (1 Bill -> Many BillItems & Many Payments)
        public List<BillItem> BillItems { get; set; } = new();
        public List<Payment> Payments { get; set; } = new();
    }
}
