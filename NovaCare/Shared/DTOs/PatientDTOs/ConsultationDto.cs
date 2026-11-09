using NovaCare.Shared.DTOs.DoctorDTOs;

namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class ConsultationDto
    {
        public int Id { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public decimal ConsultationCost { get; set; } = 0m;

        // Foreign
        public int RoomId { get; set; }
        public int VisitId { get; set; }
        public int? DoctorId { get; set; }
        public int? MedicalDepartmentId { get; set; }

        // Navigation properties
        public DoctorDto? Doctor { get; set; }
        public VisitDto? Visit { get; set; }
        public MedicalDepartmentDto? MedicalDepartment { get; set; }

        public string MedicalDepartmentName { get; set; } = string.Empty;
        public string DoctorFullName { get; set; } = string.Empty;
        public string VisitTypeName { get; set; } = string.Empty;
        public string RoomName { get; set; } = string.Empty;

        public int? BillId { get; set; }
        public BillDto? Bill { get; set; }
    }
}
