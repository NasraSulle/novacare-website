using NovaCare.Shared.DTOs.PatientDTOs;

namespace NovaCare.Shared.DTOs.LabTestDTOs
{
    public class LabTestDto
    {
        public int Id { get; set; }
        public string DiseaseName { get; set; } = string.Empty;
        public string LabTestStatusName { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
        public string TestResult { get; set; } = string.Empty;
        public byte[]? ResultAttachment { get; set; }

        public string RequesteeDoctor { get; set; } = string.Empty;
        public string UpdaterLabTechnician { get; set; } = string.Empty;
        public string RequesteeId { get; set; } = string.Empty;
        public string UpdaterId { get; set; } = string.Empty;
        public decimal LabTestCost { get; set; } = 0m;

        // foreign keys
        public int VisitId { get; set; }
        public int PatientId { get; set; }
        public int? ConsultationId { get; set; }

        // navigation properties
        public VisitDto? Visit { get; set; }
        public List<LabTestResultCompositionDto>? LabTestResultCompositions { get; set; }

        // for display
        public string PatientFullName { get; set; } = string.Empty;
        public string DiseaseTypeName { get; set; } = string.Empty;
    }
}
