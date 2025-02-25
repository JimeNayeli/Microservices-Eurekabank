using mcsv_login.Models;
using mcsv_login.Services;
using Microsoft.AspNetCore.Mvc;

namespace mcsv_login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        // Endpoint para login de cliente
        [HttpPost("cliente")]
        public async Task<IActionResult> AutenticarCliente([FromBody] LoginRequestDTO loginRequest)
        {
            var cliente = await _loginService.AutenticarClienteAsync(loginRequest.UsernameOrAccount, loginRequest.Password);

            if (cliente != null)
            {
                var response = new LoginResponseDTO(cliente.ClienteCodigo, cliente.Nombre, "Autenticación exitosa");
                return Ok(response);
            }
            return Unauthorized(new LoginResponseDTO(null, null, "Credenciales incorrectas"));
        }

        // Endpoint para login de empleado
        [HttpPost("empleado")]
        public async Task<IActionResult> AutenticarEmpleado([FromBody] LoginRequestDTO loginRequest)
        {
            var empleado = await _loginService.AutenticarEmpleadoAsync(loginRequest.UsernameOrAccount, loginRequest.Password);

            if (empleado != null)
            {
                var response = new LoginResponseDTO(empleado.EmpleadoCodigo, empleado.Nombre, "Autenticación exitosa");
                return Ok(response);
            }
            return Unauthorized(new LoginResponseDTO(null, null, "Credenciales incorrectas"));
        }
    }
}
