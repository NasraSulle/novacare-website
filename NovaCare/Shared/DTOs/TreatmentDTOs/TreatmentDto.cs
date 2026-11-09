using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.MedicineModals;
using NovaCare.Modals.RoomModals;
using NovaCare.Modals.TreatmentModals;
using NovaCare.Shared.DTOs.JoinDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NovaCare.Shared.DTOs.TreatmentDTOs
{
    public class TreatmentDto
    {
        //properties
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


        //foreign keys
        public int TreatmentStatusId { get; set; }
        public int TreatmentTypeId { get; set; }

        //navigation properties
        public TreatmentStatusDto? TreatmentStatus { get; set; }
        public TreatmentTypeDto? TreatmentType { get; set; }

        public Medicine? Medicine { get; set; }
        public Surgery? Surgery { get; set; }
        public Therapy? Therapy { get; set; }
        public MedicalDepartment? MedicalDepartment { get; set; }
        public Ward? Ward { get; set; }
        public Doctor? Doctor { get; set; }
        public Room? Room { get; set; }

        //for UI
        public string TreatmentStatusName { get; set; } = string.Empty;
        public string TreatmentTypeName { get; set; } = string.Empty;
    }
}
