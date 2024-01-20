using Microsoft.EntityFrameworkCore;
using NF.Models;

namespace NF.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<NotaFiscal> NotasFiscais { get; set; }
        public DbSet<NotaFiscalProduto> NotaFiscalProdutos { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotaFiscalProduto>()
                .HasKey(pc => new { pc.NotaFiscalId, pc.ProdutoId });
            modelBuilder.Entity<NotaFiscalProduto>()
                .HasOne(p => p.NotaFiscal)
                .WithMany(pc => pc.NotaFiscalProdutos)
                .HasForeignKey(c => c.NotaFiscalId);
            modelBuilder.Entity<NotaFiscalProduto>()
                .HasOne(p => p.Produto)
                .WithMany(pc => pc.NotaFiscalProdutos)
                .HasForeignKey(c => c.ProdutoId);
        }

    }
}