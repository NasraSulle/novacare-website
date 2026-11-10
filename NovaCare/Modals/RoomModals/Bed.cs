namespace NovaCare.Modals.RoomModals
{
    public class Bed
    {
        public int Id { get; set; }
        public string BedCodeNumber { get; set; } = string.Empty;
        public string BedAvailability { get; set; } = "UnUsed";

        public int? WardId { get; set; }
        public Ward? Ward { get; set; }
    }
}
