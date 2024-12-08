namespace SistControleEstoque.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }

        public List<Categoria> Categorias { get; set; } = new List<Categoria>();
        public List<Movimentacao> Movimentacao { get; set; } = new List<Movimentacao>();
    }
}
