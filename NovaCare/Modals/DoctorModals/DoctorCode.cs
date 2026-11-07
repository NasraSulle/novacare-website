using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.DoctorModals
{
    [Index(nameof(DocCode), IsUnique = true)]
    public class DoctorCode
    {
        [Key]
        public int Id { get; set; }

        public int DocCode { get; set; }

        [ForeignKey(nameof(Doctor))]
        public int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

    }
}
