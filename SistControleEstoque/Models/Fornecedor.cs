namespace SistControleEstoque.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string Contato { get; set; }
        public string Endereco { get; set; }    

        public List<Produto> Produtos { get; set; } = new List<Produto>();

    }
}
