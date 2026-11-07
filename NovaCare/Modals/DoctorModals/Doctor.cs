using NovaCare.Modals.JoinTables;
using NovaCare.Modals.PatientModals;
using NovaCare.Modals.RoomModals;
using NovaCare.Modals.StaffModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.DoctorModals
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        public string DoctorCode { get; set; } = string.Empty;
        public string MedicalLicenseNumber { get; set; } = string.Empty;

        // Foreign keys
        [ForeignKey(nameof(DoctorType))]
        public int DoctorTypeId { get; set; }

        [ForeignKey(nameof(DoctorStatus))]
        public int DoctorStatusId { get; set; }

        [ForeignKey(nameof(Room))]
        public int? RoomId { get; set; }

        [ForeignKey(nameof(Staff))]
        public string StaffId { get; set; } = string.Empty;
        public DateTime? LicenceExpDate { get; set; }

        // Navigation properties
        public Staff? Staff { get; set; }
        public Room? Room { get; set; }
        public DoctorStatus? DoctorStatus { get; set; }
        public DoctorType? DoctorType { get; set; }
        public List<DoctorPatient>? DoctorPatients { get; set; }
        public List<Consultation>? Consultations { get; set; }
        public List<Service>? Services { get; set; }
    }
}
