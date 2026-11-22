using NovaCare.Modals.InsuranceModals;
using NovaCare.Modals.JoinTables;
using NovaCare.Modals.LabTestModals;
using NovaCare.Modals.PaymentModals;
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
        public string FullName { get; set; } = string.Empty;

        // Foreign Keys
        [ForeignKey(nameof(VisitType))]
        public int VisitTypeId { get; set; }

        [ForeignKey(nameof(VisitStatus))]
        public int VisitStatusId { get; set; }

        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }
        [ForeignKey(nameof(PatientStatus))]
        public int PatientStatusId { get; set; }

        [ForeignKey(nameof(PatientType))]
        public int PatientTypeId { get; set; }


        // Intended payment mode at check-in (1 = Cash, 2 = Insurance)
        [ForeignKey(nameof(PaymentMode))]
        public int PaymentModeId { get; set; }

        // Optional: Active insurance policy selected if PaymentMode is Insurance
        [ForeignKey(nameof(PatientInsuarance))]
        public int? PatientInsuaranceId { get; set; }

        // Single Navigation Properties
        public Patient? Patient { get; set; }
        public PatientStatus? PatientStatus { get; set; }
        public PatientType? PatientType { get; set; }
        public VisitStatus? VisitStatus { get; set; }
        public VisitType? VisitType { get; set; }
        public PaymentMode? PaymentMode { get; set; }
        public PatientInsuarance? PatientInsuarance { get; set; }
        public VisitInsuranceAuth? VisitInsuranceAuth { get; set; }
        public PreliminaryTest? PreliminaryTest { get; set; }

        // Collection Navigation Properties (1-to-Many)
        public List<Bill> Bills { get; set; } = new();
        public List<Consultation> Consultations { get; set; } = new();
        public List<LabTest> LabTests { get; set; } = new();
        public List<Prescription> Prescriptions { get; set; } = new();
        public List<Treatment> Treatments { get; set; } = new();
    }
}
