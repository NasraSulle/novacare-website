using NovaCare.Modals.DiseaseModals;
using NovaCare.Modals.JoinTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NovaCare.Modals.TreatmentModals
{
    public class Treatment
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string TreatmentCode { get; set; } = string.Empty;
        public string TreatmentName { get; set; } = string.Empty;


        //foreign keys
        [ForeignKey(nameof(TreatmentStatus))]
        public int TreatmentStatusId { get; set; }
        [ForeignKey(nameof(TreatmentType))]
        public int TreatmentTypeId { get; set; }

        //navigation properties
        public TreatmentStatus? TreatmentStatus { get; set; }
        public TreatmentType? TreatmentType { get; set; }
        public List<DiseaseTreatment>? DiseaseTreatments { get; set; }
        public List<MedicineTreatment>? MedicineTreatments { get; set; }

    }
}
