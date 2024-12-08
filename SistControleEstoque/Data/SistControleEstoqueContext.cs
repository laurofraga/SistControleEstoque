using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistControleEstoque.Models;

namespace SistControleEstoque.Data
{
    public class SistControleEstoqueContext : DbContext
    {
        public SistControleEstoqueContext (DbContextOptions<SistControleEstoqueContext> options)
            : base(options)
        {
        }

        public DbSet<SistControleEstoque.Models.Categoria> Categoria { get; set; } = default!;

        public DbSet<SistControleEstoque.Models.Fornecedor>? Fornecedor { get; set; }

        public DbSet<SistControleEstoque.Models.Funcionario>? Funcionario { get; set; }

        public DbSet<SistControleEstoque.Models.Movimentacao>? Movimentacao { get; set; }

        public DbSet<SistControleEstoque.Models.Produto>? Produto { get; set; }
    }
}
