using NF.Models;

namespace NF.Interfaces
{
    public interface INotaFiscalRepository
    {
        ICollection<NotaFiscal> GetNotasFiscais();

        NotaFiscal GetNotaFiscal(int id);
        bool NotaFiscalExiste(int id);
        bool CreateNotaFiscal(NotaFiscal notaFiscal);
        bool DeleteNotaFiscal(NotaFiscal notaFiscal);
        bool Save();

    }
}
