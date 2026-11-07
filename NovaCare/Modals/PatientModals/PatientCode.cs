using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.PatientModals
{
    public class PatientCode
    {
        [Key]
        public int Id { get; set; }

        public int PatientCodeNumber { get; set; }

        [ForeignKey(nameof(Patient))]
        public int? PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}
