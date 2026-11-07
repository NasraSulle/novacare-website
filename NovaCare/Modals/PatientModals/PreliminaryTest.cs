using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.PatientModals
{
    public class PreliminaryTest
    {
        [Key]
        public int Id { get; set; }

        public decimal PreliminaryWeight { get; set; } = 0m;
        public decimal PreliminaryHeight { get; set; } = 0m;
        public decimal PreliminaryPressure { get; set; } = 0m;

        [ForeignKey(nameof(Visit))]
        public int VisitId { get; set; }
        public Visit? Visit { get; set; }
    }
}
