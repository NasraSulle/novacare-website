namespace NovaCare.Modals.StaffModals
{
    public class StaffTitleGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public DateTime RegisteredDate { get; set; }

        public List<StaffTitle>? StaffTitles { get; set; }
    }
}
