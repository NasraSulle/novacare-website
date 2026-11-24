using NovaCare.Shared.DTOs.RoomDTOs;

namespace NovaCare.Interfaces
{
    public interface IRoomService
    {
        Task<bool> AddRoomAsync(RoomDto roomDto);
        Task<bool> UpdateRoomAsync(RoomDto roomDto, int romId);
        Task<List<RoomDto>> GetAllRoomsAsync();
        Task<RoomDto> GetRoomByIdAsync(int roomId);
        Task<List<RoomTypeDto>> GetAllRoomTypesAsync();
        Task<RoomDto> GetRoomByNameAsync(string roomName);


        Task<bool> AddWardAsync(WardDto wardDto);
        Task<bool> UpdateWardAsync(WardDto wardDto, int wardId);
        Task<List<WardDto>> GetAllWardsAsync();
        Task<WardDto> GetWardyNameAsync(string wardName);
        Task<WardDto> GetWardIdAsync(int wardId);


        Task<List<BedDto>> GetAllBedsAsync();
        Task<BedDto> GetBedIdAsync(int bedId);
        Task<BedDto> GetBedByNameAsync(string bedName);
        Task<bool> AddBedAsync(BedDto bedDto);
        Task<bool> UpdateBedAsync(BedDto bedDto, int bedId);
    }
}
