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
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        public ClienteController(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Cliente>))]
        public IActionResult GetClientes()
        {
            var clientes = _mapper.Map<List<ClienteDto>>(_clienteRepository.GetClientes());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(clientes);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(Cliente))]
        [ProducesResponseType(400)]
        public IActionResult GetCliente(int Id)
        {
            if (!_clienteRepository.ClienteExiste(Id))
                return NotFound();

            var cliente = _mapper.Map<ClienteDto>(_clienteRepository.GetCliente(Id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cliente);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCliente([FromBody] ClienteDto clienteCreate)
        {
            if (clienteCreate == null)
                return BadRequest(ModelState);

            var cliente = _clienteRepository.GetClientes()
                .Where(c => c.Nome.Trim().ToUpper() == clienteCreate.Nome.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (cliente != null)
            {
                ModelState.AddModelError("", "Cliente já existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clienteMap = _mapper.Map<Cliente>(clienteCreate);

            if (!_clienteRepository.CreateCliente(clienteMap))
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

        public IActionResult UpdateCliente(int Id, [FromBody]ClienteDto updatedCliente)
        {
            if(updatedCliente == null) 
                return BadRequest(ModelState);   

            if(Id != updatedCliente.Id)
                return BadRequest(ModelState);

            if(!_clienteRepository.ClienteExiste(Id))
                return NotFound();

            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var clienteMap = _mapper.Map<Cliente>(updatedCliente);

            if(!_clienteRepository.UpdateCliente(clienteMap))
            {
                ModelState.AddModelError("", "Algo deu errado atualizando o cliente");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteCliente(int Id)
        {
            if (!_clienteRepository.ClienteExiste(Id))
            {
                return NotFound();
            }

            var clienteToDelete = _clienteRepository.GetCliente(Id);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_clienteRepository.DeleteCliente(clienteToDelete))
            {
                ModelState.AddModelError("", "Algo deu errado ao tentar esse cliente");
            }

            return NoContent();
        }
    }
}
