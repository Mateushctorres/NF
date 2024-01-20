using NF.Models;

namespace NF.Dto
{
    public class NotaFiscalProdutoDto
    {
        public int NotaFiscalId { get; set; }
        public int ProdutoId { get; set; }
        public NotaFiscal NotaFiscal { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }

}
