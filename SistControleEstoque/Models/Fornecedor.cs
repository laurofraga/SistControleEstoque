using System.ComponentModel.DataAnnotations;

namespace SistControleEstoque.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do fornecedor é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [RegularExpression(@"\d{14}", ErrorMessage = "O CNPJ deve conter exatamente 14 dígitos.")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "O contato é obrigatório.")]
        [StringLength(20, ErrorMessage = "O contato deve ter no máximo 20 caracteres.")]
        public string Contato { get; set; }

        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "O endereço é obrigatório.")]
        [StringLength(100, ErrorMessage = "O endereço deve ter no máximo 100 caracteres.")]
        public string Endereco { get; set; }    

        public List<Produto> Produtos { get; set; } = new List<Produto>();

    }
}
