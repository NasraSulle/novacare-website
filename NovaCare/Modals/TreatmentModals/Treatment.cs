using NovaCare.Modals.DiseaseModals;
using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.JoinTables;
using NovaCare.Modals.MedicineModals;
using NovaCare.Modals.RoomModals; // Assuming Ward and Room live here based on previous steps
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NovaCare.Modals.TreatmentModals
{
    public class Treatment
    {
        //properties
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public double TreatmentQty { get; set; } = 0d;
        public decimal TreatmentCost { get; set; } = 0m;

        public int? MedicineId { get; set; }
        public int? SurgeryId { get; set; }
        public int? TherapyId { get; set; }
        public int? MedicalDeptId { get; set; }
        public int? WardId { get; set; }
        public int? DoctorId { get; set; }
        public int? RoomId { get; set; }

        // foreign keys
        [ForeignKey(nameof(TreatmentStatus))]
        public int TreatmentStatusId { get; set; }
        [ForeignKey(nameof(TreatmentType))]
        public int TreatmentTypeId { get; set; }

        //  navigation properties
        public TreatmentStatus? TreatmentStatus { get; set; }
        public TreatmentType? TreatmentType { get; set; }

        [ForeignKey(nameof(MedicineId))]
        public Medicine? Medicine { get; set; }

        [ForeignKey(nameof(SurgeryId))]
        public Surgery? Surgery { get; set; }

        [ForeignKey(nameof(TherapyId))]
        public Therapy? Therapy { get; set; }

        [ForeignKey(nameof(MedicalDeptId))]
        public MedicalDepartment? MedicalDepartment { get; set; }

        [ForeignKey(nameof(WardId))]
        public Ward? Ward { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public Doctor? Doctor { get; set; }

        [ForeignKey(nameof(RoomId))]
        public Room? Room { get; set; }
    }
}
