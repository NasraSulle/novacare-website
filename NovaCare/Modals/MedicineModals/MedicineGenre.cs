using NovaCare.Modals.JoinTables;
using System.ComponentModel.DataAnnotations;

namespace NovaCare.Modals.MedicineModals
{
    public class MedicineGenre
    { 
        //properties
        [Key]
        public int Id { get; set; }
        public int MedicineGenreCode { get; set; }
        public string MedicineGenreName { get; set; } = string.Empty;
        public List<MedicineType>? MedicineTypes { get; set; }

    }
}
