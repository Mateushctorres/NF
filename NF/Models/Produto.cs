using System.Text.Json.Serialization;

namespace NF.Models
{
    public class Produto

    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public float Preco { get; set; }

        [JsonIgnore]
        public ICollection<NotaFiscalProduto> NotaFiscalProdutos { get; set; }

    }
}
