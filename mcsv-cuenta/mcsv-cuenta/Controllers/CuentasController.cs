using mcsv_cuenta.Models;
using mcsv_cuenta.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace mcsv_cuenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {

        private readonly CuentaService _cuentaService;

        public CuentasController(CuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        [HttpPut("{codigoCuenta}/cancelar")]
        public async Task<IActionResult> CancelarCuenta(string codigoCuenta, [FromBody] ActualizarEstadoCuentaDTO request)
        {
            var resultado = await _cuentaService.ActualizarEstadoCuenta(codigoCuenta, request);

            if (!resultado.Success)
                return BadRequest(false); // Devuelve false si la clave es incorrecta o la cuenta no existe

            return Ok(true); // Devuelve true si la cuenta se canceló correctamente
        }


        [HttpGet("{codigoCuenta}/detalles")]
        public async Task<IActionResult> ObtenerDetallesCuenta(string codigoCuenta)
        {
            var detalles = await _cuentaService.ObtenerDetallesCuenta(codigoCuenta);

            if (detalles == null)
                return NotFound(new { mensaje = "Cuenta no encontrada" });

            return Ok(detalles);
        }

        [HttpGet("activas")]
        public async Task<ActionResult<List<CuentaActivaDTO>>> ObtenerCuentasActivas()
        {
            var cuentas = await _cuentaService.ObtenerCuentasActivas();

            if (cuentas == null || cuentas.Count == 0)
                return NotFound(new { mensaje = "No hay cuentas activas" });

            return Ok(cuentas);
        }

        [HttpPost("crear")]
        public async Task<ActionResult<Cuenta>> CrearCuenta([FromBody] CrearCuentaDTO cuentaDTO)
        {
            if (cuentaDTO == null)
            {
                return BadRequest("Los datos de la cuenta no pueden estar vacíos.");
            }

            var cuentaCreada = await _cuentaService.CrearCuenta(cuentaDTO);

            return CreatedAtAction(nameof(ObtenerDetallesCuenta), new { codigoCuenta = cuentaCreada.CuentaCodigo }, cuentaCreada);
        }


        [HttpPut("{codigoCuenta}/actualizar")]
        public async Task<IActionResult> ActualizarDatosCliente(string codigoCuenta, [FromBody] ActualizarClienteDTO request)
        {
            var result = await _cuentaService.ActualizarDatosCliente(codigoCuenta, request);

            if (!result.Success)
                return NotFound(new { mensaje = result.Message });

            return Ok(new { mensaje = result.Message });
        }

    }
}
