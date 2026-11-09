using NovaCare.Modals.DiseaseModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovaCare.Modals.TreatmentModals
{
    public class TreatmentType
    {
        //properties
        [Key]
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
        public List<Treatment>? Treatments { get; set; }

    }
}
