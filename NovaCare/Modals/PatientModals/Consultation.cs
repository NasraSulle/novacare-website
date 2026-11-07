using NovaCare.Modals.DoctorModals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.PatientModals
{
    public class Consultation
    {
        [Key]
        public int Id { get; set; }
        public string Diagnosis { get; set; } = string.Empty;

        // Foreign keys
        [ForeignKey(nameof(Visit))]
        public int VisitId { get; set; }

        [ForeignKey(nameof(Doctor))]
        public int? DoctorId { get; set; }

        [ForeignKey(nameof(Service))]
        public int? ServiceId { get; set; }

        // Navigation properties
        public Doctor? Doctor { get; set; }
        public Visit? Visit { get; set; }
        public Service? Service { get; set; }
    }
}
