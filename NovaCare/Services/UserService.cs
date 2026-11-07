using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NovaCare.Data;
using NovaCare.Interfaces;
using NovaCare.Modals.StaffModals;
using NovaCare.Shared.DTOs;
using NovaCare.Shared.DTOs.StaffDTOs;
using System.Security.Claims;

namespace NovaCare.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<Staff> _userManager;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly IDbContextFactory<NovaCareDbContext> _contextFactory;
        public UserService(UserManager<Staff> userManager, AuthenticationStateProvider authStateProvider, IDbContextFactory<NovaCareDbContext> contextFactory)
        {
            _userManager = userManager;
            _authStateProvider = authStateProvider;
            _contextFactory = contextFactory;
        }
        public static async Task SeedDefaultAdmin(IServiceProvider serviceProvider)
        {
            //var dbContext = serviceProvider.GetRequiredService<NovaCareDbContext>();
            //await dbContext.Database.MigrateAsync();

            var userManager = serviceProvider.GetRequiredService<UserManager<Staff>>();

            var adminEmail = "admin@novacare.com";
            var password = "Admin@12345";

            var existingAdmin = await userManager.FindByNameAsync(adminEmail);

            if (existingAdmin == null)
            {
                List<StaffTitle> staffTitlesToAdd = new();

                var title = new StaffTitle
                {
                    //Id = 8,
                    StaffTitleName = "System Admin",
                    TitleGroupId = 4, //Administrators
                    StaffTypeId = 1 //System Administrator
                };

                staffTitlesToAdd.Add(title);


                var defaultAdmin = new Staff
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FisrtName = "System",
                    MiddleName = "Super",
                    LastName = "Administrator",
                    RegisteredDate = DateTime.UtcNow,
                    Age = 30,

                    StaffTitles = staffTitlesToAdd,

                    StaffStatusId = 1, //Available

                    IsGetStaffModules = true,
                    IsViewAllPatients = true,
                    IsViewAllDoctors = true,
                    IsViewAllStaffs = true,
                    IsViewAllHelps = true,
                    IsViewAllLeaders = true,
                    IsViewAllMedicines = true,
                    IsViewAllRooms = true,
                    IsViewAllNurses = true,
                    IsViewAllAdmins = true,
                    IsViewAllLabTests = true,

                    IsViewLabTestById = true,
                    IsViewPatientById = true,
                    IsViewDoctorById = true,
                    IsViewStaffById = true,
                    IsViewHelpById = true,
                    IsViewLeaderById = true,
                    IsViewMedicineById = true,
                    IsViewRoomById = true,

                    IsCreateNewLabTest = true,
                    IsCreateNewPatient = true,
                    IsCreateNewStaff = true,
                    IsCreateNewMedicine = true,

                    IsUpdateOrEditLabTest = true,
                    IsUpdateOrEditPatient = true,
                    IsUpdateOrEditStaff = true,
                    IsUpdateOrEditMedicine = true,
                    AccessConfigurations = true,
                };

                var result = await userManager.CreateAsync(defaultAdmin, password);

                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create default admin: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

        public async Task<StaffDto> GetLoggedInUserAsync()
        {
            Staff? staff = new();
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    using var context = _contextFactory.CreateDbContext();
                    //staff = await _userManager.FindByIdAsync(userId);
                    staff = await context.Users.AsNoTracking().FirstOrDefaultAsync(x =>x.Id == userId);
                }
            }

            if (staff == null)
            {
                return new();
            }

            var currentStaff = new StaffDto
            {
                FisrtName = staff.FisrtName,
                MiddleName = staff.MiddleName,
                LastName = staff.LastName,
                StaffPhoto = staff.StaffPhoto,
                Email = staff.Email ?? string.Empty,

                ClinicalTitleName = staff.StaffTitles?.Where(x => x.StaffTypeId == 2).Select(x => x.StaffTitleName).FirstOrDefault() ?? string.Empty,
                AdministrativeTitleName = staff.StaffTitles?.Where(x => x.StaffTypeId == 1).Select(x => x.StaffTitleName).FirstOrDefault() ?? string.Empty,
                AdministrativeGroupId = staff.StaffTitles?.Where(x => x.StaffTypeId == 1).Select(x => x.TitleGroupId).FirstOrDefault() ?? 0,
                ClinicalGroupId = staff.StaffTitles?.Where(x => x.StaffTypeId == 2).Select(x => x.TitleGroupId).FirstOrDefault() ?? 0,

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

                CanChooseStaffOfTheWeek = staff.CanChooseStaffOfTheWeek,

                IsCreateNewLabTest = staff.IsCreateNewLabTest,
                IsCreateNewPatient = staff.IsCreateNewPatient,
                IsCreateNewStaff = staff.IsCreateNewStaff,
                IsCreateNewMedicine = staff.IsCreateNewMedicine,

                IsUpdateOrEditLabTest = staff.IsUpdateOrEditLabTest,
                IsUpdateOrEditPatient = staff.IsUpdateOrEditPatient,
                IsUpdateOrEditStaff = staff.IsUpdateOrEditStaff,
                AccessConfigurations = staff.AccessConfigurations,
                IsUpdateOrEditMedicine = staff.IsUpdateOrEditMedicine
            };

            return currentStaff;
        }

        public async Task<string> FindStaffValidEmailAddressAsync(string email)
        {
            var staff = await _userManager.FindByEmailAsync(email);
            if (staff == null)
            {
                return string.Empty;
            }


            return staff.Email ?? string.Empty;
        }

        public async Task<string> SendNewPasswordAsync(string email)
        {
            var staff = await _userManager.FindByEmailAsync(email);
            if (staff == null)
            {
                return string.Empty;
            }

            string newPasswordToReturn = string.Empty;
            string randomStringPart = Guid.NewGuid().ToString().Substring(0, 4);
            Random rand = new Random();
            string randomNumberPart = rand.Next(100, 1000).ToString();
            string newPassword = $"PSW@{randomStringPart}_{randomNumberPart}";

            var token = await _userManager.GeneratePasswordResetTokenAsync(staff);
            var result = await _userManager.ResetPasswordAsync(staff, token, newPassword);

            if (result.Succeeded)
                newPasswordToReturn = newPassword;

            return newPasswordToReturn;
        }
    }
}
