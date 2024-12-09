using System.ComponentModel.DataAnnotations;

namespace SistControleEstoque.Models
{
    public class Produto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Display(Name = "Nome")]
        [StringLength(50)]
        public string Nome { get; set; }

        [Display (Name = "Descrição")]
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        public int Quantidade { get; set; }

        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "O fornecedor é obrigatório.")]
        public int FornecedorId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O estoque mínimo deve ser maior ou igual a zero.")]
        public int EstoqueMinimo { get; set; }

        public Categoria Categoria { get; set; }
        public Fornecedor Fornecedor { get; set; }

        public List<Movimentacao> Movimentacoes { get; set; } = new List<Movimentacao>();
    }
}
