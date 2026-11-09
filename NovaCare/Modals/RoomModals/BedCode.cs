using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.RoomModals
{
    [Index(nameof(CodeNumber), IsUnique = true)]
    public class BedCode
    {
        [Key]
        public int Id { get; set; }

        public int CodeNumber { get; set; }

        [ForeignKey(nameof(Bed))]
        public string? BedId { get; set; }
        public Bed? Bed { get; set; }
    }
}
