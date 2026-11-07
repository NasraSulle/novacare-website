using NovaCare.Modals.DiseaseModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovaCare.Modals.PatientModals
{
    public class PatientType
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string PatientTypeCode { get; set; } = string.Empty;
        public string PatientTypeName { get; set; } = string.Empty;

        //navigation properties
        public List<Patient>? Patients { get; set; }

    }
}
