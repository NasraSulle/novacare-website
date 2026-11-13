using NovaCare.Modals.PaymentModals;
using NovaCare.Modals.StaffModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.PatientModals
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public DateTime BillDate { get; set; }
        public string BillNumber { get; set; } =  $"NCMH-BN-{DateTime.Now.ToString("yyMMdd")}";

        public string BillDescription { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; } = 0m;

        public decimal PaidAmount { get; set; } = 0m;

        // Foreign keys
        [ForeignKey(nameof(Visit))]
        public int VisitId { get; set; }

        [ForeignKey(nameof(BillType))]
        public int BillTypeId { get; set; }

        [ForeignKey(nameof(Payment))]
        public int? PaymentId { get; set; }

        [ForeignKey(nameof(Staff))]
        public string StaffId { get; set; } = string.Empty;

        // Navigation properties
        public BillType? BillType { get; set; }
        public Staff? Staff { get; set; }
        public Visit? Visit { get; set; }
        public Payment? Payment { get; set; } = new();
        public List<BillItem> BillItems { get; set; } = new();


        public decimal BalanceDue => TotalAmount - PaidAmount;

    }
}
