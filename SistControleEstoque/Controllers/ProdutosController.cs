﻿using System;
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
    public class ProdutosController : Controller
    {
        private readonly SistControleEstoqueContext _context;

        public ProdutosController(SistControleEstoqueContext context)
        {
            _context = context;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            var sistControleEstoqueContext = _context.Produto.Include(p => p.Categoria).Include(p => p.Fornecedor);
            return View(await sistControleEstoqueContext.ToListAsync());
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produto == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .Include(p => p.Categoria)
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Nome");
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedor, "Id", "Nome");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,Quantidade,Preco,CategoriaId,FornecedorId,EstoqueMinimo ")] Produto produto)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ////ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Id", produto.CategoriaId);
            ////ViewData["FornecedorId"] = new SelectList(_context.Fornecedor, "Id", "Id", produto.FornecedorId);
            ////return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produto == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Nome", produto.CategoriaId);
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedor, "Id", "Nome", produto.FornecedorId);
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Quantidade,Preco,CategoriaId,FornecedorId,EstoqueMinimo")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
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
            //ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Id", produto.CategoriaId);
            //ViewData["FornecedorId"] = new SelectList(_context.Fornecedor, "Id", "Id", produto.FornecedorId);
            //return View(produto);
        

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produto == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .Include(p => p.Categoria)
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produto == null)
            {
                return Problem("O conjunto de entidades 'SistControleEstoqueContext.Produtos' está nulo.");
            }

            // Verificar se o produto está vinculado a alguma movimentação
            var movimentacoesVinculadas = await _context.Movimentacao.AnyAsync(m => m.ProdutoId == id);
            if (movimentacoesVinculadas)
            {
                // Exibir mensagem de erro ou redirecionar para uma página informando a impossibilidade de exclusão
                TempData["Erro"] = "Este produto não pode ser excluído porque está vinculado a uma ou mais movimentações.";
                return RedirectToAction(nameof(Index));
            }

            var produto = await _context.Produto.FindAsync(id);
            if (produto != null)
            {
                _context.Produto.Remove(produto);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        private bool ProdutoExists(int id)
        {
          return (_context.Produto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
