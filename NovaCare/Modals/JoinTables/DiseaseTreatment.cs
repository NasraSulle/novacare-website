using NovaCare.Modals.DiseaseModals;
using NovaCare.Modals.TreatmentModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NovaCare.Modals.JoinTables
{
    public class DiseaseTreatment
    {
        //properties
        [Key]
        public int DiseaseTreatmentId { get; set; }

        //foreign keys
        [ForeignKey(nameof(Disease))]
        public int DiseaseId { get; set; }
        [ForeignKey(nameof(Treatment))]
        public int TreatmentId { get; set; }

        //navigation properties
        public Disease? Disease { get; set; }
        public Treatment? Treatment { get; set; }
    }
}
