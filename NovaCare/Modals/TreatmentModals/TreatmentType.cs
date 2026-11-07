using NovaCare.Modals.DiseaseModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovaCare.Modals.TreatmentModals
{
    public class TreatmentType
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string TreatmentTypeCode { get; set; } = string.Empty;
        public string TreatmentTypeName { get; set; } = string.Empty;


        //navigation properties
        public List<Treatment>? Treatments { get; set; }

    }
}
