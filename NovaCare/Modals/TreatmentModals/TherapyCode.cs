using Microsoft.EntityFrameworkCore;
using NovaCare.Modals.RoomModals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.TreatmentModals
{
    [Index(nameof(CodeNumber), IsUnique = true)]
    public class TherapyCode
    {
        [Key]
        public int Id { get; set; }

        public int CodeNumber { get; set; }

        [ForeignKey(nameof(Therapy))]
        public string? TherapyId { get; set; }
        public Therapy? Therapy { get; set; }
    }
}
