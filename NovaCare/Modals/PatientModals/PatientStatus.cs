using NovaCare.Modals.JoinTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovaCare.Modals.PatientModals
{
    public class PatientStatus
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string PatientStatusName { get; set; } = string.Empty;

        //navigation properties
        public List<Patient>? Patients { get; set; }

    }
}
