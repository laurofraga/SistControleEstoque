using System.ComponentModel.DataAnnotations;

namespace SistControleEstoque.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        public string Nome { get; set; }
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "A descrição da categoria é obrigatória.")]
        public string Descricao { get; set; }

        [Display(Name = "Funcionário Responsável")]
        [Required(ErrorMessage = "O funcionário responsável é obrigatório.")]
        public int? FuncionarioResponsavelId { get; set; }

        public Funcionario FuncionarioResponsavel { get; set; }

        public List<Produto> Produtos { get; set; } = new List<Produto>();

    }
}
