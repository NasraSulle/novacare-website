using NovaCare.Modals.PatientModals;
using NovaCare.Modals.PaymentModals;
using NovaCare.Shared.DTOs.PaymentDTOs;
using NovaCare.Shared.DTOs.StaffDTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class BillDto
    {
        // Properties
        public int Id { get; set; }
        public DateTime BillDate { get; set; }
        public string BillNumber { get; set; } = $"NCMH-BN-{DateTime.Now.ToString("yyMMdd")}";

        public string BillDescription { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; } = 0m;

        public decimal PaidAmount { get; set; } = 0m;

        // Foreign keys
        public int VisitId { get; set; }
        public int BillTypeId { get; set; }
        public int? PaymentId { get; set; }
        public string StaffId { get; set; } = string.Empty;

        // Navigation properties
        //public BillTypeDto? BillType { get; set; }
        public StaffDto? Staff { get; set; }
        public VisitDto? Visit { get; set; }
        public PaymentDto? Payment { get; set; } = new();
        public List<BillItemDto> BillItems { get; set; } = new();


        public decimal BalanceDue => TotalAmount - PaidAmount;


        // UI Helpers
        public string VisitName { get; set; } = string.Empty;
        public string BillTypeName { get; set; } = string.Empty;
        public string PatientFullName { get; set; } = string.Empty;
        public string PaymentModeName { get; set; } = "cash";
        public string PaymentStatusName { get; set; } = string.Empty;
        public string StaffFullName { get; set; } = string.Empty;
    }
}




