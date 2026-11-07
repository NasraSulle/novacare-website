using NovaCare.Shared.DTOs.JoinDto;
using NovaCare.Shared.DTOs.JoinDTOs;
using NovaCare.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class PatientDto
    {
        //properties
        public int Id { get; set; }
        public string FisrtName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string PatientCode { get; set; } = string.Empty;

        public byte[]? PatientPhoto { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string HomeAddress { get; set; } = string.Empty;
        public DateTime RegisteredDate { get; set; }
        public Gender Gender { get; set; }

        //===========first of Kin===============
        public string KinFullName { get; set; } = string.Empty;
        public string KinPhoneNumber { get; set; } = string.Empty;
        public string KinHomeAddress { get; set; } = string.Empty;

        //foreign keys
        public int PatientStatusId { get; set; }
        public int PatientTypeId { get; set; }

        //navigation properties
        public PatientStatusDto? PatientStatus { get; set; }
        public PatientTypeDto? PatientType { get; set; }
        public List<PatientDiseaseDto>? PatientDiseases { get; set; }
        public List<DoctorPatientDto>? DoctorPatients { get; set; }
        public List<PatientInsuaranceDto>? PatientInsuarances { get; set; }
        public List<PatientPaymentModeDto>? PatientPaymentModes { get; set; }
        public List<VisitDto>? Visits { get; set; }

        //for display
        public string PatientStatusName { get; set; } = string.Empty;
        public string PatientTypeName { get; set; } = string.Empty;
    }
}
