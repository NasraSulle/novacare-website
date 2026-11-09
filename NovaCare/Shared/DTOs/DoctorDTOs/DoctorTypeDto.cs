using NovaCare.Modals.DoctorModals;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace NovaCare.Shared.DTOs.DoctorDTOs
{
    public class DoctorTypeDto
    {
        //properties
        public int Id { get; set; }
        public string DoctorTypeCode { get; set; } = string.Empty;
        public string DoctorTypeName { get; set; } = string.Empty;
        public decimal DeptConsultationFee { get; set; } = 0m;

        public int MedicalDeptId { get; set; }

        //navigation properties
        public MedicalDepartmentDto? MedicalDepartment { get; set; }
        public List<DoctorDto>? Doctors { get; set; }
    }
}
