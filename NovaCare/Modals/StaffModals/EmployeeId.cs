using Microsoft.EntityFrameworkCore;
using NovaCare.Modals.DoctorModals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.StaffModals
{
    [Index(nameof(EmployeeIdCode), IsUnique = true)]
    public class EmployeeId
    {
        [Key]
        public int Id { get; set; }

        public int EmployeeIdCode { get; set; }

        [ForeignKey(nameof(Staff))]
        public string? StaffId { get; set; }
        public Staff? Staff { get; set; }
    }
}
