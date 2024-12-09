using System.ComponentModel.DataAnnotations;

namespace SistControleEstoque.Models
{
    public class Funcionario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 caracteres.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter apenas números.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail fornecido não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O cargo é obrigatório.")]
        public string Cargo { get; set; }

        public List<Categoria> Categorias { get; set; } = new List<Categoria>();
        public List<Movimentacao> Movimentacao { get; set; } = new List<Movimentacao>();
    }
}
