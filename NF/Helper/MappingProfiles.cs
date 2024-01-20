using AutoMapper;
using NF.Dto;
using NF.Models;

namespace NF.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<NotaFiscal, NotaFiscalDto>()
                .ForMember(dest => dest.Produtos, opt => opt.MapFrom(src => src.NotaFiscalProdutos));

            CreateMap<NotaFiscalDto, NotaFiscal>();            
            CreateMap<Cliente, ClienteDto>();
            CreateMap<ClienteDto, Cliente>();
            CreateMap<Fornecedor, FornecedorDto>();
            CreateMap<FornecedorDto, Fornecedor>();
            CreateMap<Produto, ProdutoDto>();
            CreateMap<ProdutoDto, Produto>();
            CreateMap<NotaFiscalProduto, NotaFiscalProdutoDto>()
                .ForMember(dest => dest.Produto, opt => opt.MapFrom(src => src.Produto));

            CreateMap<NotaFiscalProdutoDto, NotaFiscalProduto>();
        }
    }
}
