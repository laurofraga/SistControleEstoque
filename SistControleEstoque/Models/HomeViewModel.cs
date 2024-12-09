using System.Collections.Generic;
using SistControleEstoque.Models;

namespace SistControleEstoque.ViewModels

{
    public class HomeViewModel
    {
        public List<Produto> ProdutosComEstoqueBaixo { get; set; }
        public List<Movimentacao> MovimentacoesRecentes { get; set; }
        public Dictionary<string, List<Produto>> ProdutosPorCategoria { get; set; }
    }
}
