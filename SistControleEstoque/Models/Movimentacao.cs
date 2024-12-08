namespace SistControleEstoque.Models
{
    public class Movimentacao
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int FuncionarioId { get; set; }
        public int Quantidade { get; set; }
        public TipoMovimentacao Tipo { get; set; }
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
