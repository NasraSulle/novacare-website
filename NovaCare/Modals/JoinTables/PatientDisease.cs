using NovaCare.Modals.DiseaseModals;
using NovaCare.Modals.PatientModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NovaCare.Modals.JoinTables
{
    public class PatientDisease
    {
        //properties
        [Key]
        public int PatientDiseaseId { get; set; }

        //foreign keys
        [ForeignKey(nameof(Disease))]
        public int DiseaseId { get; set; }
        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }

        //navigation properties
        public Disease? Disease { get; set; }
        public Patient? Patient { get; set; }

    }
}
