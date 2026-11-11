using NovaCare.Shared.DTOs.DoctorDTOs;
using NovaCare.Shared.DTOs.MedicineDTOs;
using NovaCare.Shared.DTOs.PatientDTOs;

namespace NovaCare.Shared.DTOs.TreatmentDTOs
{
    public class PrescriptionDto
    {
        public int Id { get; set; }
        public DateTime PrescribedDate { get; set; }
        public double Quantity { get; set; } = 1d;
        public string Dosage { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; } = 0m;
        public decimal TotalPrice => (decimal)Quantity * UnitPrice;

        // Foreign keys
        public int VisitId { get; set; }
        public int? ConsultationId { get; set; }
        public int MedicineId { get; set; }
        public int? DoctorId { get; set; }
        public int? BillItemId { get; set; }

        // Navigation properties
        public VisitDto? Visit { get; set; }
        public ConsultationDto? Consultation { get; set; }
        public MedicineDto? Medicine { get; set; }
        public DoctorDto? Doctor { get; set; }
        public BillItemDto? BillItem { get; set; }

        // UI Helpers
        public string MedicineName { get; set; } = string.Empty;
        public string DoctorFullName { get; set; } = string.Empty;
        public string PaymentStatusName { get; set; } = string.Empty;
    }
}
