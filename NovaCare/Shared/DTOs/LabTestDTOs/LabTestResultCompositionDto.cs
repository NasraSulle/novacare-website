using NovaCare.Modals.LabTestModals;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Shared.DTOs.LabTestDTOs
{
    public class LabTestResultCompositionDto
    {
        public int Id { get; set; }
        public string TestComposiiton { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;


        public int LabTestId { get; set; }

        public LabTest? LabTest { get; set; }
    }
}
