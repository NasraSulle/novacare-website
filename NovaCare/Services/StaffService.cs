using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.NativeInterop;
using NovaCare.Data;
using NovaCare.Interfaces;
using NovaCare.Modals.DiseaseModals;
using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.StaffModals;
using NovaCare.Shared.DTOs;
using NovaCare.Shared.DTOs.ReportDTOs;
using NovaCare.Shared.DTOs.RoomDTOs;
using NovaCare.Shared.DTOs.StaffDTOs;
using System.Text.RegularExpressions;
using static MudBlazor.Icons.Material;

namespace NovaCare.Services
{
    public class StaffService : IStaffService
    {
        private readonly IDbContextFactory<NovaCareDbContext> _contextFactory;
        public StaffService(IDbContextFactory<NovaCareDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<int> AddEmployeeIdCodeAsync()
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var emplIdCode = new EmployeeId();

                var result = context.EmployeeIds.Add(emplIdCode);
                await context.SaveChangesAsync();

                return result.Entity.EmployeeIdCode;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> AddStaffDepartmentAsync(StaffTypeDto typeDto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                if (typeDto == null) return false;

                var newStaffType = new StaffType
                {
                    StaffTypeName = typeDto.StaffTypeName,
                    StaffTypeCode = typeDto.StaffTypeCode
                };

                var result = context.StaffTypes.Add(newStaffType);
                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddStaffTitleAsync(StaffTitleDto titleDto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                if (titleDto == null) return false;

                var newStaffTitle = new StaffTitle
                {
                    StaffTitleName = titleDto.StaffTitleName,
                    TitleGroupId = titleDto.TitleGroupId,
                    StaffTypeId = titleDto.StaffTypeId
                };

                var result = context.StaffTitles.Add(newStaffTitle);
                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddStaffTitleGroupAsync(StaffTitleGroupDto groupDto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                if (groupDto == null) return false;

                var newStaffTitle = new StaffTitleGroup
                {
                    GroupName = groupDto.GroupName,
                    RegisteredDate = groupDto.RegisteredDate
                };

                var result = context.StaffTitleGroups.Add(newStaffTitle);
                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AwardStaffOfTheWeekAsync(string staffId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                await context.Staffs
                            .Where(x => x.IsStaffOfTTheWeek)
                            .ExecuteUpdateAsync(setter => setter.SetProperty(x => x.IsStaffOfTTheWeek, false));

                await context.Staffs
                    .Where(x => x.Id == staffId)
                    .ExecuteUpdateAsync(setter => setter.SetProperty(x => x.IsStaffOfTTheWeek, true));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<StaffDto>> GetAllStaffsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var staffs = await context.Staffs
                .AsNoTracking()
                .Select(staff => new StaffDto
                {
                    Id = staff.Id,
                    FisrtName = staff.FisrtName,
                    MiddleName = staff.MiddleName,
                    LastName = staff.LastName,
                    StaffPhoto = staff.StaffPhoto,
                    Email = staff.Email ?? string.Empty,
                    ClinicalTitleName = staff.StaffTitles!.Where(x => x.StaffTypeId == 2).Select(x => x.StaffTitleName).FirstOrDefault() ?? string.Empty,
                    AdministrativeTitleName = staff.StaffTitles!.Where(x => x.StaffTypeId == 1).Select(x => x.StaffTitleName).FirstOrDefault() ?? string.Empty,
                    StaffStatusName = staff.StaffStatus!.StaffStatusName,
                    AdministrativeGroupId = staff.StaffTitles!.Where(x => x.StaffTypeId == 1).Select(x => x.TitleGroupId).FirstOrDefault(),
                    AdministrativeTitleGroupName = staff.StaffTitles!.Where(x => x.StaffTypeId == 1).Select(x => x.StaffTitleGroup!.GroupName).FirstOrDefault() ?? string.Empty,
                    ClinicalGroupId = staff.StaffTitles!.Where(x => x.StaffTypeId == 2).Select(x => x.TitleGroupId).FirstOrDefault(),
                    ClinicalTitleGroupName = staff.StaffTitles!.Where(x => x.StaffTypeId == 2).Select(x => x.StaffTitleGroup!.GroupName).FirstOrDefault() ?? string.Empty,
                    RegisteredDate = staff.RegisteredDate,
                    IsStaffOfTTheWeek = staff.IsStaffOfTTheWeek,
                })
                .ToListAsync();

            return staffs;
        }

        public async Task<ReportPreviewDto<StaffDto>> GetAllStaffsAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var context = _contextFactory.CreateDbContext();
            var staffs = await context.Staffs
                .Where(x => x.RegisteredDate >= fromDate && x.RegisteredDate <= toDate)
                .AsNoTracking()
                .Select(staff => new StaffDto
                {
                    FisrtName = staff.FisrtName,
                    MiddleName = staff.MiddleName,
                    LastName = staff.LastName,
                    StaffPhoto = staff.StaffPhoto,
                    Email = staff.Email ?? string.Empty,
                    ClinicalTitleName = staff.StaffTitles!.Where(x => x.StaffTypeId == 2).Select(x => x.StaffTitleName).FirstOrDefault() ?? string.Empty,
                    AdministrativeTitleName = staff.StaffTitles!.Where(x => x.StaffTypeId == 1).Select(x => x.StaffTitleName).FirstOrDefault() ?? string.Empty,
                    AdministrativeGroupId = staff.StaffTitles!.Where(x => x.StaffTypeId == 1).Select(x => x.TitleGroupId).FirstOrDefault(),
                    ClinicalGroupId = staff.StaffTitles!.Where(x => x.StaffTypeId == 2).Select(x => x.TitleGroupId).FirstOrDefault(),
                    StaffStatusName = staff.StaffStatus!.StaffStatusName,
                    RegisteredDate = staff.RegisteredDate,
                })
                .ToListAsync();

            var result = new ReportPreviewDto<StaffDto>
            {
                FromDate = fromDate,
                ToDate = toDate,
                ReportType = "Staff",
                Data = staffs
            };
            return result;
        }

        public async Task<List<StaffTitleGroupDto>> GetAllStaffTitleGroupssAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var staffGroups = await context.StaffTitleGroups
                .AsNoTracking()
                .Select(group => new StaffTitleGroupDto
                {
                    Id = group.Id,
                    GroupName = group.GroupName,
                    RegisteredDate = group.RegisteredDate,
                    GroupsCount = group.StaffTitles != null ? group.StaffTitles.Count : 0,
                })
                .ToListAsync();

            return staffGroups ?? new();
        }

        public async Task<StaffDto> GetStaffById(string staffId)
        {
            using var context = _contextFactory.CreateDbContext();
            var existingStaff = await context.Staffs
                .AsNoTracking()
                .Select(staff => new StaffDto
                {
                    Id = staff.Id,
                    FisrtName = staff.FisrtName,
                    MiddleName = staff.MiddleName,
                    LastName = staff.LastName,
                    StaffPhoto = staff.StaffPhoto,
                    Email = staff.Email ?? string.Empty,
                    RegisteredDate = staff.RegisteredDate,
                    Age = staff.Age,
                    EmployeeId = staff.EmployeeId,
                    Gender = staff.Gender,

                    //for permissions
                    IsGetStaffModules = staff.IsGetStaffModules,
                    IsViewAllAdmins = staff.IsViewAllAdmins,
                    IsViewAllNurses = staff.IsViewAllNurses,
                    IsViewAllPatients = staff.IsViewAllPatients,
                    IsViewAllDoctors = staff.IsViewAllDoctors,
                    IsViewAllStaffs = staff.IsViewAllStaffs,
                    IsViewAllHelps = staff.IsViewAllHelps,
                    IsViewAllLeaders = staff.IsViewAllLeaders,
                    IsViewAllMedicines = staff.IsViewAllMedicines,
                    IsViewAllRooms = staff.IsViewAllRooms,
                    IsViewAllLabTests = staff.IsViewAllLabTests,

                    IsViewLabTestById = staff.IsViewLabTestById,
                    IsViewPatientById = staff.IsViewPatientById,
                    IsViewDoctorById = staff.IsViewDoctorById,
                    IsViewStaffById = staff.IsViewStaffById,
                    IsViewHelpById = staff.IsViewHelpById,
                    IsViewLeaderById = staff.IsViewLeaderById,
                    IsViewMedicineById = staff.IsViewMedicineById,
                    IsViewRoomById = staff.IsViewRoomById,

                    IsCreateNewLabTest = staff.IsCreateNewLabTest,
                    IsCreateNewPatient = staff.IsCreateNewPatient,
                    IsCreateNewStaff = staff.IsCreateNewStaff,
                    IsCreateNewMedicine = staff.IsCreateNewMedicine,

                    IsUpdateOrEditLabTest = staff.IsUpdateOrEditLabTest,
                    IsUpdateOrEditPatient = staff.IsUpdateOrEditPatient,
                    IsUpdateOrEditStaff = staff.IsUpdateOrEditStaff,
                    IsUpdateOrEditMedicine = staff.IsUpdateOrEditMedicine,
                    CanChooseStaffOfTheWeek = staff.CanChooseStaffOfTheWeek,
                    IsStaffOfTTheWeek = staff.IsStaffOfTTheWeek,

                    ClinicalTitleName = staff.StaffTitles.Where(x => x.StaffTypeId == 2).Select(x => x.StaffTitleName).FirstOrDefault() ?? string.Empty,
                    AdministrativeTitleName = staff.StaffTitles.Where(x => x.StaffTypeId == 1).Select(x => x.StaffTitleName).FirstOrDefault() ?? string.Empty,

                    AdministrativeTitleId = staff.StaffTitles.Where(x => x.StaffTypeId == 1).Select(x => x.Id).FirstOrDefault(),
                    ClinicalTitleId = staff.StaffTitles.Where(x => x.StaffTypeId == 2).Select(x => x.Id).FirstOrDefault(),

                    AdministrativeGroupId = staff.StaffTitles.Where(x => x.StaffTypeId == 1).Select(x => x.TitleGroupId).FirstOrDefault(),
                    ClinicalGroupId = staff.StaffTitles.Where(x => x.StaffTypeId == 2).Select(x => x.TitleGroupId).FirstOrDefault(),


                    StaffStatusName = staff.StaffStatus != null ? staff.StaffStatus.StaffStatusName : string.Empty,
                    StaffStatusId = staff.StaffStatusId,
                })
                .FirstOrDefaultAsync( x => x.Id == staffId);

            return existingStaff ?? new();
        }

        
        public async Task<List<StaffStatusDto>> GetStaffStatusAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var staffStatus = await context.StaffStatuses
                .AsNoTracking()
                .Select(status => new StaffStatusDto
                {
                    Id = status.Id,
                    StaffStatusName = status.StaffStatusName,

                })
                .ToListAsync();

            return staffStatus;
        }

        public async Task<StaffTitleDto> GetStaffTitleByIdAsync(int titleId)
        {
            using var context = _contextFactory.CreateDbContext();
            var existingStaff = await context.StaffTitles
                .AsNoTracking()
                .Select(staff => new StaffTitleDto
                {
                    Id = staff.Id,
                    StaffTitleName = staff.StaffTitleName,
                    StaffTypeId = staff.StaffTypeId,
                    TitleGroupId = staff.TitleGroupId,
                    StaffsCount = staff.Staffs != null ? staff.Staffs.Count : 0,
                })
                .FirstOrDefaultAsync(x => x.Id == titleId);

            return existingStaff ?? new();
        }

        public async Task<StaffTitleGroupDto> GetStaffTitleGroupByIdAsync(int groupId)
        {
            using var context = _contextFactory.CreateDbContext();
            var existingStaffGroup = await context.StaffTitleGroups
                .AsNoTracking()
                .Select(group => new StaffTitleGroupDto
                {
                    Id = group.Id,
                    RegisteredDate = group.RegisteredDate,
                    GroupName = group.GroupName,
                    GroupsCount = group.StaffTitles != null ? group.StaffTitles.Count : 0,
                })
                .FirstOrDefaultAsync(x => x.Id == groupId);

            return existingStaffGroup ?? new();
        }

        public async Task<StaffTitleGroupDto> GetStaffTitleGroupIdByNameAsync(string groupName)
        {
            using var context = _contextFactory.CreateDbContext();
            var existingStaffGroup = await context.StaffTitleGroups
                .AsNoTracking()
                .Select(group => new StaffTitleGroupDto
                {
                    Id = group.Id,
                    RegisteredDate = group.RegisteredDate,
                    GroupName = group.GroupName,
                    GroupsCount = group.StaffTitles != null ? group.StaffTitles.Count : 0,
                })
                .FirstOrDefaultAsync(x => x.GroupName == groupName);

            return existingStaffGroup ?? new();
        }

        public async Task<StaffTitleDto> GetStaffTitleIdByNameAsync(string titleName)
        {
            using var context = _contextFactory.CreateDbContext();
            var existingtitle = await context.StaffTitles
                .AsNoTracking()
                .Select(title => new StaffTitleDto
                {
                    Id = title.Id,
                    RegisteredDate = title.RegisteredDate,
                    StaffTitleName = title.StaffTitleName,
                    StaffTypeId = title.StaffTypeId,
                    TitleGroupId = title.TitleGroupId,
                    StaffTypeName = title.StaffType != null ? title.StaffType.StaffTypeName : string.Empty,
                    TitleGroupName = title.StaffTitleGroup != null ? title.StaffTitleGroup.GroupName : string.Empty,
                    StaffsCount = title.Staffs != null ? title.Staffs.Count : 0,
                })
                .FirstOrDefaultAsync(x => x.StaffTitleName == titleName);

            return existingtitle ?? new();
        }

        public async Task<List<StaffTitleDto>> GetStaffTitlesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var staffTitles = await context.StaffTitles
                .AsNoTracking()
                .Select(title => new StaffTitleDto
                {
                    Id = title.Id,
                    RegisteredDate = title.RegisteredDate,
                    StaffTitleName = title.StaffTitleName,
                    StaffTypeId = title.StaffTypeId,
                    TitleGroupId = title.TitleGroupId,
                    StaffTypeName = title.StaffType != null ? title.StaffType.StaffTypeName : string.Empty,
                    TitleGroupName = title.StaffTitleGroup != null ? title.StaffTitleGroup.GroupName : string.Empty,
                    StaffsCount = title.Staffs != null ? title.Staffs.Count : 0,

                })
                .ToListAsync();

            return staffTitles;
         }

        public async Task<StaffTypeDto> GetStaffTypeByIdAsync(int typeId)
        {
            using var context = _contextFactory.CreateDbContext();
            var existingType = await context.StaffTypes
                .AsNoTracking()
                .Select(type => new StaffTypeDto
                {
                    Id = type.Id,
                    StaffTypeCode = type.StaffTypeCode,
                    StaffTypeName = type.StaffTypeName,
                    StaffsTitleCount = type.StaffTitles != null ? type.StaffTitles.Count : 0,
                })
                .FirstOrDefaultAsync(x => x.Id == typeId);

            return existingType ?? new();
        }

        public async Task<StaffTypeDto> GetStaffTypeIdByNameAsync(string deptName)
        {
            using var context = _contextFactory.CreateDbContext();
            var existingType = await context.StaffTypes
                .AsNoTracking()
                .Select(group => new StaffTypeDto
                {
                    Id = group.Id,
                    RegisteredDate = group.RegisteredDate,
                    StaffTypeName = group.StaffTypeName,
                    StaffsTitleCount = group.StaffTitles != null ? group.StaffTitles.Count : 0,
                })
                .FirstOrDefaultAsync(x => x.StaffTypeName == deptName);

            return existingType ?? new();
        }

        public async Task<List<StaffTypeDto>> GetStaffTypesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var staffTypes = await context.StaffTypes
                .AsNoTracking()
                .Select(title => new StaffTypeDto
                {
                    Id = title.Id,
                    StaffTypeName = title.StaffTypeName,

                })
                .ToListAsync();

            return staffTypes;
        }

        public async Task<bool> UpdateEmployeeIdCodeAsync(string staffId, int code)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingEmpId = await context.EmployeeIds.FirstOrDefaultAsync(x => x.EmployeeIdCode == code);

                if (existingEmpId != null)
                {
                    existingEmpId.StaffId = staffId;

                    context.EmployeeIds.Update(existingEmpId);
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateStaffAsync(StaffDto staff, string staffId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingStaff = await context.Staffs.FirstOrDefaultAsync(x => x.Id == staffId);

                if (existingStaff != null)
                {
                    existingStaff.FisrtName = staff.FisrtName;
                    existingStaff.MiddleName = staff.MiddleName;
                    existingStaff.LastName = staff.LastName;
                    existingStaff.StaffPhoto = staff.StaffPhoto;
                    existingStaff.Age = staff.Age;
                    existingStaff.EmployeeId = staff.EmployeeId;
                    existingStaff.Address = staff.Address;
                    existingStaff.PhoneNumber = staff.PhoneNumber;
                    existingStaff.Email = staff.Email;
                    existingStaff.Gender = staff.Gender;

                    existingStaff.IsGetStaffModules = staff.IsGetStaffModules;
                    existingStaff.IsViewAllPatients = staff.IsViewAllPatients;

                    existingStaff.IsViewAllDoctors = staff.IsViewAllDoctors;
                    existingStaff.IsViewAllStaffs = staff.IsViewAllStaffs;
                    existingStaff.IsViewAllHelps = staff.IsViewAllHelps;
                    existingStaff.IsViewAllLeaders = staff.IsViewAllLeaders;
                    existingStaff.IsViewAllMedicines = staff.IsViewAllMedicines;
                    existingStaff.IsViewAllRooms = staff.IsViewAllRooms;
                    existingStaff.IsViewAllNurses = staff.IsViewAllNurses;
                    existingStaff.IsViewAllAdmins = staff.IsViewAllAdmins;
                    existingStaff.IsViewAllLabTests = staff.IsViewAllLabTests;

                    existingStaff.IsViewLabTestById = staff.IsViewLabTestById;
                    existingStaff.IsViewPatientById = staff.IsViewPatientById;
                    existingStaff.IsViewDoctorById = staff.IsViewDoctorById;
                    existingStaff.IsViewStaffById = staff.IsViewStaffById;
                    existingStaff.IsViewHelpById = staff.IsViewHelpById;
                    existingStaff.IsViewLeaderById = staff.IsViewLeaderById;
                    existingStaff.IsViewMedicineById = staff.IsViewMedicineById;
                    existingStaff.IsViewRoomById = staff.IsViewRoomById;

                    existingStaff.IsCreateNewLabTest = staff.IsCreateNewLabTest;
                    existingStaff.IsCreateNewPatient = staff.IsCreateNewPatient;
                    existingStaff.IsCreateNewStaff = staff.IsCreateNewStaff;
                    existingStaff.IsCreateNewMedicine = staff.IsCreateNewMedicine;

                    existingStaff.IsUpdateOrEditLabTest = staff.IsUpdateOrEditLabTest;
                    existingStaff.IsUpdateOrEditPatient = staff.IsUpdateOrEditPatient;
                    existingStaff.IsUpdateOrEditStaff = staff.IsUpdateOrEditStaff;
                    existingStaff.IsUpdateOrEditMedicine = staff.IsUpdateOrEditMedicine;


                    existingStaff.StaffStatusId = staff.StaffStatusId;

                    foreach (var extitle in existingStaff.StaffTitles ?? new())
                    {
                        extitle.StaffTitleName = staff.StaffTitles?.FirstOrDefault(t => t.Id == extitle.Id)?.StaffTitleName ?? string.Empty;
                        extitle.StaffTypeId = staff.StaffTitles?.FirstOrDefault(t => t.Id == extitle.Id)?.StaffTypeId ?? 0;
                        extitle.TitleGroupId = staff.StaffTitles?.FirstOrDefault(t => t.Id == extitle.Id)?.TitleGroupId ?? 0;
                    }

                    context.Staffs.Update(existingStaff);
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateStaffDepartmentAsync(StaffTypeDto staffType, int deptId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingDept = await context.StaffTypes.FirstOrDefaultAsync(x => x.Id == deptId);

                if (existingDept != null)
                {
                    existingDept.StaffTypeCode = staffType.StaffTypeCode;
                    existingDept.StaffTypeName = staffType.StaffTypeName;

                    context.StaffTypes.Update(existingDept);
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateStaffTitleAsync(StaffTitleDto staffTitle, int titled)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingTitle = await context.StaffTitles.FirstOrDefaultAsync(x => x.Id == titled);

                if (existingTitle != null)
                {
                    existingTitle.StaffTitleName = staffTitle.StaffTitleName;
                    existingTitle.StaffTypeId = staffTitle.StaffTypeId;
                    existingTitle.TitleGroupId = staffTitle.TitleGroupId;

                    context.StaffTitles.Update(existingTitle);
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateTitleGroupAsync(StaffTitleGroupDto staffGroup, int groupId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingGroup = await context.StaffTitleGroups.FirstOrDefaultAsync(x => x.Id == groupId);

                if (existingGroup != null)
                {
                    existingGroup.GroupName = staffGroup.GroupName;

                    context.StaffTitleGroups.Update(existingGroup);
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
