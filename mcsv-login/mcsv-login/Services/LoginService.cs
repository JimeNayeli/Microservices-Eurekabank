using mcsv_login.Data;
using mcsv_login.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace mcsv_login.Services
{
    public class LoginService
    {
        private readonly ApplicationDbContext _context;

        public LoginService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Login para Cliente usando la cuenta y clave
        public async Task<Cliente> AutenticarClienteAsync(string cuentaId, string clave)
        {
            var cuenta = await _context.Cuentas
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(c => c.CuentaCodigo == cuentaId);

            if (cuenta != null && cuenta.Clave == clave)
            {
                return cuenta.Cliente;
            }
            return null;
        }

        // Login para Empleado usando código y clave
        public async Task<Empleado> AutenticarEmpleadoAsync(string codigoEmpleado, string clave)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.EmpleadoCodigo == codigoEmpleado);

            if (usuario != null)
            {
                string claveEncriptada = EncriptarSHA1(clave);
                if (usuario.Clave == claveEncriptada)
                {
                    var empleado = await _context.Empleados
                        .FirstOrDefaultAsync(e => e.EmpleadoCodigo == codigoEmpleado);
                    return empleado;
                }
            }
            return null;
        }

        // Método para encriptar usando SHA-1 (igual que en tu código Java)
        private string EncriptarSHA1(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha1.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // Formato hexadecimal
                }
                return sb.ToString();
            }
        }
    }
}
