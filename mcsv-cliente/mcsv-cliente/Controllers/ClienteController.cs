using mcsv_cliente.Models;
using mcsv_cliente.Services;
using Microsoft.AspNetCore.Mvc;

namespace mcsv_cliente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // Crear Cliente
        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> CrearCliente([FromBody] ClienteDTO clienteDTO)
        {
            var clienteCreado = await _clienteService.CrearCliente(clienteDTO);
            return CreatedAtAction(nameof(ObtenerCliente), new { id = clienteCreado.id }, clienteCreado);
        }

        // Obtener Cliente por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> ObtenerCliente(string id)
        {
            try
            {
                var cliente = await _clienteService.ObtenerClientePorId(id);
                return Ok(cliente);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { mensaje = e.Message });
            }
        }

        // Obtener Cliente por DNI
        [HttpGet("dni/{dni}")]
        public async Task<ActionResult<ClienteDTO>> ObtenerClientePorDni(string dni)
        {
            try
            {
                var cliente = await _clienteService.ObtenerClientePorDni(dni);
                return Ok(cliente);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { mensaje = e.Message });
            }
        }

        // Listar todos los Clientes
        [HttpGet]
        public async Task<ActionResult<List<ClienteDTO>>> ListarClientes()
        {
            var clientes = await _clienteService.ListarClientes();
            return Ok(clientes);
        }

        // Actualizar Cliente
        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteDTO>> ActualizarCliente(string id, [FromBody] ClienteDTO clienteDTO)
        {
            try
            {
                var clienteActualizado = await _clienteService.ActualizarCliente(id, clienteDTO);
                return Ok(clienteActualizado);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { mensaje = e.Message });
            }
        }

        // Eliminar Cliente
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCliente(string id)
        {
            try
            {
                await _clienteService.EliminarCliente(id);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { mensaje = e.Message });
            }
        }
    }
}
