using NovaCare.Modals.PatientModals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.LabTestModals
{
    public class LabTest
    {
        // Properties
        [Key]
        public int Id { get; set; }
        public string? LabTestCode { get; set; } // Optional standard/LOINC/NHIF lab code for tariff lookup
        public string DiseaseName { get; set; } = string.Empty;
        public string LabTestStatusName { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string TestResult { get; set; } = string.Empty;
        public byte[]? ResultAttachment { get; set; }

        public string RequesteeDoctor { get; set; } = string.Empty;
        public string UpdaterLabTechnician { get; set; } = string.Empty;
        public string RequesteeId { get; set; } = string.Empty;
        public string UpdaterId { get; set; } = string.Empty;
        public decimal LabTestCost { get; set; } = 0m;

        // Foreign Keys
        [ForeignKey(nameof(Visit))]
        public int VisitId { get; set; }

        [ForeignKey(nameof(Consultation))]
        public int? ConsultationId { get; set; }

        [ForeignKey(nameof(BillItem))]
        public int? BillItemId { get; set; } // Null = unbilled; HasValue = linked to a BillItem

        // Navigation Properties
        public Visit? Visit { get; set; }
        public Consultation? Consultation { get; set; }
        public BillItem? BillItem { get; set; }
        public List<LabTestResultComposition> LabTestResultCompositions { get; set; } = new();
    }
}
