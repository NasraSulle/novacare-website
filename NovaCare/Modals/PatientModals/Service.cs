using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NovaCare.Modals.DoctorModals;

namespace NovaCare.Modals.PatientModals
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string ServiceName { get; set; } = string.Empty;

        public decimal? Cost { get; set; } = 0m;

        [ForeignKey(nameof(DoctorType))]
        public int? DepartmentId { get; set; }

        [ForeignKey(nameof(Doctor))]
        public int? DoctorId { get; set; }

        public int? MedicineGenreId { get; set; }
        public int? MedicineTypeId { get; set; }

        public bool HasDept { get; set; }
        public bool HasDoctor { get; set; }
        public bool HasName { get; set; }
        public bool HasGenre { get; set; }
        public bool HasType { get; set; }

        public Doctor? Doctor { get; set; }
        public DoctorType? DoctorType { get; set; }
        public List<BillService>? BillItems { get; set; }
    }
}
