using NF.Data;
using NF.Interfaces;
using NF.Models;

namespace NF.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private AppDbContext _context;
        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool ClienteExiste(int id)
        {
            return _context.Clientes.Any(c => c.Id == id);
        }

        public bool CreateCliente(Cliente cliente)
        {
            _context.Add(cliente);
            return Save();
        }

        public bool DeleteCliente(Cliente cliente)
        {
            _context.Remove(cliente);
            return Save();
        }

        public Cliente GetCliente(int id)
        {
            return _context.Clientes.Where(n => n.Id == id).FirstOrDefault();
        }

        public ICollection<Cliente> GetClientes()
        {
            return _context.Clientes.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCliente(Cliente cliente)
        {
            _context.Update(cliente);
            return Save();
        }
    }
}
