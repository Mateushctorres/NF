using System.Text.Json.Serialization;

namespace NF.Models
{
    public class NotaFiscalProduto

    {
        public int NotaFiscalId { get; set; }
        public int ProdutoId { get; set; }
        [JsonIgnore]
        public NotaFiscal NotaFiscal { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }
}
