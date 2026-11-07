using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovaCare.Modals.DoctorModals
{
    public class DoctorType
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string DoctorTypeCode { get; set; } = string.Empty;
        public string DoctorTypeName { get; set; } = string.Empty;

        //navigation properties
        public List<Doctor>? Doctors {  get; set; }
    }
}
