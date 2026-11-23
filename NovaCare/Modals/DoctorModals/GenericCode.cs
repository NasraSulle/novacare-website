using Microsoft.EntityFrameworkCore;
using NovaCare.Modals.RoomModals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.DoctorModals
{
    [Index(nameof(CodeNumber), IsUnique = true)]
    public class GenericCode
    {
        [Key]
        public int Id { get; set; }

        public int CodeNumber { get; set; }

        [ForeignKey(nameof(Doctor))]
        public int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        [ForeignKey(nameof(Bed))]
        public int? BedId { get; set; }
        public Bed? Bed { get; set; }

        [ForeignKey(nameof(Room))]
        public int? RoomId { get; set; }
        public Room? Room { get; set; }
    }
}
