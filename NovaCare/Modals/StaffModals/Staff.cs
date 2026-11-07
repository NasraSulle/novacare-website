using Microsoft.AspNetCore.Identity;
using NovaCare.Modals.DoctorModals;
using NovaCare.Modals.JoinTables;
using NovaCare.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NovaCare.Modals.StaffModals
{
    public class Staff : IdentityUser
    {
        //properties
        public string FisrtName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public byte[]? StaffPhoto { get; set; }
        public int Age { get; set; }
        public string Address { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
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
        [ForeignKey(nameof(StaffStatus))]
        public int StaffStatusId { get; set; }

        //navigation properties
        public StaffStatus? StaffStatus { get; set; }
        public List<StaffTitle>? StaffTitles { get; set; }
        public Doctor? Doctor { get; set; }

    }
}
