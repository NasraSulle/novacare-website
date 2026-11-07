using NovaCare.Shared.DTOs.DoctorDTOs;
using NovaCare.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text;

namespace NovaCare.Shared.DTOs.StaffDTOs
{
    public class StaffDto
    {
        public string FisrtName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public byte[]? StaffPhoto { get; set; }
        public int Age { get; set; }
        public string EmployeeId { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime RegisteredDate { get; set; }
        public Gender Gender { get; set; }

        //mapping bools
        public bool IsGetStaffModules { get; set; }
        public bool IsViewAllPatients { get; set; }
        public bool IsViewAllDoctors { get; set; }
        public bool IsViewAllStaffs { get; set; }
        public bool IsViewAllHelps { get; set; }
        public bool IsViewAllLeaders { get; set; }
        public bool IsViewAllMedicines { get; set; }
        public bool IsViewAllRooms { get; set; }
        public bool IsViewAllNurses { get; set; }
        public bool IsViewAllAdmins { get; set; }
        public bool IsViewAllLabTests { get; set; }

        public bool IsViewLabTestById { get; set; }
        public bool IsViewPatientById { get; set; }
        public bool IsViewDoctorById { get; set; }
        public bool IsViewStaffById { get; set; }
        public bool IsViewHelpById { get; set; }
        public bool IsViewLeaderById { get; set; }
        public bool IsViewMedicineById { get; set; }
        public bool IsViewRoomById { get; set; }

        public bool IsCreateNewLabTest { get; set; }
        public bool IsCreateNewPatient { get; set; }
        public bool IsCreateNewStaff { get; set; }
        public bool IsCreateNewMedicine { get; set; }
        public bool AccessConfigurations { get; set; }

        public bool IsUpdateOrEditLabTest { get; set; }
        public bool IsUpdateOrEditPatient { get; set; }
        public bool IsUpdateOrEditStaff { get; set; }
        public bool IsUpdateOrEditMedicine { get; set; }
        public bool IsStaffOfTTheWeek { get; set; }
        public bool CanChooseStaffOfTheWeek { get; set; }


        //foreign keys
        public int StaffStatusId { get; set; }
        public int ClinicalTitleId { get; set; }
        public int AdministrativeTitleId { get; set; }
        public int AdministrativeGroupId { get; set; }
        public int ClinicalGroupId { get; set; }

        //navigation properties
        public StaffStatusDto? StaffStatus { get; set; }
        public List<StaffTitleDto>? StaffTitles { get; set; }
        public DoctorDto? Doctor { get; set; }

        //addons
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;


        public string StaffStatusName { get; set; } = string.Empty;
        public string ClinicalTitleName { get; set; } = string.Empty;
        public string AdministrativeTitleName { get; set; } = string.Empty;
        public string AdministrativeTitleGroupName { get; set; } = string.Empty;
        public string ClinicalTitleGroupName { get; set; } = string.Empty;

    }
}
