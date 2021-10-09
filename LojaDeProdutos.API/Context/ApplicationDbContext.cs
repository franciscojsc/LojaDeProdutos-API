using LojaDeProdutos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaDeProdutos.API.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Produto> Produtos { get; set; }

        public virtual void SetModifield(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}
