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
                    LicenceExpDate = doctorDto.LicenceExpDate,
                    DoctorConsultationFee = doctorDto.DoctorConsultationFee,
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

        public async Task<int> AddGenericCodeAsync()
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var doctorCode = new GenericCode();

                var result = context.GenericCodes.Add(doctorCode);
                await context.SaveChangesAsync();

                return result.Entity.CodeNumber;
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
                    DoctorType = new DoctorTypeDto
                    {
                        DoctorTypeName = doc.DoctorType!.DoctorTypeName
                    },
                    DoctorStatusId = doc.DoctorStatusId,
                    RoomId = doc.RoomId,
                    StaffId = doc.StaffId,
                    Staff = new StaffDto 
                                {
                                    StaffStatusId = doc.Staff!.StaffStatusId
                                },
                    DoctorFullName = doc.Staff != null ? $"{doc.Staff.FisrtName ?? string.Empty} {doc.Staff.MiddleName ?? string.Empty} {doc.Staff.LastName ?? string.Empty}" : string.Empty,
                    LicenceExpDate = doc.LicenceExpDate,
                    DoctorConsultationFee = doc.DoctorConsultationFee,

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
                    DoctorType = new DoctorTypeDto
                    {
                        DoctorTypeName = doc.DoctorType!.DoctorTypeName
                    },
                    DoctorStatusId = doc.DoctorStatusId,
                    RoomId = doc.RoomId,
                    StaffId = doc.StaffId,
                    Staff = new StaffDto
                    {
                        StaffStatusId = doc.Staff!.StaffStatusId
                    },
                    DoctorFullName = doc.Staff != null ? $"{doc.Staff.FisrtName ?? string.Empty} {doc.Staff.MiddleName ?? string.Empty} {doc.Staff.LastName ?? string.Empty}" : string.Empty,
                    LicenceExpDate = doc.LicenceExpDate,
                    DoctorConsultationFee = doc.DoctorConsultationFee,

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
                    DeptConsultationFee = title.DeptConsultationFee,
                    MedicalDeptId = title.MedicalDeptId,

                })
                .ToListAsync();

            return docTypes;
        }

        public async Task<GenericCode> GetGenericCodeAsync(string codeFor)
        {
            using var context = _contextFactory.CreateDbContext();
            GenericCode? result = new();

            if (codeFor == "doctor")
            {

                result = await context.GenericCodes
                    .AsNoTracking().AsSplitQuery().FirstOrDefaultAsync(gc => gc.DoctorId == null);
            }

            if (codeFor == "bed")
            {

                result = await context.GenericCodes
                    .AsNoTracking().AsSplitQuery().FirstOrDefaultAsync(gc => gc.BedId == null);
            }

            if (codeFor == "room")
            {

                result = await context.GenericCodes
                    .AsNoTracking().AsSplitQuery().FirstOrDefaultAsync(gc => gc.RoomId == null);
            }
            return result;
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
                    existingDoctor.DoctorConsultationFee = doctor.DoctorConsultationFee;

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

        public async Task<bool> UpdateGenericCodeAsync(int doctorId, int code, string codeFor)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var existingDocCode = await context.GenericCodes.FirstOrDefaultAsync(x => x.CodeNumber == code);

                if (existingDocCode != null)
                {
                    if(codeFor == "doctor")
                        existingDocCode.DoctorId = doctorId;

                    if(codeFor == "bed")
                        existingDocCode.BedId = doctorId;

                    if(codeFor == "room")
                        existingDocCode.RoomId = doctorId;

                    context.GenericCodes.Update(existingDocCode);
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
