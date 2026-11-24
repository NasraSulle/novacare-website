using Microsoft.EntityFrameworkCore;
using NovaCare.Components.Pages.Configurations.WardConfig;
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
                    RoomStatusName = type.RoomStatus != null ? type.RoomStatus.RoomStatusName : string.Empty,
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
                    MedicalDepartmentName = type.MedicalDepartment != null ? type.MedicalDepartment.MedicalDepartmentName : string.Empty,
                    BedsCount = type.Beds != null ? type.Beds.Count : 0,
                    Beds = type.Beds.Select(b => new BedDto
                    {
                        Id = b.Id,
                        BedCodeNumber = b.BedCodeNumber,
                        BedAvailability = b.BedAvailability,
                    }).ToList()
                })
                .ToListAsync();

            return types ?? new();
        }

        public async Task<WardDto> GetWardIdAsync(int wardId)
        {
            using var context = _contextFactory.CreateDbContext();
            var ward = await context.Wards
                .AsNoTracking().Where(x => x.Id == wardId)
                .Select(type => new WardDto
                {
                    Id = type.Id,
                    WardName = type.WardName,
                    WardCost = type.WardCost,
                    BedsCount = type.Beds != null ? type.Beds.Count : 0,
                    MedicalDepartmentId = type.MedicalDepartmentId,
                    MedicalDepartmentName = type.MedicalDepartment != null ? type.MedicalDepartment.MedicalDepartmentName : string.Empty,
                    Beds = type.Beds.Select(b => new BedDto
                    {
                        Id = b.Id,
                        BedCodeNumber = b.BedCodeNumber,
                        BedAvailability = b.BedAvailability,
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return ward ?? new();
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

        public async Task<List<BedDto>> GetAllBedsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var types = await context.Beds
                .AsNoTracking()
                .Select(type => new BedDto
                {
                    Id = type.Id,
                    BedCodeNumber = type.BedCodeNumber,
                    BedAvailability = type.BedAvailability,
                    WardId = type.WardId,
                    WardName = type.Ward != null ? type.Ward.WardName : string.Empty,
                    Ward = type.Ward == null ? null : new WardDto
                    {
                        Id = type.Ward.Id,
                        WardName = type.Ward.WardName,
                        MedicalDepartmentId = type.Ward.MedicalDepartmentId,
                        MedicalDepartmentName = type.Ward.WardName,
                        WardCost = type.Ward.WardCost
                    }
                })
                .ToListAsync();

            return types ?? new();
        }

        public async Task<BedDto> GetBedIdAsync(int bedId)
        {
            using var context = _contextFactory.CreateDbContext();
            var bed = await context.Beds
                .AsNoTracking().Where(x => x.Id == bedId)
                .Select(type => new BedDto
                {
                    Id = type.Id,
                    BedCodeNumber = type.BedCodeNumber,
                    BedAvailability = type.BedAvailability,
                    WardId = type.WardId,
                    WardName = type.Ward != null ? type.Ward.WardName : string.Empty,
                    Ward = type.Ward == null ? null : new WardDto
                    {
                        Id = type.Ward.Id,
                        WardName = type.Ward.WardName,
                        MedicalDepartmentId = type.Ward.MedicalDepartmentId,
                        MedicalDepartmentName = type.Ward.WardName,
                        WardCost = type.Ward.WardCost
                    }
                })
                .FirstOrDefaultAsync();

            return bed ?? new();
        }

        public async Task<List<RoomTypeDto>> GetAllRoomTypesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var types = await context.RoomTypes
                .AsNoTracking()
                .Select(type => new RoomTypeDto
                {
                    Id = type.Id,
                    RoomTypeCode = type.RoomTypeCode,
                    RoomTypeName = type.RoomTypeName
                })
                .ToListAsync();

            return types ?? new();
        }

        public async Task<RoomDto> GetRoomByNameAsync(string roomName)
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
                .FirstOrDefaultAsync(x => x.RoomName == roomName);

            return existingType ?? new();
        }

        public async Task<WardDto> GetWardyNameAsync(string wardName)
        {
            using var context = _contextFactory.CreateDbContext();
            var ward = await context.Wards
                .AsNoTracking().Where(x => x.WardName == wardName)
                .Select(type => new WardDto
                {
                    Id = type.Id,
                    WardName = type.WardName,
                    WardCost = type.WardCost,
                    BedsCount = type.Beds != null ? type.Beds.Count : 0,
                    MedicalDepartmentId = type.MedicalDepartmentId,
                    MedicalDepartmentName = type.MedicalDepartment != null ? type.MedicalDepartment.MedicalDepartmentName : string.Empty,
                    Beds = type.Beds.Select(b => new BedDto
                    {
                        Id = b.Id,
                        BedCodeNumber = b.BedCodeNumber,
                        BedAvailability = b.BedAvailability,
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return ward ?? new();
        }

        public async Task<bool> AddWardAsync(WardDto wardDto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                if (wardDto == null) return false;

                var newWard = new Modals.RoomModals.Ward
                {
                    WardName = wardDto.WardName,
                    WardCost = wardDto.WardCost,
                    MedicalDepartmentId = wardDto.MedicalDepartmentId
                };

                var result = context.Wards.Add(newWard);
                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateWardAsync(WardDto wardDto, int wardId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingWard = await context.Wards.FirstOrDefaultAsync(x => x.Id == wardId);

                if (existingWard != null)
                {
                    existingWard.WardName = wardDto.WardName;
                    existingWard.WardCost = wardDto.WardCost;
                    existingWard.MedicalDepartmentId = wardDto.MedicalDepartmentId;

                    context.Wards.Update(existingWard);
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<BedDto> GetBedByNameAsync(string bedName)
        {
            using var context = _contextFactory.CreateDbContext();
            var bed = await context.Beds
                .AsNoTracking().Where(x => x.BedCodeNumber == bedName)
                .Select(type => new BedDto
                {
                    Id = type.Id,
                    BedCodeNumber = type.BedCodeNumber,
                    BedAvailability = type.BedAvailability,
                    WardId = type.WardId,
                    WardName = type.Ward != null ? type.Ward.WardName : string.Empty,
                    Ward = type.Ward == null ? null : new WardDto
                    {
                        Id = type.Ward.Id,
                        WardName = type.Ward.WardName,
                        MedicalDepartmentId = type.Ward.MedicalDepartmentId,
                        MedicalDepartmentName = type.Ward.WardName,
                        WardCost = type.Ward.WardCost
                    }
                })
                .FirstOrDefaultAsync();

            return bed ?? new();
        }

        public async Task<bool> AddBedAsync(BedDto bedDto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                if (bedDto == null) return false;

                var newBed = new Bed
                {
                    BedCodeNumber = bedDto.BedCodeNumber,
                    WardId = bedDto.WardId
                };

                var result = context.Beds.Add(newBed);
                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateBedAsync(BedDto bedDto, int bedId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingBed = await context.Beds.FirstOrDefaultAsync(x => x.Id == bedId);

                if (existingBed != null)
                {
                    existingBed.BedCodeNumber = bedDto.BedCodeNumber;
                    existingBed.BedAvailability = bedDto.BedAvailability;
                    existingBed.WardId = bedDto.WardId;

                    context.Beds.Update(existingBed);
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
