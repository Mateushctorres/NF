using NF.Models;

namespace NF.Interfaces
{
    public interface IProdutoRepository
    {
        ICollection<Produto> GetProdutos();
        Produto GetProduto(int Id);
        bool ProdutoExiste(int Id);
        bool CreateProduto(Produto produto);
        bool UpdateProduto(Produto produto);
        bool DeleteProduto(Produto produto);
        bool Save();
    }
}
