using System.ComponentModel.DataAnnotations.Schema;

namespace NovaCare.Modals.LabTestModals
{
    public class LabTestResultComposition
    {
        public int Id { get; set; }
        public string TestComposiiton { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;


        [ForeignKey(nameof(LabTest))]
        public int LabTestId { get; set; }

        public LabTest? LabTest { get; set; }
    }
}
