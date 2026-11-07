using NovaCare.Modals.JoinTables;
using NovaCare.Modals.PatientModals;
using NovaCare.Modals.TreatmentModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NovaCare.Modals.DiseaseModals
{
    public class Disease
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string DiseaseCode { get; set; } = string.Empty;
        public string DiseaseName { get; set; } = string.Empty;
        public decimal LabTestCost { get; set; } = 0m;
        public DateTime RegisteredDate { get; set; }

        //foreign keys
        [ForeignKey(nameof(DiseaseType))]
        public int DiseaseTypeId { get; set; }

        //navigation properties
        public DiseaseType? DiseaseType { get; set; }
        public List<DiseaseTreatment>? DiseaseTreatments { get; set; }
        public List<PatientDisease>? PatientDiseases { get; set; }
    }
}
