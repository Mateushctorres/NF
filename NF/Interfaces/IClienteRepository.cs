using NF.Models;

namespace NF.Interfaces
{
    public interface IClienteRepository
    {
        ICollection<Cliente> GetClientes();
        Cliente GetCliente(int id);
        bool ClienteExiste(int id);
        bool CreateCliente(Cliente cliente);
        bool UpdateCliente(Cliente cliente);
        bool DeleteCliente(Cliente cliente);
        bool Save();
    }
}
