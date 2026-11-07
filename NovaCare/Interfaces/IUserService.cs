using NovaCare.Shared.DTOs;
using NovaCare.Shared.DTOs.StaffDTOs;

namespace NovaCare.Interfaces
{
    public interface IUserService
    {
        Task<StaffDto> GetLoggedInUserAsync();
        Task<string> FindStaffValidEmailAddressAsync(string email);
        Task<string> SendNewPasswordAsync(string email);
    }
}
