using System.ComponentModel.DataAnnotations;

namespace NovaCare.Modals.PatientModals
{
    public class BillType
    {
        [Key]
        public int Id { get; set; }
        public string TypeNme { get; set; } = string.Empty;
        public List<Bill>? Bills { get; set; }
    }
}
