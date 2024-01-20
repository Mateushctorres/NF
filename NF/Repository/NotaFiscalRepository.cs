using Microsoft.EntityFrameworkCore;
using NF.Data;
using NF.Interfaces;
using NF.Models;
using System.Text.Json;

namespace NF.Repository
{
    public class NotaFiscalRepository : INotaFiscalRepository
    {
        private readonly AppDbContext _context;

        public NotaFiscalRepository(AppDbContext context)
        {
            _context = context;
        }

        public NotaFiscal GetNotaFiscal(int id)
        {
            return _context.NotasFiscais.Where(n => n.Id == id).FirstOrDefault();
        }

        public bool NotaFiscalExiste(int id)
        {
            return _context.NotasFiscais.Any(n => n.Id == id);
        }

        public ICollection<NotaFiscal> GetNotasFiscais()
        {
            return _context.NotasFiscais
                .Include(n => n.Cliente)
                .Include(n => n.Fornecedor)
                .Include(n => n.NotaFiscalProdutos)
                    .ThenInclude(n => n.Produto)
                .OrderBy(p => p.Id)
                .ToList();
        }

        public bool CreateNotaFiscal(int id, NotaFiscal notaFiscal)
        {
            var notaFiscalProdutoEntity = _context.Produtos.Where(x => x.Id == id).FirstOrDefault();

            var notaFiscalProduto = new NotaFiscalProduto()
            {
                Produto = notaFiscalProdutoEntity,
                NotaFiscal = notaFiscal,
            };

            _context.Add(notaFiscalProduto);

            _context.Add(notaFiscal);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateNotaFiscal(int id, NotaFiscal notaFiscal)
        {
            _context.Update(notaFiscal);
            return Save();
        }

        public bool DeleteNotaFiscal(NotaFiscal notaFiscal)
        {
            _context.Remove(notaFiscal);
            return Save();
        }
    }
}
