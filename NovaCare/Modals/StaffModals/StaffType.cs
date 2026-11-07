using NovaCare.Modals.StaffModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovaCare.Modals.StaffModals
{
    public class StaffType
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string StaffTypeCode { get; set; } = string.Empty;
        public string StaffTypeName { get; set; } = string.Empty;

        public DateTime RegisteredDate { get; set; }

        //navigation properties
        public List<StaffTitle>? StaffTitles { get; set; }

    }
}
