using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.StaffDTOs
{
    public class StaffTitleDto
    {
        //properties
        public int Id { get; set; }
        public string StaffTitleName { get; set; } = string.Empty;
        public DateTime RegisteredDate { get; set; }

        //foreign keys
        public int StaffTypeId { get; set; }
        public int TitleGroupId { get; set; }

        //navigation properties
        public StaffTitleGroupDto? StaffTitleGroup { get; set; }
        public StaffTypeDto? StaffType { get; set; }
        public List<StaffDto>? Staffs { get; set; }

        public string StaffTypeName { get; set; } = string.Empty;
        public string TitleGroupName { get; set; } = string.Empty;
        public int StaffsCount { get; set; }
    }
}
