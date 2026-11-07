using NovaCare.Modals.MedicineModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NovaCare.Modals.JoinTables
{
    public class MedicineMedicineType
    {
        //properties
        [Key]
        public int MedicineMedicineTypeId { get; set; }

        //foreign keys
        [ForeignKey(nameof(MedicineType))]
        public int MedicineTypeId { get; set; }
        [ForeignKey(nameof(Medicine))]
        public int MedicineId { get; set; }

        //navigation properties
        public Medicine? Medicine { get; set; }
        public MedicineType? MedicineType { get; set; }

    }
}
