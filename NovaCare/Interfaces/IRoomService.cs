using NovaCare.Shared.DTOs.RoomDTOs;

namespace NovaCare.Interfaces
{
    public interface IRoomService
    {
        Task<bool> AddRoomAsync(RoomDto roomDto);
        Task<bool> UpdateRoomAsync(RoomDto roomDto, int romId);
        Task<List<RoomDto>> GetAllRoomsAsync();
        Task<RoomDto> GetRoomByIdAsync(int roomId);
    }
}
