using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NF.Dto;
using NF.Interfaces;
using NF.Models;
using NF.Repository;

namespace NF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Produto>))]
        public IActionResult GetProdutos()
        {
            var produtos = _mapper.Map<List<ProdutoDto>>(_produtoRepository.GetProdutos());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(produtos);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(Produto))]
        [ProducesResponseType(400)]
        public IActionResult GetProduto(int Id)
        {
            if (!_produtoRepository.ProdutoExiste(Id))
                return NotFound();

            var produto = _mapper.Map<ProdutoDto>(_produtoRepository.GetProduto(Id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(produto);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProduto([FromBody] ProdutoDto produtoCreate)
        {
            if (produtoCreate == null)
                return BadRequest(ModelState);

            var produto = _produtoRepository.GetProdutos()
                .Where(c => c.Descricao.Trim().ToUpper() == produtoCreate.Descricao.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (produto != null)
            {
                ModelState.AddModelError("", "Produto já existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var produtoMap = _mapper.Map<Produto>(produtoCreate);

            if (!_produtoRepository.CreateProduto(produtoMap))
            {
                ModelState.AddModelError("", "Algo deu errado ao tentar salvar");
                return StatusCode(500, ModelState);
            }

            return Ok("Criado com sucesso");
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateProduto(int Id, [FromBody] ProdutoDto updatedProduto)
        {
            if (updatedProduto == null)
                return BadRequest(ModelState);

            if (Id != updatedProduto.Id)
                return BadRequest(ModelState);

            if (!_produtoRepository.ProdutoExiste(Id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var produtoMap = _mapper.Map<Produto>(updatedProduto);

            if (!_produtoRepository.UpdateProduto(produtoMap))
            {
                ModelState.AddModelError("", "Algo deu errado atualizando o produto");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteProduto(int Id)
        {
            if (!_produtoRepository.ProdutoExiste(Id))
            {
                return NotFound();
            }

            var produtoToDelete = _produtoRepository.GetProduto(Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_produtoRepository.DeleteProduto(produtoToDelete))
            {
                ModelState.AddModelError("", "Algo deu errado ao tentar esse produto");
            }

            return NoContent();
        }
    }
}
