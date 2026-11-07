using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.StaffDTOs
{
    public class StaffStatusDto
    {
        //properties
        public int Id { get; set; }
        public string StaffStatusName { get; set; } = string.Empty;

        //navigation properties
        public List<StaffDto>? Staffs { get; set; }
    }
}
