using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.PatientModals
{
    public class BillItem
    {
        [Key]
        public int Id { get; set; }
        public string Itemname { get; set; } = string.Empty;
        public int Qty { get; set; } = 1;
        public decimal cost { get; set; } = 0m;

        // Foreign keys
        [ForeignKey(nameof(Bill))]
        public int BillId { get; set; }

        //navigation properties
        public Bill? Bill { get; set; }
    }
}
