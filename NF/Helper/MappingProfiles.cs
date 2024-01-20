using AutoMapper;
using NF.Dto;
using NF.Models;

namespace NF.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<NotaFiscal, NotaFiscalDto>();
            CreateMap<NotaFiscalDto, NotaFiscal>();
            CreateMap<Cliente, ClienteDto>();
            CreateMap<ClienteDto, Cliente>();
            CreateMap<Fornecedor, FornecedorDto>();
            CreateMap<FornecedorDto, Fornecedor>();
            CreateMap<Produto, ProdutoDto>();
            CreateMap<ProdutoDto, Produto>();
            CreateMap<NotaFiscalProduto, NotaFiscalProdutoDto>();
            CreateMap<NotaFiscalProdutoDto, NotaFiscalProduto>();
        }
    }
}
