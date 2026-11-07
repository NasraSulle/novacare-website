using NovaCare.Modals.PatientModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovaCare.Modals.StaffModals
{
    public class StaffStatus
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string StaffStatusName { get; set; } = string.Empty;

        //navigation properties
        public List<Staff>? Staffs { get; set; }
    }
}
