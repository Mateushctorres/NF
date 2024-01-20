using NF.Models;

namespace NF.Interfaces
{
    public interface IFornecedorRepository
    {
        ICollection<Fornecedor> GetFornecedores();
        Fornecedor GetFornecedor(int id);
        bool FornecedorExiste(int id);
        bool CreateFornecedor(Fornecedor fornecedor);
        bool UpdateFornecedor(Fornecedor fornecedor);
        bool DeleteFornecedor(Fornecedor fornecedor);
        bool Save();
    }
}
