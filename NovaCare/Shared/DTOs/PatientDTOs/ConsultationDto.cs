using NovaCare.Shared.DTOs.DoctorDTOs;
using NovaCare.Shared.DTOs.LabTestDTOs;
using NovaCare.Shared.DTOs.RoomDTOs;
using NovaCare.Shared.DTOs.TreatmentDTOs;

namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class ConsultationDto
    {
        public int Id { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; } // Nullable until consultation is finished
        public decimal ConsultationCost { get; set; } = 0m;

        // Foreign keys
        public int RoomId { get; set; }
        public int VisitId { get; set; }
        public int? DoctorId { get; set; }
        public int? MedicalDepartmentId { get; set; }
        public int? BillItemId { get; set; } // Null = unbilled; HasValue = linked to a BillItem

        // Navigation properties
        public DoctorDto? Doctor { get; set; }
        public RoomDto? Room { get; set; }
        public VisitDto? Visit { get; set; }
        public MedicalDepartmentDto? MedicalDepartment { get; set; }
        public BillItemDto? BillItem { get; set; }

        // Orders placed during this specific consultation
        public List<LabTestDto> LabTests { get; set; } = new();
        public List<TreatmentDto> Treatments { get; set; } = new();

        // UI Helpers
        public string MedicalDepartmentName { get; set; } = string.Empty;
        public string DoctorFullName { get; set; } = string.Empty;
        public string VisitTypeName { get; set; } = string.Empty;
        public string RoomName { get; set; } = string.Empty;
        public string PaymentStatusName { get; set; } = string.Empty; // Populated from BillItem?.Bill?.PaymentStatusName ?? "Not Billed"
    }
}
