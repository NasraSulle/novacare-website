using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.StaffDTOs
{
    public class StaffTypeDto
    {
        //properties
        public int Id { get; set; }
        public string StaffTypeCode { get; set; } = string.Empty;
        public string StaffTypeName { get; set; } = string.Empty;
        public DateTime RegisteredDate { get; set; }


        //navigation properties
        public List<StaffTitleDto>? StaffTitles { get; set; }
        public int StaffsTitleCount { get; set; }
    }
}
