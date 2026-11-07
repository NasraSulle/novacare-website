using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.RoomDTOs
{
    public class RoomStatusDto
    {
        //properties
        public int Id { get; set; }
        public string RoomStatusName { get; set; } = string.Empty;

        //navigation properties
        public List<RoomDto>? Rooms { get; set; }
    }
}
