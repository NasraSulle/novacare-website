using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.StaffDTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;

        public byte[]? StaffPhoto { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int StaffTitleId { get; set; }

        //for permissions
        public bool IsViewAllPatients { get; set; }
        public bool IsViewAllDoctors { get; set; }
        public bool IsViewAllStaffs { get; set; }
        public bool IsViewAllHelps { get; set; }
        public bool IsViewAllLeaders { get; set; }
        public bool IsViewAllMedicines { get; set; }
        public bool IsViewAllRooms { get; set; }

        public bool IsViewPatientById { get; set; }
        public bool IsViewDoctorById { get; set; }
        public bool IsViewStaffById { get; set; }
        public bool IsViewHelpById { get; set; }
        public bool IsViewLeaderById { get; set; }
        public bool IsViewMedicineById { get; set; }
        public bool IsViewRoomById { get; set; }

        public bool IsCreateNewPatient { get; set; }
        public bool IsCreateNewStaff { get; set; }
        public bool IsCreateNewMedicine { get; set; }

        public bool IsUpdateOrEditPatient { get; set; }
        public bool IsUpdateOrEditStaff { get; set; }
        public bool IsUpdateOrEditMedicine { get; set; }
    }
}
