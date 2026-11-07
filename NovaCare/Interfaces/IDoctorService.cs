using NovaCare.Shared.DTOs.DoctorDTOs;
using NovaCare.Shared.DTOs.StaffDTOs;

namespace NovaCare.Interfaces
{
    public interface IDoctorService
    {
        Task<List<DoctorDto>> GetAllDoctorsAsync();
        Task<List<DoctorTypeDto>> GetDoctorTypesAsync();
        Task<bool> AddDoctorAsync(DoctorDto doctorDto);
        Task<DoctorDto> GetDoctorByStaffId(string staffId);
        Task<bool> UpdateDoctorAsync(DoctorDto doctor, int doctorId);
        Task<int> AddDoctorCodeAsync();
        Task<bool> UpdateDoctorCodeAsync(int doctorId, int code);
    }
}
