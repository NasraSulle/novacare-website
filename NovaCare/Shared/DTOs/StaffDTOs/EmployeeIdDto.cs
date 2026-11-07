using NovaCare.Modals.StaffModals;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Shared.DTOs.StaffDTOs
{
    public class EmployeeIdDto
    {
        public int Id { get; set; }

        public int EmployeeIdCode { get; set; }

        public string? StaffId { get; set; }
        public Staff? Staff { get; set; }
    }
}
