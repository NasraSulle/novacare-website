using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.PatientModals
{
    public class Referral
    {
        [Key]
        public int Id { get; set; }
        public string DoctorFullName { get; set; } = string.Empty;
        public string PatientFullName { get; set; } = string.Empty;
        public string ReferredHospital { get; set; } = string.Empty;
        public string DoctorsNote { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; }

        [ForeignKey(nameof(Consultation))]
        public int ConsultationId { get; set; }

        public Consultation Consultation { get; set; } = new();
    }
}
