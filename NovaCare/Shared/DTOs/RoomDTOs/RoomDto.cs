using NovaCare.Shared.DTOs.DoctorDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.RoomDTOs
{
    public class RoomDto
    {
        //properties
        public int Id { get; set; }
        public string RoomName { get; set; } = string.Empty;

        //foreign keys
        public int RoomStatusId { get; set; }
        public int RoomTypeId { get; set; }

        //navigation properties
        public DoctorDto? Doctor { get; set; }
        public RoomStatusDto? RoomStatus { get; set; }
        public RoomTypeDto? RoomType { get; set; }

        public string RoomTypeName { get; set; } = string.Empty;
        public string RoomStatusName { get; set; } = string.Empty;
    }
}
