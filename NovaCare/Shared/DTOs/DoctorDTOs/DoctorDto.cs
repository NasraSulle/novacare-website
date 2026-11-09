using NovaCare.Shared.DTOs.JoinDTOs;
using NovaCare.Shared.DTOs.RoomDTOs;
using NovaCare.Shared.DTOs.StaffDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.DoctorDTOs
{
    public class DoctorDto
    {
        //properties
        public int Id { get; set; }
        public string DoctorCode { get; set; } = string.Empty;
        public string MedicalLicenseNumber { get; set; } = string.Empty;
        public decimal DoctorConsultationFee { get; set; } = 0m;

        //foreign keys
        public int DoctorTypeId { get; set; }
        public int DoctorStatusId { get; set; }
        public int? RoomId { get; set; }
        public string StaffId { get; set; } = string.Empty;
        public DateTime? LicenceExpDate { get; set; }

        //navigation properties
        public StaffDto? Staff { get; set; }
        public RoomDto? Room { get; set; }
        public DoctorStatusDto? DoctorStatus { get; set; }
        public DoctorTypeDto? DoctorType { get; set; }
        public List<DoctorPatientDto>? DoctorPatients { get; set; }
        public string DoctorFullName { get; set; } = string.Empty;

    }
}
