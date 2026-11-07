using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.RoomDTOs
{
    public class RoomTypeDto
    {
        //properties
        public int Id { get; set; }
        public string RoomTypeCode { get; set; } = string.Empty;
        public string RoomTypeName { get; set; } = string.Empty;


        //navigation properties
        public List<RoomDto>? Rooms { get; set; }
    }
}
