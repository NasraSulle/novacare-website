using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.TreatmentDTOs
{
    public class TreatmentTypeDto
    {
        //properties
        public int Id { get; set; }
        public string TreatmentTypeCode { get; set; } = string.Empty;
        public string TreatmentTypeName { get; set; } = string.Empty;

        public bool HasMedicineId { get; set; }
        public bool HasSurgeryId { get; set; }
        public bool HasTherapyId { get; set; }
        public bool HasMedicalDeptId { get; set; }
        public bool HasWardId { get; set; }
        public bool HasDoctorId { get; set; }
        public bool HasRoomId { get; set; }

        //navigation properties
        public List<TreatmentDto>? Treatments { get; set; }

    }
}
