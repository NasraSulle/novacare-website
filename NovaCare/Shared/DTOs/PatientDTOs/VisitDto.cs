using NovaCare.Shared.DTOs.LabTestDTOs;
using NovaCare.Shared.DTOs.MedicineDTOs;
using NovaCare.Shared.DTOs.TreatmentDTOs;

namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class VisitDto
    {
        public int Id { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime? DischargedDate { get; set; } // Nullable until discharged

        public int VisitTypeId { get; set; }
        public int PatientId { get; set; }
        public int VisitStatusId { get; set; }

        // Core navigation contracts using pure DTOs
        public PatientDto? Patient { get; set; }
        public VisitTypeDto? VisitType { get; set; }
        public PreliminaryTestDto? PreliminaryTest { get; set; }
        public VisitStatusDto? VisitStatus { get; set; }

        // Normalized operational collections
        public List<BillDto> Bills { get; set; } = new();
        public List<ConsultationDto> Consultations { get; set; } = new();
        public List<LabTestDto> LabTests { get; set; } = new();
        public List<PrescriptionDto> Prescriptions { get; set; } = new();
        public List<TreatmentDto> Treatments { get; set; } = new(); 

        // UI Helpers
        public string VisitTypeName { get; set; } = string.Empty;
        public string PatientFullName { get; set; } = string.Empty;
        public int? PreliminaryTestId { get; set; }
        public string VisitStatusName { get; set; } = string.Empty;
        public string RegistedPaymentMode { get; set; } = "cash";

        // Live calculation of unbilled items for this visit
        public decimal TotalUnbilledCharges =>
            Consultations.Where(c => c.BillItemId == null).Sum(c => c.ConsultationCost) +
            LabTests.Where(l => l.BillItemId == null).Sum(l => l.LabTestCost) +
            Prescriptions.Where(p => p.BillItemId == null).Sum(p => (decimal)p.Quantity * p.UnitPrice) +
            Treatments.Where(t => t.BillItemId == null).Sum(t => t.TreatmentQty * t.TreatmentCost);
    }
}
