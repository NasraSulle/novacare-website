using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NovaCare.Modals.StaffModals
{
    public class StaffTitle
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string StaffTitleName { get; set; } = string.Empty;
        public DateTime RegisteredDate { get; set; }

        //foreign keys
        [ForeignKey(nameof(StaffType))]
        public int StaffTypeId { get; set; }

        [ForeignKey(nameof(StaffTitleGroup))]
        public int TitleGroupId { get; set; }

        //navigation properties
        public StaffTitleGroup? StaffTitleGroup { get; set; }
        public StaffType? StaffType { get; set; }
        public List<Staff>? Staffs { get; set; }
    }
}
