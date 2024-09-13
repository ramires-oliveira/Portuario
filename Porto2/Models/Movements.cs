using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Porto.Models
{
    public class Movements
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Container")]
        public int ContainerId { get; set; }
        
        public virtual Container? Container { get; set; }

        public string? MovementType { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória.")]
        [DataType(DataType.DateTime, ErrorMessage = "Data de início inválida.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "A data de término é obrigatória.")]
        [DataType(DataType.DateTime, ErrorMessage = "Data de término inválida.")]
        public DateTime EndDate { get; set; }
        
        public Boolean Enabled { get; set; }

    }
}
