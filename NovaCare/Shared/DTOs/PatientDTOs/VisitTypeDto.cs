namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class VisitTypeDto
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = string.Empty; // Keep as TypeNme if you kept TypeNme on VisitType
    }
}
