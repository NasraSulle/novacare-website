using NovaCare.Modals.StaffModals;

namespace NovaCare.Shared.DTOs.StaffDTOs
{
    public class StaffTitleGroupDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public DateTime RegisteredDate { get; set; }
        public List<StaffTitle>? StaffTitles { get; set; }
        public int GroupsCount { get; set; }
    }
}
