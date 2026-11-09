using NovaCare.Modals.PaymentModals;
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
        public string BillDescription { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; } = 0m;

        // Foreign keys
        [ForeignKey(nameof(Visit))]
        public int VisitId { get; set; }

        [ForeignKey(nameof(BillType))]
        public int BillTypeId { get; set; }

        // Navigation properties
        public BillType? BillType { get; set; }
        public Visit? Visit { get; set; }
        public List<Payment>? Payments { get; set; }
    }
}
