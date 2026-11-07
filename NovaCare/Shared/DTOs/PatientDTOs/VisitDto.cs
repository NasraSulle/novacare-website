using NovaCare.Shared.DTOs.LabTestDTOs;
using NovaCare.Shared.DTOs.MedicineDTOs;
using NovaCare.Shared.DTOs.TreatmentDTOs;
using System;
using System.Collections.Generic;

namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class VisitDto
    {
        public int Id { get; set; }
        public DateTime VisitDate { get; set; }
        public int VisitTypeId { get; set; }
        public int PatientId { get; set; }

        // Core navigation contracts using pure DTOs
        public PatientDto? Patient { get; set; }
        public VisitTypeDto? VisitType { get; set; }
        public PreliminaryTestDto? PreliminaryTest { get; set; }

        // Fully normalized operational 
        public List<BillDto>? Bills { get; set; }
        public List<ConsultationDto>? Consultations { get; set; }
        public List<LabTestDto>? LabTests { get; set; }
        public List<MedicineDto>? MedicinePrescription { get; set; }
        public List<TreatmentDto>? Treatments { get; set; }
    }
}
