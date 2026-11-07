using NovaCare.Modals.DiseaseModals;
using NovaCare.Modals.MedicineModals;
using NovaCare.Modals.TreatmentModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NovaCare.Modals.JoinTables
{
    public class MedicineTreatment
    {
        //properties
        [Key]
        public int MedicineTreatmentId { get; set; }

        //foreign keys
        [ForeignKey(nameof(Medicine))]
        public int MedicineId { get; set; }
        [ForeignKey(nameof(Treatment))]
        public int TreatmentId { get; set; }

        //navigation properties
        public Medicine? Medicine { get; set; }
        public Treatment? Treatment { get; set; }
    }
}
