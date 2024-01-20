using System.Globalization;
using System.Text.Json.Serialization;

namespace NF.Models
{
    public class NotaFiscal
    {
        public int Id { get; set; }
        public int NumeroNota { get; set; }
        public float ValorTotal { get; set; }
        public Cliente Cliente { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public ICollection<NotaFiscalProduto> NotaFiscalProdutos { get; set; } = new List<NotaFiscalProduto>();

    }
}
