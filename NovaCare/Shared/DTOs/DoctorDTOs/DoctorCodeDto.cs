using NovaCare.Modals.DoctorModals;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Shared.DTOs.DoctorDTOs
{
    public class DoctorCodeDto
    {
        public int Id { get; set; }

        public int DocCode { get; set; }

        public int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
