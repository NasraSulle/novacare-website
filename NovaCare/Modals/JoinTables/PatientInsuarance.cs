using NovaCare.Modals.PatientModals;
using NovaCare.Modals.PaymentModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NovaCare.Modals.JoinTables
{
    public class PatientInsuarance
    {
        //properties
        [Key]
        public int PatientInsuaranceId { get; set; }

        //foreign keys
        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }
        [ForeignKey(nameof(Insuarance))]
        public int InsuaranceId { get; set; }

        //navigation properties
        public Patient? Patient { get; set; }
        public Insuarance? Insuarance { get; set; }


    }
}
