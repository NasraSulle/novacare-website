using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class PatientStatusDto
    {
        //properties
        public int Id { get; set; }
        public string PatientStatusName { get; set; } = string.Empty;

        //navigation properties
        public List<VisitDto>? Visits { get; set; }

    }
}
