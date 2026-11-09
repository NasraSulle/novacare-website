using System.ComponentModel.DataAnnotations;

namespace NovaCare.Modals.PatientModals
{
    public class VisitStatus
    {
        [Key]
        public int Id { get; set; }
        public string VisitStatusName { get; set; } = string.Empty;

        public List<Visit>? Visits { get; set; }
    }
}
