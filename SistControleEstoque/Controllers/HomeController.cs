using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistControleEstoque.Data;
using SistControleEstoque.Models;
using System.Diagnostics;

namespace SistControleEstoque.Controllers
{
    public class HomeController : Controller
    {
        private readonly SistControleEstoqueContext _context;

        public HomeController(SistControleEstoqueContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var produtosComEstoqueBaixo = await _context.Produto
                .Where(p => p.Quantidade <=p.EstoqueMinimo).ToListAsync();
            
            var MovimentacoesRecentes = await _context.Movimentacao
                .Include(m => m.Produto)
                .Include(m => m.Funcionario)
                .OrderByDescending(m => m.Data).Take(5).ToListAsync();

            var produtos = await _context.Produto
               .Include(p => p.Categoria) 
               .ToListAsync();

            var ProdutosPorCategoria = produtos
               .GroupBy(p => p.Categoria?.Nome ?? "Sem Categoria")
               .ToDictionary(g => g.Key, g => g.ToList());

            var viewModel = new ViewModels.HomeViewModel
            {
                ProdutosComEstoqueBaixo = produtosComEstoqueBaixo,
                MovimentacoesRecentes = MovimentacoesRecentes,
                ProdutosPorCategoria = ProdutosPorCategoria
            };


            return View(viewModel);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
