using NF.Models;

namespace NF.Interfaces
{
    public interface INotaFiscalRepository
    {
        ICollection<NotaFiscal> GetNotasFiscais();

        NotaFiscal GetNotaFiscal(int id);
        bool NotaFiscalExiste(int id);
        bool CreateNotaFiscal(int id, NotaFiscal notaFiscal);
        bool UpdateNotaFiscal(int id, NotaFiscal notaFiscal);
        bool DeleteNotaFiscal(NotaFiscal notaFiscal);
        bool Save();

    }
}
