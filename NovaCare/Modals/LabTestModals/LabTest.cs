using NovaCare.Modals.PatientModals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.LabTestModals
{
    public class LabTest
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string DiseaseName { get; set; } = string.Empty;
        public string LabTestStatusname { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
        public string TestResult { get; set; } = string.Empty;
        public byte[]? ResultAttachment { get; set; }

        public string RequesteeDoctor { get; set; } = string.Empty;
        public string UpdaterLabTechnician { get; set; } = string.Empty;
        public string RequesteeId { get; set; } = string.Empty;
        public string UpdaterId { get; set; } = string.Empty;
        public decimal LabTestCost { get; set; } = 0m;

        //foreign keys
        [ForeignKey(nameof(Visit))]
        public int VisitId { get; set; }

        [ForeignKey(nameof(Consultation))]
        public int? ConsultationId { get; set; }

        //navigation properties
        public Visit? Visit { get; set; }
        public List<LabTestResultComposition>? LabTestResultCompositions { get; set; }

        [ForeignKey(nameof(Bill))]
        public int? BillId { get; set; }
        public Bill? Bill { get; set; }
    }
}
