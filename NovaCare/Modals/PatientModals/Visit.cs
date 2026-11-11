using NovaCare.Modals.LabTestModals;
using NovaCare.Modals.TreatmentModals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.PatientModals
{
    public class Visit
    {
        [Key]
        public int Id { get; set; }
        public DateTime VisitDate { get; set; } = DateTime.UtcNow;
        public DateTime? DischargedDate { get; set; } // Nullable until the patient is discharged

        // Foreign Keys
        [ForeignKey(nameof(VisitType))]
        public int VisitTypeId { get; set; }

        [ForeignKey(nameof(VisitStatus))]
        public int VisitStatusId { get; set; }

        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }

        // Single Navigation Properties
        public Patient? Patient { get; set; }
        public VisitStatus? VisitStatus { get; set; }
        public VisitType? VisitType { get; set; }
        public PreliminaryTest? PreliminaryTest { get; set; }

        // Collection Navigation Properties (1-to-Many)
        public List<Bill> Bills { get; set; } = new();
        public List<Consultation> Consultations { get; set; } = new();
        public List<LabTest> LabTests { get; set; } = new();
        public List<Prescription> Prescriptions { get; set; } = new(); // Added for patient medications
        public List<Treatment> Treatments { get; set; } = new(); // Surgery, Therapy, Hospitalized
    }
}
