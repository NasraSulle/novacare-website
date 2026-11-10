using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.RoomModals;
using NovaCare.Shared.DTOs.DoctorDTOs;

namespace NovaCare.Shared.DTOs.RoomDTOs
{
    public class WardDto
    {
        public int Id { get; set; }

        public string WardName { get; set; } = string.Empty;

        public decimal WardCost { get; set; } = 0m;

        public int? MedicalDepartmentId { get; set; }
        public MedicalDepartmentDto? MedicalDepartment { get; set; }
        public List<BedDto>? Beds { get; set; }
        public string MedicalDepartmentName { get; set; } = string.Empty;
    }
}
