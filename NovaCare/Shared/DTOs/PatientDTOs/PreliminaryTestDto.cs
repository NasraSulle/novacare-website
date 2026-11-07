using NovaCare.Modals.PatientModals;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class PreliminaryTestDto
    {
        public int Id { get; set; }

        public decimal PreliminaryWeight { get; set; } = 0m;
        public decimal PreliminaryHeight { get; set; } = 0m;
        public decimal PreliminaryPressure { get; set; } = 0m;

        [ForeignKey(nameof(Visit))]
        public int VisitId { get; set; }
        public Visit? Visit { get; set; }
    }
}
