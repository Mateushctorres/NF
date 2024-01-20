using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NF.Data;
using NF.Dto;
using NF.Interfaces;
using NF.Models;
using NF.Repository;

namespace NF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaFiscalController : Controller
    {
        private readonly INotaFiscalRepository _notaFiscalRepository;
        private readonly IProdutoRepository produtoRepository;
        private readonly IMapper _mapper;
        private readonly IFornecedorRepository fornecedorRepository;
        private readonly IClienteRepository clienteRepository;

        public NotaFiscalController(INotaFiscalRepository notaFiscalRepository,
            IProdutoRepository produtoRepository,
            IFornecedorRepository fornecedorRepository,
            IClienteRepository clienteRepository,
            IMapper mapper)
        {
            _notaFiscalRepository = notaFiscalRepository;
            this.produtoRepository = produtoRepository;
            this.fornecedorRepository = fornecedorRepository;
            this.clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<NotaFiscal>))]
        public IActionResult GetNotasFiscais()
        {
            var notasFiscais = _notaFiscalRepository.GetNotasFiscais();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(notasFiscais);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(NotaFiscal))]
        [ProducesResponseType(400)]
        public IActionResult GetNotaFiscal(int Id)
        {
            if (!_notaFiscalRepository.NotaFiscalExiste(Id))
                return NotFound();

            var notaFiscal = _notaFiscalRepository.GetNotaFiscal(Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(notaFiscal);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateNotaFiscal( [FromBody] NotaFiscalDto notaFiscalCreate)
        {
            if (notaFiscalCreate == null)
                return BadRequest(ModelState);

            if (_notaFiscalRepository.NotaFiscalExiste(notaFiscalCreate.Id))
            {
                ModelState.AddModelError("", "Nota Fiscal já existe");
                return StatusCode(422, ModelState);
            }
            
            var cliente = clienteRepository.ClienteExiste(notaFiscalCreate.ClienteId);
            var fornecedor = fornecedorRepository.FornecedorExiste(notaFiscalCreate.FornecedorId);

            if (!fornecedor)
                return BadRequest("Invalid FornecedorId");

            if (!cliente)
                return BadRequest("Invalid ClienteId");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clienteNota = clienteRepository.GetCliente(notaFiscalCreate.ClienteId);            
            var fornecedorNota = fornecedorRepository.GetFornecedor(notaFiscalCreate.FornecedorId);

            var notaFiscal = new NotaFiscal
            {
                Cliente = clienteNota,
                Fornecedor = fornecedorNota,
                NumeroNota = notaFiscalCreate.NumeroNota,
            };

            var valorTotal = 0f;

            foreach (var produtoQuantidade in notaFiscalCreate.Produtos)
            {
                var produto = produtoRepository.GetProduto(produtoQuantidade.ProdutoId);

                if (produto != null)
                {
                    var nfp = new NotaFiscalProduto
                    {
                        NotaFiscal = notaFiscal,
                        Produto = produto,
                        Quantidade = produtoQuantidade.Quantidade,
                    };

                    valorTotal += produto.Preco * produtoQuantidade.Quantidade;

                    notaFiscal.NotaFiscalProdutos.Add(nfp);
                } else
                {
                    
                    ModelState.AddModelError("", "Erro ao criar a nota. Produto não existe.");
                    return StatusCode(500, ModelState);
                    
                }
            }

            notaFiscal.ValorTotal = valorTotal;


            if (!_notaFiscalRepository.CreateNotaFiscal(notaFiscal))
            {
                ModelState.AddModelError("", "Algo deu errado ao tentar salvar");
                return StatusCode(500, ModelState);
            }

            return Ok("Criado com sucesso");

        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteNotaFiscal(int Id)
        {
            if (!_notaFiscalRepository.NotaFiscalExiste(Id))
            {
                return NotFound();
            }

            var notaFiscalToDelete = _notaFiscalRepository.GetNotaFiscal(Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_notaFiscalRepository.DeleteNotaFiscal(notaFiscalToDelete))
            {
                ModelState.AddModelError("", "Algo deu errado ao tentar esse notaFiscal");
            }

            return NoContent();

        }
    }
}
