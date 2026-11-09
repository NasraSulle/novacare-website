using NovaCare.Modals.LabTestModals;
using NovaCare.Modals.MedicineModals;
using NovaCare.Modals.TreatmentModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.PatientModals
{
    public class Visit
    {
        [Key]
        public int Id { get; set; }
        public DateTime VisitDate { get; set; }


        [ForeignKey(nameof(VisitType))]
        public int VisitTypeId { get; set; }

        [ForeignKey(nameof(VisitStatus))]
        public int VisitStatusId { get; set; }

        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }

        public Patient? Patient { get; set; }
        public VisitStatus? VisitStatus { get; set; }
        public VisitType? VisitType { get; set; }
        public PreliminaryTest? PreliminaryTest { get; set; }

        public List<Bill>? Bills { get; set; }
        public List<Consultation>? Consultations { get; set; }
        public List<LabTest>? LabTests { get; set; }
        public List<Medicine>? MedicinePrescription { get; set; }
        public List<Treatment>? Treatments { get; set; }

    }
}
