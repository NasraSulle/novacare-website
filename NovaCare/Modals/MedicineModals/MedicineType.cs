using NovaCare.Modals.JoinTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NovaCare.Modals.MedicineModals
{
    public class MedicineType
    {
        //properties
        [Key]
        public int Id { get; set; }
        public int MedicineTypeCode { get; set; }
        public string MedicineTypeName { get; set; } = string.Empty;

        [ForeignKey(nameof(MedicineGenre))]
        public int? MedicineGenreId { get; set; }

        //navigation properties
        public MedicineGenre? MedicineGenre { get; set; }
        public List<Medicine>? Medicines { get; set; }
    }
}
