using Microsoft.AspNetCore.Identity;
using NovaCare.Modals.DiseaseModals;
using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.JoinTables;
using NovaCare.Modals.TreatmentModals;
using NovaCare.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NovaCare.Modals.PatientModals
{
    public class Patient
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string PatientFullName => string.Join(" ",
            new[] { FirstName, MiddleName, LastName }
            .Where(s => !string.IsNullOrWhiteSpace(s)));


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
        [ForeignKey(nameof(PatientStatus))]
        public int PatientStatusId { get; set; }
        [ForeignKey(nameof(PatientType))]
        public int PatientTypeId { get; set; }

        //navigation properties
        public PatientStatus? PatientStatus { get; set; }
        public PatientType? PatientType { get; set; }
        public List<PatientDisease>? PatientDiseases { get; set; }
        public List<DoctorPatient>? DoctorPatients { get; set; }
        public List<PatientInsuarance>? PatientInsuarances { get; set; }
        public List<PatientPaymentMode>? PatientPaymentModes { get; set; }
        public List<Visit>? Visits { get; set; }

    }
}
