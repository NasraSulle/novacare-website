using NovaCare.Modals.RoomModals;

namespace NovaCare.Shared.DTOs.RoomDTOs
{
    public class BedDto
    {
        public int Id { get; set; }
        public string BedCodeNumber { get; set; } = string.Empty;
        public string BedAvailability { get; set; } = "UnUsed";

        public int? WardId { get; set; }
        public WardDto? Ward { get; set; }
        public string WardName { get; set; } = string.Empty;

    }
}
