using NovaCare.Modals.DoctorModals;
using NovaCare.Shared.DTOs.DoctorDTOs;
using NovaCare.Shared.DTOs.StaffDTOs;

namespace NovaCare.Interfaces
{
    public interface IDoctorService
    {
        Task<List<DoctorDto>> GetAllDoctorsAsync();
        Task<bool> AddDoctorAsync(DoctorDto doctorDto);
        Task<DoctorDto> GetDoctorByStaffId(string staffId);
        Task<bool> UpdateDoctorAsync(DoctorDto doctor, int doctorId);
        Task<int> AddGenericCodeAsync();
        Task<bool> UpdateGenericCodeAsync(int doctorId, int code, string codeFor);
        Task<GenericCode> GetGenericCodeAsync(string codeFor);

        //docType
        Task<List<DoctorTypeDto>> GetDoctorTypesAsync();
        Task<bool> AddDoctorTypeAsync(DoctorTypeDto doctorTypeDto);
        Task<bool> UpdateDoctorTypeAsync(DoctorTypeDto doctorType, int doctorTypeId);
        Task<DoctorTypeDto> GetDoctorTypeByIdAsync(int docTypeId);
        Task<DoctorTypeDto> GetDoctorTypeByNameAsync(string docTypeName);

    }
}
