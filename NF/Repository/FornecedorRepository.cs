using AutoMapper;
using NF.Data;
using NF.Interfaces;
using NF.Models;

namespace NF.Repository
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FornecedorRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool FornecedorExiste(int id)
        {
            return _context.Fornecedores.Any(x => x.Id == id);
        }

        public ICollection<Fornecedor> GetFornecedores()
        {
            return _context.Fornecedores.ToList();
        }

        public Fornecedor GetFornecedor(int id)
        {
            return _context.Fornecedores.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool CreateFornecedor(Fornecedor fornecedor)
        {
            _context.Add(fornecedor);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateFornecedor(Fornecedor fornecedor)
        {
            _context.Update(fornecedor);
            return Save();
        }

        public bool DeleteFornecedor(Fornecedor fornecedor)
        {
            _context.Remove(fornecedor);
            return Save();
        }
    }
}
