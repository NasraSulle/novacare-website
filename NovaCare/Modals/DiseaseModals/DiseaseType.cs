using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovaCare.Modals.DiseaseModals
{
    public class DiseaseType
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string DiseaseTypeCode { get; set; } = string.Empty;
        public string DiseaseTypeName { get; set; } = string.Empty;


        //navigation properties
        public List<Disease>? Diseases { get; set; } 
    }
}
