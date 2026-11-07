using NovaCare.Modals.TreatmentModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovaCare.Modals.RoomModals
{
    public class RoomType
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string RoomTypeCode { get; set; } = string.Empty;
        public string RoomTypeName { get; set; } = string.Empty;


        //navigation properties
        public List<Room>? Rooms { get; set; }
    }
}
