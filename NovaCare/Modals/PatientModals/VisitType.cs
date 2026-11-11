using System.ComponentModel.DataAnnotations;

namespace NovaCare.Modals.PatientModals
{
    public class VisitType
    {
        [Key]
        public int Id { get; set; }
        public string TypeName { get; set; } = string.Empty; // Note: Keep as TypeNme if you don't want to rename the DB column

        // Navigation property
        public List<Visit> Visits { get; set; } = new();
    }
}
