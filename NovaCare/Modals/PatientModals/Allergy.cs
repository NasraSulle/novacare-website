using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.PatientModals
{
    public class Allergy
    {
        [Key]
        public int Id { get; set; }
        public string AllergyName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}
