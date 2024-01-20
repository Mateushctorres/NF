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
            return _context.NotasFiscais
                    .Include(n => n.Cliente)
                    .Include(n => n.Fornecedor)
                    .Include(n => n.NotaFiscalProdutos)
                    .ThenInclude(nfp => nfp.Produto)
                    .Where(n => n.Id == id)
                    .FirstOrDefault();
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

        public bool CreateNotaFiscal(NotaFiscal notaFiscal)
        {

            _context.Add(notaFiscal);
            return Save();

        }

        public bool Save()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Handle the exception or log it
                    Console.WriteLine($"Error creating NotaFiscal: {ex.Message}");
                    return false;
                }
            }
        }

        public bool DeleteNotaFiscal(NotaFiscal notaFiscal)
        {
            _context.Remove(notaFiscal);
            return Save();
        }
    }
}
