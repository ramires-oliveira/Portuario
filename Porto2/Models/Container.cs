using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Porto.Models
{
    public enum Status
    {
        [Description("Cheio")]
        Cheio = 1,
        [Description("Vazio")]
        Vazio = 2,
    }

    public enum Category
    {
        [Description("Importação")]
        Importacao = 1,
        [Description("Exportação")]
        Exportacao = 2,
    }

    public class Container
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        public virtual Client? Client { get; set; }

        [Required(ErrorMessage = "O número do container é obrigatório.")]
        public string? Number { get; set; }

        public string? Type { get; set; }

        public string? Category { get; set; }
        
        public string? Status { get; set; }
        
        public bool Enabled { get; set; }
    }
}
