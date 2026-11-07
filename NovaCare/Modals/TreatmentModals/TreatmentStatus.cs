using NovaCare.Modals.PatientModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovaCare.Modals.TreatmentModals
{
    public class TreatmentStatus
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string TreatmentStatusName { get; set; } = string.Empty;

        //navigation properties
        public List<Treatment>? Treatments { get; set; }
    }
}
