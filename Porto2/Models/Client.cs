using System.ComponentModel.DataAnnotations;

namespace Porto.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "O nome deve conter apenas letras e espaços.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        public string? CNPJ { get; set; }

        public bool Enabled { get; set; }
    }
}
