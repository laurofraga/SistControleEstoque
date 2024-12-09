using System.ComponentModel.DataAnnotations;

namespace SistControleEstoque.Models
{
    public class Movimentacao
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O produto é obrigatório.")]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "O funcionário responsável é obrigatório.")]
        public int FuncionarioId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        public int Quantidade { get; set; }

        [Display(Name = "Tipo de Movimentação")]
        [Required(ErrorMessage = "O tipo de movimentação é obrigatório.")]
        public TipoMovimentacao Tipo { get; set; }

        [Display(Name = "Data da Movimentação")]
        [DataType(DataType.Date, ErrorMessage = "Formato de data inválido.")]
        [Required(ErrorMessage = "A data da movimentação é obrigatória.")]
        public DateTime Data { get; set; }

        public Funcionario Funcionario { get; set; }
        public Produto Produto { get; set; }
        public enum TipoMovimentacao
        {
            Entrada =1 ,
            Saida = 2
        }
    }
}
