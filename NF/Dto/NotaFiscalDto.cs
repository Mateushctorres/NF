using NF.Models;

namespace NF.Dto
{
    public class NotaFiscalDto
    {
        public int Id { get; set; }
        public int NumeroNota { get; set; }
        public int ClienteId { get; set; }
        public int FornecedorId { get; set; }
        public List<ProdutoQuantidadeDto> Produtos { get; set; }

    }
    public class ProdutoQuantidadeDto
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
