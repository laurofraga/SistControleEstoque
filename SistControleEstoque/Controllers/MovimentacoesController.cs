using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistControleEstoque.Data;
using SistControleEstoque.Models;

namespace SistControleEstoque.Controllers
{
    public class MovimentacoesController : Controller
    {
        private readonly SistControleEstoqueContext _context;

        public MovimentacoesController(SistControleEstoqueContext context)
        {
            _context = context;
        }

        // GET: Movimentacoes
        public async Task<IActionResult> Index()
        {
            var sistControleEstoqueContext = _context.Movimentacao.Include(m => m.Funcionario).Include(m => m.Produto);
            return View(await sistControleEstoqueContext.ToListAsync());
        }

        // GET: Movimentacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movimentacao == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacao
                .Include(m => m.Funcionario)
                .Include(m => m.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimentacao == null)
            {
                return NotFound();
            }

            return View(movimentacao);
        }

        // GET: Movimentacoes/Create
        public IActionResult Create()
        {
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "Id", "Id");
            ViewData["ProdutoId"] = new SelectList(_context.Set<Produto>(), "Id", "Id");
            return View();
        }

        // POST: Movimentacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProdutoId,FuncionarioId,Quantidade,Tipo,Data")] Movimentacao movimentacao)
        {
            var produto = await _context.Produto.FindAsync(movimentacao.ProdutoId);
            if (produto == null)
            {
                ModelState.AddModelError("", "Produto não encontrado.");
                ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "Id", "Id", movimentacao.FuncionarioId);
                ViewData["ProdutoId"] = new SelectList(_context.Produto, "Id", "Id", movimentacao.ProdutoId);
                return View(movimentacao);
            }

            // Ajusta a quantidade do produto de acordo com o tipo de movimentação
            if (movimentacao.Tipo == Movimentacao.TipoMovimentacao.Entrada)
            {
                produto.Quantidade += movimentacao.Quantidade;
            }
            else if (movimentacao.Tipo == Movimentacao.TipoMovimentacao.Saida)
            {
                if (produto.Quantidade < movimentacao.Quantidade)
                {
                    ModelState.AddModelError("", "Quantidade insuficiente em estoque.");
                    ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "Id", "Id", movimentacao.FuncionarioId);
                    ViewData["ProdutoId"] = new SelectList(_context.Produto, "Id", "Id", movimentacao.ProdutoId);
                    return View(movimentacao);
                }
                produto.Quantidade -= movimentacao.Quantidade;
            }

            try
            {
                // Atualiza o produto no banco de dados
                _context.Update(produto);

                // Adiciona a movimentação ao banco de dados
                _context.Add(movimentacao);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao salvar os dados: {ex.Message}");
            }

            // Recarrega as listas para a view em caso de erro
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "Id", "Id", movimentacao.FuncionarioId);
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "Id", "Id", movimentacao.ProdutoId);
            return View(movimentacao);
        }

        // GET: Movimentacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movimentacao == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacao.FindAsync(id);
            if (movimentacao == null)
            {
                return NotFound();
            }
           
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "Id", "Id", movimentacao.FuncionarioId);
            ViewData["ProdutoId"] = new SelectList(_context.Set<Produto>(), "Id", "Id", movimentacao.ProdutoId);
            return View(movimentacao);
        }

        // POST: Movimentacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProdutoId,FuncionarioId,Quantidade,Tipo,Data")] Movimentacao movimentacao)
        {
            if (id != movimentacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimentacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentacaoExists(movimentacao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "Id", "Id", movimentacao.FuncionarioId);
            ViewData["ProdutoId"] = new SelectList(_context.Set<Produto>(), "Id", "Id", movimentacao.ProdutoId);
            return View(movimentacao);
        }

        // GET: Movimentacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movimentacao == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacao
                .Include(m => m.Funcionario)
                .Include(m => m.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimentacao == null)
            {
                return NotFound();
            }

            return View(movimentacao);
        }

        // POST: Movimentacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movimentacao == null)
            {
                return Problem("Entity set 'SistControleEstoqueContext.Movimentacao'  is null.");
            }
            var movimentacao = await _context.Movimentacao.FindAsync(id);
            if (movimentacao != null)
            {
                _context.Movimentacao.Remove(movimentacao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentacaoExists(int id)
        {
          return (_context.Movimentacao?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
