using NF.Models;

namespace NF.Dto
{
    public class NotaFiscalDto
    {
        public int Id { get; set; }
        public int NumeroNota { get; set; }
        public float ValorTotal { get; set; }
        public Cliente Cliente { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}
