using Microsoft.EntityFrameworkCore;
using NovaCare.Components.Pages.Configurations;
using NovaCare.Data;
using NovaCare.Interfaces;
using NovaCare.Modals.RoomModals;
using NovaCare.Modals.StaffModals;
using NovaCare.Shared.DTOs.DiseaseDTOs;
using NovaCare.Shared.DTOs.RoomDTOs;
using static MudBlazor.Icons.Material;

namespace NovaCare.Services
{
    public class RoomService : IRoomService
    {

        private readonly IDbContextFactory<NovaCareDbContext> _contextFactory;
        public RoomService(IDbContextFactory<NovaCareDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<bool> AddRoomAsync(RoomDto roomDto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                if (roomDto == null) return false;

                var newRoom = new Room
                {
                    RoomName = roomDto.RoomName,
                    RoomTypeId = roomDto.RoomTypeId,
                    RoomStatusId = roomDto.RoomStatusId
                };

                var result = context.Rooms.Add(newRoom);
                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<RoomDto>> GetAllRoomsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var types = await context.Rooms
                .AsNoTracking()
                .Select(type => new RoomDto
                {
                    Id = type.Id,
                    RoomName = type.RoomName,
                    RoomStatusId = type.RoomStatusId,
                    RoomTypeId = type.RoomTypeId,
                    RoomTypeName = type.RoomType != null ? type.RoomType.RoomTypeName : string.Empty,
                    RoomStatusName = type.RoomStatus != null ? type.RoomStatus.RoomStatusName : string.Empty
                })
                .ToListAsync();

            return types ?? new();
        }

        public async Task<List<WardDto>> GetAllWardsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var types = await context.Wards
                .AsNoTracking()
                .Select(type => new WardDto
                {
                    Id = type.Id,
                    WardName = type.WardName,
                    WardCost = type.WardCost,
                    MedicalDepartmentId = type.MedicalDepartmentId,
                    MedicalDepartmentName = type.MedicalDepartment != null ? type.MedicalDepartment.MedicalDepartmentName : string.Empty
                })
                .ToListAsync();

            return types ?? new();
        }

        public async Task<RoomDto> GetRoomByIdAsync(int roomId)
        {
            using var context = _contextFactory.CreateDbContext();
            var existingType = await context.Rooms
                .AsNoTracking()
                .Select(type => new RoomDto
                {
                    Id = type.Id,
                    RoomName = type.RoomName,
                    RoomStatusId = type.RoomStatusId,
                    RoomTypeId = type.RoomTypeId,
                    RoomTypeName = type.RoomType != null ? type.RoomType.RoomTypeName : string.Empty,
                    RoomStatusName = type.RoomStatus != null ? type.RoomStatus.RoomStatusName : string.Empty
                })
                .FirstOrDefaultAsync(x => x.Id == roomId);

            return existingType ?? new();
        }

        public async Task<bool> UpdateRoomAsync(RoomDto roomDto, int romId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingRoom = await context.Rooms.FirstOrDefaultAsync(x => x.Id == romId);

                if (existingRoom != null)
                {
                    existingRoom.RoomName = roomDto.RoomName;
                    existingRoom.RoomStatusId = roomDto.RoomStatusId;
                    existingRoom.RoomTypeId = roomDto.RoomTypeId;

                    context.Rooms.Update(existingRoom);
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
