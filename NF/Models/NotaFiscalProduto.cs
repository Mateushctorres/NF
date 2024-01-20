namespace NF.Models
{
    public class NotaFiscalProduto

    {
        public int NotaFiscalId { get; set; }
        public int ProdutoId { get; set; }
        public NotaFiscal NotaFiscal { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }
}
