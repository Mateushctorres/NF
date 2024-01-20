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
    public class FornecedorController : Controller
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public FornecedorController(IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Fornecedor>))]
        public IActionResult GetFornecedores()
        {
            var fornecedores = _mapper.Map<List<FornecedorDto>>(_fornecedorRepository.GetFornecedores());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(fornecedores);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(Fornecedor))]
        [ProducesResponseType(400)]
        public IActionResult GetFornecedor(int Id)
        {
            if (!_fornecedorRepository.FornecedorExiste(Id))
                return NotFound();

            var fornecedor = _mapper.Map<FornecedorDto>(_fornecedorRepository.GetFornecedor(Id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(fornecedor);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateFornecedor([FromBody] FornecedorDto fornecedorCreate)
        {
            if (fornecedorCreate == null)
                return BadRequest(ModelState);

            var fornecedor = _fornecedorRepository.GetFornecedores()
                .Where(c => c.Nome.Trim().ToUpper() == fornecedorCreate.Nome.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (fornecedor != null)
            {
                ModelState.AddModelError("", "Fornecedor já existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var fornecedorMap = _mapper.Map<Fornecedor>(fornecedorCreate);

            if (!_fornecedorRepository.CreateFornecedor(fornecedorMap))
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

        public IActionResult UpdateFornecedor(int Id, [FromBody] FornecedorDto updatedFornecedor)
        {
            if (updatedFornecedor == null)
                return BadRequest(ModelState);

            if (Id != updatedFornecedor.Id)
                return BadRequest(ModelState);

            if (!_fornecedorRepository.FornecedorExiste(Id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var fornecedorMap = _mapper.Map<Fornecedor>(updatedFornecedor);

            if (!_fornecedorRepository.UpdateFornecedor(fornecedorMap))
            {
                ModelState.AddModelError("", "Algo deu errado atualizando o fornecedor");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteFornecedor(int Id)
        {
            if (!_fornecedorRepository.FornecedorExiste(Id))
            {
                return NotFound();
            }

            var fornecedorToDelete = _fornecedorRepository.GetFornecedor(Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_fornecedorRepository.DeleteFornecedor(fornecedorToDelete))
            {
                ModelState.AddModelError("", "Algo deu errado ao tentar esse fornecedor");
            }

            return NoContent();
        }
    }
}
