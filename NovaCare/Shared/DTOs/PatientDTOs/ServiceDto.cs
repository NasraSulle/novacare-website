using System.Collections.Generic;
using NovaCare.Shared.DTOs.DoctorDTOs;

namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public decimal? Cost { get; set; } = 0m;

        // Foreign keys
        public int? DepartmentId { get; set; }
        public int? DoctorId { get; set; }
        public int? MedicineGenreId { get; set; }
        public int? MedicineTypeId { get; set; }

        // Strategy Flags
        public bool HasDept { get; set; }
        public bool HasDoctor { get; set; }
        public bool HasName { get; set; }
        public bool HasGenre { get; set; }
        public bool HasType { get; set; }

        // Navigation properties
        public DoctorDto? Doctor { get; set; }
        public DoctorTypeDto? DoctorType { get; set; }
        public List<BillServiceDto>? BillItems { get; set; }

        public string DepartmentName { get; set; } = string.Empty;
    }
}
