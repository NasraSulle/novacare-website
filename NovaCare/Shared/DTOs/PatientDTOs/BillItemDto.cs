using NovaCare.Modals.PatientModals;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class BillItemDto
    {
        public int Id { get; set; }
        public string Itemname { get; set; } = string.Empty;
        public int Qty { get; set; } = 1;
        public decimal cost { get; set; } = 0m;

        // Foreign keys
        public int BillId { get; set; }

        public BillDto? Bill { get; set; }
        public string BillTypeName { get; set; } = string.Empty;

    }
}
