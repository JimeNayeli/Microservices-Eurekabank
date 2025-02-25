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
        /// <param name="cuenta">C�digo de la cuenta.</param>
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
        /// Registra un dep�sito en una cuenta.
        /// </summary>
        /// <param name="cuenta">C�digo de la cuenta.</param>
        /// <param name="importe">Importe del dep�sito.</param>
        /// <param name="codEmp">C�digo del empleado.</param>
        /// <returns>Estado de la operaci�n.</returns>
        [HttpPost("deposito")]
        public IActionResult RegistrarDeposito([FromQuery] string cuenta, [FromQuery] double importe)
        {
            string codEmp = "0001";
            try
            {
                var estado = _eurekaService.RegistrarDeposito(cuenta, importe, codEmp);
                if (estado == 1)
                    return Ok("Dep�sito registrado con �xito.");
                else
                    return BadRequest("La cuenta no existe o no est� activa.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar dep�sito: {ex.Message}");
            }
        }

        /// <summary>
        /// Registra un retiro en una cuenta.
        /// </summary>
        /// <param name="cuenta">C�digo de la cuenta.</param>
        /// <param name="importe">Importe del retiro.</param>
        /// <param name="codEmp">C�digo del empleado.</param>
        /// <returns>Estado de la operaci�n.</returns>
        [HttpPost("retiro")]
        public IActionResult RegistrarRetiro([FromQuery] string cuenta, [FromQuery] double importe)
        {
            string codEmp = "0001";
            try
            {
                var estado = _eurekaService.RegistrarRetiro(cuenta, importe, codEmp);
                if (estado == 1)
                    return Ok("Retiro registrado con �xito.");
                else if (estado == -2)
                    return BadRequest("Saldo insuficiente.");
                else
                    return BadRequest("La cuenta no existe o no est� activa.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar retiro: {ex.Message}");
            }
        }

        /// <summary>
        /// Registra una transferencia entre cuentas.
        /// </summary>
        /// <param name="cuentaOrigen">C�digo de la cuenta origen.</param>
        /// <param name="cuentaDestino">C�digo de la cuenta destino.</param>
        /// <param name="importe">Importe de la transferencia.</param>
        /// <param name="codEmp">C�digo del empleado.</param>
        /// <returns>Estado de la operaci�n.</returns>
        [HttpPost("transferencia")]
        public IActionResult RegistrarTransferencia([FromQuery] string cuentaOrigen, [FromQuery] string cuentaDestino, [FromQuery] double importe)
        {
            string codEmp = "0001";
            try
            {
                var estado = _eurekaService.RegistrarTransferencia(cuentaOrigen, cuentaDestino, importe, codEmp);
                if (estado == 1)
                    return Ok("Transferencia realizada con �xito.");
                else if (estado == -2)
                    return BadRequest("Saldo insuficiente en la cuenta origen.");
                else if (estado == -3)
                    return BadRequest("La cuenta destino no existe o no est� activa.");
                else
                    return BadRequest("La cuenta origen no existe o no est� activa.");
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
        /// <param name="password">Contrase�a.</param>
        /// <returns>Resultado de la validaci�n.</returns>
        [HttpPost("validar-usuario")]
        public IActionResult ValidarUsuario([FromQuery] string usuario, [FromQuery] string password)
        {
            try
            {
                var esValido = _eurekaService.ValidarUsuario(usuario, password);
                if (esValido)
                    return Ok("Usuario v�lido.");
                else
                    return Unauthorized("Usuario o contrase�a incorrectos.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al validar usuario: {ex.Message}");
            }
        }
    }
}
