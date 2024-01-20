using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        public NotaFiscalController(INotaFiscalRepository notaFiscalRepository,
            IProdutoRepository produtoRepository,
            IMapper mapper)
        {
            _notaFiscalRepository = notaFiscalRepository;
            this.produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<NotaFiscal>))]
        public IActionResult GetNotasFiscais()
        {
            var notasFiscais = _mapper.Map<List<NotaFiscalDto>>(_notaFiscalRepository.GetNotasFiscais());

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

            var notaFiscal = _mapper.Map<NotaFiscalDto>(_notaFiscalRepository.GetNotaFiscal(Id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(notaFiscal);
        }

        [HttpPost("{Id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateNotaFiscal([FromRoute] int Id, [FromBody] NotaFiscalDto notaFiscalCreate)
        {
            if (notaFiscalCreate == null)
                return BadRequest(ModelState);

            if (_notaFiscalRepository.NotaFiscalExiste(Id))
            {
                ModelState.AddModelError("", "Nota Fiscal já existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var notaFiscalMap = _mapper.Map<NotaFiscal>(notaFiscalCreate);

            if (!_notaFiscalRepository.CreateNotaFiscal(Id, notaFiscalMap))
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

        public IActionResult UpdateNotaFiscal(int Id, [FromBody] NotaFiscalDto updatedNotaFiscal)
        {
            if (updatedNotaFiscal == null)
                return BadRequest(ModelState);

            if (Id != updatedNotaFiscal.Id)
                return BadRequest(ModelState);

            if (!_notaFiscalRepository.NotaFiscalExiste(Id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var notaFiscalMap = _mapper.Map<NotaFiscal>(updatedNotaFiscal);

            if (!_notaFiscalRepository.UpdateNotaFiscal(Id, notaFiscalMap))
            {
                ModelState.AddModelError("", "Algo deu errado atualizando o notaFiscal");
                return StatusCode(500, ModelState);
            }

            return NoContent();
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
