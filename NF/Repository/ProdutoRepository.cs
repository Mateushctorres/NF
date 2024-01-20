using NF.Data;
using NF.Interfaces;
using NF.Models;

namespace NF.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CreateProduto(Produto produto)
        {
            _context.Add(produto);
            return Save();
        }

        public bool DeleteProduto(Produto produto)
        {
            _context.Remove(produto);
            return Save();
        }

        public Produto GetProduto(int id)
        {
            return _context.Produtos.Where(n => n.Id == id).FirstOrDefault();
        }

        public ICollection<Produto> GetProdutos()
        {
            return _context.Produtos.ToList();
        }

        public bool ProdutoExiste(int id)
        {
            return _context.Produtos.Any(c => c.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProduto(Produto produto)
        {
            _context.Update(produto);
            return Save();
        }
    }
}
