using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.PatientModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NovaCare.Modals.RoomModals
{
    public class Room
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string RoomName { get; set; } = string.Empty;

        //foreign keys
        [ForeignKey(nameof(RoomStatus))]
        public int RoomStatusId { get; set; }
        [ForeignKey(nameof(RoomType))]
        public int RoomTypeId { get; set; }

        //navigation properties
        public Doctor? Doctor { get; set; }
        public RoomStatus? RoomStatus { get; set; }
        public RoomType? RoomType { get; set; }
        public List<Consultation>? Consultation { get; set; }

    }
}
