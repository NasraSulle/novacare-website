using NovaCare.Modals.JoinTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovaCare.Modals.PaymentModals
{
    public class Insuarance
    {
        //properties
        [Key]
        public int Id { get; set; }
        public string InsuaranceName { get; set; } = string.Empty;
        public string InsuranceNumber { get; set; } = string.Empty;
        public int PrivillageCodeNumber { get; set; }

        //navigation properties
        public List<PatientInsuarance>? PatientInsuarances { get; set; }
    }
}
