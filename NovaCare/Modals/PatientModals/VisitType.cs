using System.ComponentModel.DataAnnotations;

namespace NovaCare.Modals.PatientModals
{
    public class VisitType
    {
        [Key]
        public int Id { get; set; }
        public string TypeNme { get; set; } = string.Empty;
        public List<Visit>? Visits { get; set; }
    }
}
