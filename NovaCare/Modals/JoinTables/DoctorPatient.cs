using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.PatientModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NovaCare.Modals.JoinTables
{
    public class DoctorPatient
    {
        //properties
        [Key]
        public int DoctorPatientId { get; set; }

        //foreign keys
        [ForeignKey(nameof(Doctor))]
        public int DoctorId { get; set; }
        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }

        //navigation properties
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }

    }
}
