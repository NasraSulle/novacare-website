using NovaCare.Modals.PatientModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovaCare.Modals.RoomModals
{
    public class RoomStatus
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string RoomStatusName { get; set; } = string.Empty;

        //navigation properties
        public List<Room>? Rooms { get; set; }
    }
}
