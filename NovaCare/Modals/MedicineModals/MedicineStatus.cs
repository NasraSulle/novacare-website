using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovaCare.Modals.MedicineModals
{
    public class MedicineStatus
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string MedicineStatusName { get; set; } = string.Empty;

        //navigation properties
        public List<Medicine>? Medicines { get; set; }
    }
}
