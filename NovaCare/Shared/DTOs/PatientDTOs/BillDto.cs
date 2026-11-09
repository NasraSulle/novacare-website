using NovaCare.Shared.DTOs.PaymentDTOs;
using System;
using System.Collections.Generic;

namespace NovaCare.Shared.DTOs.PatientDTOs
{
    public class BillDto
    {
        // Properties
        public int Id { get; set; }
        public DateTime BillDate { get; set; }
        public string BillDescription { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; } = 0m;

        // Foreign keys
        public int VisitId { get; set; }
        public int BillTypeId { get; set; }

        // Navigation properties 
        public VisitDto? Visit { get; set; }
        public List<PaymentDto>? Payments { get; set; } 

        // UI Helpers
        public string BillTypeName { get; set; } = string.Empty; 
        public string PatientFullName { get; set; } = string.Empty; 
    }
}
