using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NovaCare.Components.Pages.StaffPages;
using NovaCare.Data;
using NovaCare.Interfaces;
using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.StaffModals;
using NovaCare.Shared.DTOs.DoctorDTOs;
using NovaCare.Shared.DTOs.StaffDTOs;

namespace NovaCare.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDbContextFactory<NovaCareDbContext> _contextFactory;
        public DoctorService(IDbContextFactory<NovaCareDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<bool> AddDoctorAsync(DoctorDto doctorDto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var doctor = new Doctor
                {
                    DoctorCode = doctorDto.DoctorCode,
                    MedicalLicenseNumber = doctorDto.MedicalLicenseNumber,
                    DoctorTypeId = doctorDto.DoctorTypeId,
                    DoctorStatusId = 1,
                    StaffId = doctorDto.StaffId
                };

                context.Doctors.Add(doctor);
                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> AddDoctorCodeAsync()
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var doctorCode = new DoctorCode();

                var result = context.DoctorCodes.Add(doctorCode);
                await context.SaveChangesAsync();

                return result.Entity.DocCode;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<List<DoctorDto>> GetAllDoctorsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var doctor = await context.Doctors
                .AsNoTracking()
                .Select(doc => new DoctorDto
                {
                    Id = doc.Id,
                    DoctorCode = doc.DoctorCode,
                    MedicalLicenseNumber = doc.MedicalLicenseNumber,
                    DoctorTypeId = doc.DoctorTypeId,
                    DoctorStatusId = doc.DoctorStatusId,
                    RoomId = doc.RoomId,
                    StaffId = doc.StaffId,
                    DoctorFullName = doc.Staff != null ? $"{doc.Staff.FisrtName ?? string.Empty} {doc.Staff.MiddleName ?? string.Empty} {doc.Staff.LastName ?? string.Empty}" : string.Empty,
                    LicenceExpDate = doc.LicenceExpDate,

                })
                .ToListAsync();

            return doctor ?? new();
        }

        public async Task<DoctorDto> GetDoctorByStaffId(string staffId)
        {
            using var context = _contextFactory.CreateDbContext();
            var doctor = await context.Doctors
                .AsNoTracking()
                .Select(doc => new DoctorDto
                {
                    Id = doc.Id,
                    DoctorCode = doc.DoctorCode,
                    MedicalLicenseNumber = doc.MedicalLicenseNumber,
                    DoctorTypeId = doc.DoctorTypeId,
                    DoctorStatusId = doc.DoctorStatusId,
                    RoomId = doc.RoomId,
                    StaffId = doc.StaffId,
                    DoctorFullName = doc.Staff != null ? $"{doc.Staff.FisrtName ?? string.Empty} {doc.Staff.MiddleName ?? string.Empty} {doc.Staff.LastName ?? string.Empty}" : string.Empty,
                    LicenceExpDate = doc.LicenceExpDate,

                })
                .FirstOrDefaultAsync(x => x.StaffId == staffId);

            return doctor ?? new();
        }

        public async Task<List<DoctorTypeDto>> GetDoctorTypesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var docTypes = await context.DoctorTypes
                .AsNoTracking()
                .Select(title => new DoctorTypeDto
                {
                    Id = title.Id,
                    DoctorTypeName = title.DoctorTypeName,

                })
                .ToListAsync();

            return docTypes;
        }

        public async Task<bool> UpdateDoctorAsync(DoctorDto doctor, int doctorId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingDoctor = await context.Doctors.FirstOrDefaultAsync(x => x.Id == doctorId);

                if (existingDoctor != null)
                {
                    existingDoctor.DoctorCode = doctor.DoctorCode;
                    existingDoctor.MedicalLicenseNumber = doctor.MedicalLicenseNumber;
                    existingDoctor.DoctorTypeId = doctor.DoctorTypeId;
                    existingDoctor.DoctorStatusId = doctor.DoctorStatusId;
                    existingDoctor.RoomId = doctor.RoomId;
                    existingDoctor.StaffId = doctor.StaffId;
                    existingDoctor.LicenceExpDate = doctor.LicenceExpDate;

                    context.Doctors.Update(existingDoctor);
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateDoctorCodeAsync(int doctorId, int code)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingDocCode = await context.DoctorCodes.FirstOrDefaultAsync(x => x.DocCode == code);

                if (existingDocCode != null)
                {
                    existingDocCode.DoctorId = doctorId;

                    context.DoctorCodes.Update(existingDocCode);
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
