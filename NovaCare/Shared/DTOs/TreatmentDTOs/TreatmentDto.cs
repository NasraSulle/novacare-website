using NovaCare.Shared.DTOs.DoctorDTOs;
using NovaCare.Shared.DTOs.JoinDto;
using NovaCare.Shared.DTOs.PatientDTOs;
using NovaCare.Shared.DTOs.RoomDTOs;

namespace NovaCare.Shared.DTOs.TreatmentDTOs
{
    public class TreatmentDto
    {
        // Properties
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; } // Nullable until treatment finishes
        public int TreatmentQty { get; set; } = 1;
        public decimal TreatmentCost { get; set; } = 0m;
        public decimal TotalCost => TreatmentQty * TreatmentCost;

        // Required Foreign Keys (Visit, Consultation & Billing)
        public int VisitId { get; set; }
        public int? ConsultationId { get; set; } // Null = walk-in treatment; HasValue = ordered during consultation
        public int? BillItemId { get; set; } // Null = unbilled; HasValue = linked to a BillItem

        // Clinical & Location Foreign Keys
        public int? SurgeryId { get; set; }
        public int? TherapyId { get; set; }
        public int? MedicalDeptId { get; set; }
        public int? WardId { get; set; }
        public int? DoctorId { get; set; }
        public int? RoomId { get; set; }
        public int TreatmentStatusId { get; set; }
        public int TreatmentTypeId { get; set; }

        // Navigation Properties
        public VisitDto? Visit { get; set; }
        public ConsultationDto? Consultation { get; set; }
        public BillItemDto? BillItem { get; set; }
        public TreatmentStatusDto? TreatmentStatus { get; set; }
        public TreatmentTypeDto? TreatmentType { get; set; }
        public SurgeryDto? Surgery { get; set; }
        public TherapyDto? Therapy { get; set; }
        public MedicalDepartmentDto? MedicalDepartment { get; set; }
        public WardDto? Ward { get; set; }
        public DoctorDto? Doctor { get; set; }
        public RoomDto? Room { get; set; }

        // UI Helpers
        public string TreatmentStatusName { get; set; } = string.Empty;
        public string TreatmentTypeName { get; set; } = string.Empty;
        public string SurgeryName { get; set; } = string.Empty;
        public string TherapyName { get; set; } = string.Empty;
        public string MedicalDeptName { get; set; } = string.Empty;
        public string WardName { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string RoomName { get; set; } = string.Empty;
        public string PaymentStatusName { get; set; } = string.Empty;
    }
}
