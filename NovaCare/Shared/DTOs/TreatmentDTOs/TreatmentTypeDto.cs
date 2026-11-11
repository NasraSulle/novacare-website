namespace NovaCare.Shared.DTOs.TreatmentDTOs
{
    public class TreatmentTypeDto
    {
        public int Id { get; set; }
        public string TreatmentTypeCode { get; set; } = string.Empty;
        public string TreatmentTypeName { get; set; } = string.Empty;

        // UI & Validation Feature Flags
        public bool HasSurgeryId { get; set; }
        public bool HasTherapyId { get; set; }
        public bool HasMedicalDeptId { get; set; }
        public bool HasWardId { get; set; }
        public bool HasDoctorId { get; set; }
        public bool HasRoomId { get; set; }
    }
}
