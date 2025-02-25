using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using WS_EUREKANUBE_RESTFULL.EurekaService;
using WS_EUREKANUBE_RESTFULL.Model;

namespace WS_EUREKANUBE_RESTFULL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EurekaController : ControllerBase
    {
        private readonly EurekaServicio _eurekaService;

        public EurekaController()
        {
            _eurekaService = new EurekaServicio();
        }

        /// <summary>
        /// Obtiene los movimientos de una cuenta.
        /// </summary>
        /// <param name="cuenta">Código de la cuenta.</param>
        /// <returns>Lista de movimientos.</returns>
        [HttpGet("movimientos/{cuenta}")]
        public IActionResult LeerMovimientos(string cuenta)
        {
            try
            {
                var movimientos = _eurekaService.LeerMovimientos(cuenta);
                return Ok(movimientos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al leer movimientos: {ex.Message}");
            }
        }

        /// <summary>
        /// Registra un depósito en una cuenta.
        /// </summary>
        /// <param name="cuenta">Código de la cuenta.</param>
        /// <param name="importe">Importe del depósito.</param>
        /// <param name="codEmp">Código del empleado.</param>
        /// <returns>Estado de la operación.</returns>
        [HttpPost("deposito")]
        public IActionResult RegistrarDeposito([FromQuery] string cuenta, [FromQuery] double importe)
        {
            string codEmp = "0001";
            try
            {
                var estado = _eurekaService.RegistrarDeposito(cuenta, importe, codEmp);
                if (estado == 1)
                    return Ok("Depósito registrado con éxito.");
                else
                    return BadRequest("La cuenta no existe o no está activa.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar depósito: {ex.Message}");
            }
        }

        /// <summary>
        /// Registra un retiro en una cuenta.
        /// </summary>
        /// <param name="cuenta">Código de la cuenta.</param>
        /// <param name="importe">Importe del retiro.</param>
        /// <param name="codEmp">Código del empleado.</param>
        /// <returns>Estado de la operación.</returns>
        [HttpPost("retiro")]
        public IActionResult RegistrarRetiro([FromQuery] string cuenta, [FromQuery] double importe)
        {
            string codEmp = "0001";
            try
            {
                var estado = _eurekaService.RegistrarRetiro(cuenta, importe, codEmp);
                if (estado == 1)
                    return Ok("Retiro registrado con éxito.");
                else if (estado == -2)
                    return BadRequest("Saldo insuficiente.");
                else
                    return BadRequest("La cuenta no existe o no está activa.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar retiro: {ex.Message}");
            }
        }

        /// <summary>
        /// Registra una transferencia entre cuentas.
        /// </summary>
        /// <param name="cuentaOrigen">Código de la cuenta origen.</param>
        /// <param name="cuentaDestino">Código de la cuenta destino.</param>
        /// <param name="importe">Importe de la transferencia.</param>
        /// <param name="codEmp">Código del empleado.</param>
        /// <returns>Estado de la operación.</returns>
        [HttpPost("transferencia")]
        public IActionResult RegistrarTransferencia([FromQuery] string cuentaOrigen, [FromQuery] string cuentaDestino, [FromQuery] double importe)
        {
            string codEmp = "0001";
            try
            {
                var estado = _eurekaService.RegistrarTransferencia(cuentaOrigen, cuentaDestino, importe, codEmp);
                if (estado == 1)
                    return Ok("Transferencia realizada con éxito.");
                else if (estado == -2)
                    return BadRequest("Saldo insuficiente en la cuenta origen.");
                else if (estado == -3)
                    return BadRequest("La cuenta destino no existe o no está activa.");
                else
                    return BadRequest("La cuenta origen no existe o no está activa.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar transferencia: {ex.Message}");
            }
        }

        /// <summary>
        /// Valida las credenciales de un usuario.
        /// </summary>
        /// <param name="usuario">Nombre de usuario.</param>
        /// <param name="password">Contraseña.</param>
        /// <returns>Resultado de la validación.</returns>
        [HttpPost("validar-usuario")]
        public IActionResult ValidarUsuario([FromQuery] string usuario, [FromQuery] string password)
        {
            try
            {
                var esValido = _eurekaService.ValidarUsuario(usuario, password);
                if (esValido)
                    return Ok("Usuario válido.");
                else
                    return Unauthorized("Usuario o contraseña incorrectos.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al validar usuario: {ex.Message}");
            }
        }
    }
}
