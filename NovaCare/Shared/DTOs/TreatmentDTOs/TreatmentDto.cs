using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.MedicineModals;
using NovaCare.Modals.RoomModals;
using NovaCare.Modals.TreatmentModals;
using NovaCare.Shared.DTOs.DoctorDTOs;
using NovaCare.Shared.DTOs.JoinDto;
using NovaCare.Shared.DTOs.MedicineDTOs;
using NovaCare.Shared.DTOs.PatientDTOs;
using NovaCare.Shared.DTOs.RoomDTOs;
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
        public DateTime? StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int TreatmentQty { get; set; } = 1;
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

        public MedicineDto? Medicine { get; set; }
        public SurgeryDto? Surgery { get; set; }
        public TherapyDto? Therapy { get; set; }
        public MedicalDepartmentDto? MedicalDepartment { get; set; }
        public WardDto? Ward { get; set; }
        public DoctorDto? Doctor { get; set; }
        public RoomDto? Room { get; set; }

        //for UI
        public string TreatmentStatusName { get; set; } = string.Empty;
        public string TreatmentTypeName { get; set; } = string.Empty;

        public string MedicineName { get; set; } = string.Empty;
        public string SurgeryName { get; set; } = string.Empty;
        public string TherapyName { get; set; } = string.Empty;
        public string MedicalDeptName { get; set; } = string.Empty;
        public string WardName { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string RoomName { get; set; } = string.Empty;

        public int? BillId { get; set; }
        public BillDto? Bill { get; set; }
    }
}
