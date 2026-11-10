using NovaCare.Shared.DTOs.LabTestDTOs;
using NovaCare.Shared.DTOs.MedicineDTOs;
using NovaCare.Shared.DTOs.TreatmentDTOs;

namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class VisitDto
    {
        public int Id { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime DischargedDate { get; set; }

        public int VisitTypeId { get; set; }
        public int PatientId { get; set; }
        public int VisitStatusId { get; set; }

        // Core navigation contracts using pure DTOs
        public PatientDto? Patient { get; set; }
        public VisitTypeDto? VisitType { get; set; }
        public PreliminaryTestDto? PreliminaryTest { get; set; }
        public VisitStatusDto? VisitStatus { get; set; }

        // Fully normalized operational 
        public List<BillDto>? Bills { get; set; }
        public List<ConsultationDto>? Consultations { get; set; }
        public List<LabTestDto>? LabTests { get; set; }
        public List<MedicineDto>? MedicinePrescription { get; set; }
        public List<TreatmentDto>? Treatments { get; set; }

        //for UI
        public string VisitTypeName { get; set; } = string.Empty;
        public string PatientFullName { get; set; } = string.Empty;
        public int PreliminaryTestId { get; set; }
        public string VisitStatusName { get; set; } = string.Empty;

        public decimal TotalUnbilledCharges =>
            (Consultations?.Where(c => c.BillId == null).Sum(c => c.ConsultationCost) ?? 0) +
            (LabTests?.Where(l => l.BillId == null).Sum(l => l.LabTestCost) ?? 0) +
            (MedicinePrescription?.Where(m => m.BillId == null).Sum(m => m.BuyingPrice) ?? 0) +
            (Treatments?.Where(t => t.BillId == null).Sum(t => t.TreatmentCost) ?? 0);

    }
}
