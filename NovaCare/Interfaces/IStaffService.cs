using NovaCare.Shared.DTOs;
using NovaCare.Shared.DTOs.DiseaseDTOs;
using NovaCare.Shared.DTOs.ReportDTOs;
using NovaCare.Shared.DTOs.StaffDTOs;

namespace NovaCare.Interfaces
{
    public interface IStaffService
    {
        Task<ReportPreviewDto<StaffDto>> GetAllStaffsAsync(DateTime? fromDate, DateTime? toDate);
        Task<List<StaffDto>> GetAllStaffsAsync();

        Task<List<StaffTitleGroupDto>> GetAllStaffTitleGroupssAsync();

        Task<List<StaffTypeDto>> GetStaffTypesAsync();
        Task<List<StaffStatusDto>> GetStaffStatusAsync();
        Task<List<StaffTitleDto>> GetStaffTitlesAsync();

        Task<StaffDto> GetStaffById(string staffId);
        Task<StaffTypeDto> GetStaffTypeByIdAsync(int typeId);
        Task<StaffTitleDto> GetStaffTitleByIdAsync(int titleId);
        Task<StaffTitleGroupDto> GetStaffTitleGroupByIdAsync(int groupId);


        Task<bool> AwardStaffOfTheWeekAsync(string staffId);

        Task<int> AddEmployeeIdCodeAsync();
        Task<bool> AddStaffTitleAsync(StaffTitleDto titleDto);
        Task<bool> AddStaffTitleGroupAsync(StaffTitleGroupDto groupDto);
        Task<bool> AddStaffDepartmentAsync(StaffTypeDto typeDto);

        Task<bool> UpdateEmployeeIdCodeAsync(string staffId, int code);
        Task<bool> UpdateStaffAsync(StaffDto staff, string staffId);
        Task<bool> UpdateStaffTitleAsync(StaffTitleDto staffTitle, int titled);
        Task<bool> UpdateTitleGroupAsync(StaffTitleGroupDto staffGroup, int groupId);
        Task<bool> UpdateStaffDepartmentAsync(StaffTypeDto staffType, int deptId);

        Task<StaffTitleGroupDto> GetStaffTitleGroupIdByNameAsync(string groupName);
        Task<StaffTitleDto> GetStaffTitleIdByNameAsync(string titleName);
        Task<StaffTypeDto> GetStaffTypeIdByNameAsync(string deptName);

    }
}
