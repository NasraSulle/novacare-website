using NovaCare.Modals.DiseaseModals;
using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.JoinTables;
using NovaCare.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.PatientModals
{
    public class Patient
    {
        // Properties
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PatientFullName { get; set; } = string.Empty;

        public string PatientCode { get; set; } = string.Empty;
        public byte[]? PatientPhoto { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string HomeAddress { get; set; } = string.Empty;
        public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;
        public Gender Gender { get; set; }

        // First of Kin
        public string KinFullName { get; set; } = string.Empty;
        public string KinPhoneNumber { get; set; } = string.Empty;
        public string KinHomeAddress { get; set; } = string.Empty;

        // Foreign keys
        [ForeignKey(nameof(PatientStatus))]
        public int PatientStatusId { get; set; }

        [ForeignKey(nameof(PatientType))]
        public int PatientTypeId { get; set; }

        // Navigation properties
        public PatientStatus? PatientStatus { get; set; }
        public PatientType? PatientType { get; set; }
        public List<PatientDisease> PatientDiseases { get; set; } = new();
        public List<DoctorPatient> DoctorPatients { get; set; } = new();
        public List<PatientInsuarance> PatientInsuarances { get; set; } = new();
        public List<Visit> Visits { get; set; } = new();
        public List<Allergy> Allergies { get; set; } = new();
    }
}
