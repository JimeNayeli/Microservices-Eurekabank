using mcsv_cuenta.Data;
using mcsv_cuenta.Models;
using Microsoft.EntityFrameworkCore;

namespace mcsv_cuenta.Services
{
    public class CuentaService
    {
        private readonly ApplicationDbContext _context;

        public CuentaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string Message, string NewState)> ActualizarEstadoCuenta(string codigoCuenta, ActualizarEstadoCuentaDTO request)
        {
            var cuenta = await _context.Cuentas.FirstOrDefaultAsync(c => c.CuentaCodigo == codigoCuenta);

            if (cuenta == null)
                return (false, "Cuenta no encontrada", null);

            if (cuenta.Clave != request.Clave)
                return (false, "Clave incorrecta", null);

            string estadoNuevo = request.Cancelar ? "CANCELADO" : "ACTIVO";

            if (cuenta.Estado != estadoNuevo)
            {
                cuenta.Estado = estadoNuevo;
                _context.Cuentas.Update(cuenta);
                await _context.SaveChangesAsync();
            }

            return (true, "Estado de cuenta actualizado", estadoNuevo);
        }


        public async Task<CuentaDetalleDTO> ObtenerDetallesCuenta(string codigoCuenta)
        {
            var cuentaDetalle = await (from c in _context.Cuentas
                                       join m in _context.Monedas on c.MonedaCodigo equals m.MonedaCodigo
                                       join s in _context.Sucursales on c.SucursalCodigo equals s.SucursalCodigo
                                       join e in _context.Empleados on c.EmpleadoCodigo equals e.EmpleadoCodigo
                                       join cli in _context.Clientes on c.ClienteCodigo equals cli.ClienteCodigo
                                       where c.CuentaCodigo == codigoCuenta
                                       select new CuentaDetalleDTO
                                       {
                                           CodigoCuenta = c.CuentaCodigo,
                                           Moneda = m.Descripcion,
                                           Sucursal = s.Nombre,
                                           CiudadSucursal = s.Ciudad,
                                           NombreEmpleado = $"{e.Nombre} {e.Paterno} {e.Materno}",
                                           DniCliente = cli.DNI,
                                           NombreCliente = $"{cli.Nombre} {cli.Paterno} {cli.Materno}",
                                           CiudadCliente = cli.Ciudad,
                                           DireccionCliente = cli.Direccion,
                                           TelefonoCliente = cli.Telefono,
                                           EmailCliente = cli.Email,
                                           FechaCreacion = c.FechaCreacion.ToString("yyyy-MM-dd"),
                                           NumeroMovimientos = c.ContMovimientos,
                                           Saldo = c.Saldo
                                       }).FirstOrDefaultAsync();

            return cuentaDetalle;
        }

        public async Task<List<CuentaActivaDTO>> ObtenerCuentasActivas()
        {
            var cuentasActivas = await (from c in _context.Cuentas
                                        join cli in _context.Clientes on c.ClienteCodigo equals cli.ClienteCodigo
                                        where c.Estado == "ACTIVO"
                                        select new CuentaActivaDTO
                                        {
                                            CodigoCuenta = c.CuentaCodigo,
                                            NombreCliente = cli.Nombre
                                        }).ToListAsync();

            return cuentasActivas;
        }

        public async Task<Cuenta> CrearCuenta(CrearCuentaDTO cuentaDTO)
        {
            // 🔹 Verificar que las entidades referenciadas existen en la base de datos
            var moneda = await _context.Monedas.FindAsync(cuentaDTO.MonedaCodigo);
            if (moneda == null) throw new Exception("Moneda no encontrada");

            var sucursal = await _context.Sucursales.FindAsync(cuentaDTO.SucursalCodigo);
            if (sucursal == null) throw new Exception("Sucursal no encontrada");

            var empleado = await _context.Empleados.FindAsync(cuentaDTO.EmpleadoCodigo);
            if (empleado == null) throw new Exception("Empleado no encontrado");

            var cliente = await _context.Clientes.FindAsync(cuentaDTO.ClienteCodigo);
            if (cliente == null) throw new Exception("Cliente no encontrado");

            // 🔹 Generar un nuevo código de cuenta basado en el último existente en la BD
            string nuevoCodigo = await ObtenerSiguienteCodigoCuenta();

            // 🔹 Crear la nueva cuenta con los valores correctos
            var nuevaCuenta = new Cuenta
            {
                CuentaCodigo = nuevoCodigo,
                MonedaCodigo = cuentaDTO.MonedaCodigo,
                SucursalCodigo = cuentaDTO.SucursalCodigo,
                EmpleadoCodigo = cuentaDTO.EmpleadoCodigo,
                ClienteCodigo = cuentaDTO.ClienteCodigo,
                Clave = cuentaDTO.Clave,
                Estado = "ACTIVO", // 🔹 Asegurar que el estado se asigne correctamente
                FechaCreacion = DateTime.UtcNow, // 🔹 Se establece la fecha actual
                Saldo = 0.0m, // 🔹 El saldo inicial es 0
                ContMovimientos = 0 // 🔹 Inicializar con 0 movimientos
            };

            // 🔹 Guardar en la base de datos
            _context.Cuentas.Add(nuevaCuenta);
            await _context.SaveChangesAsync();

            return nuevaCuenta;
        }

        public async Task<(bool Success, string Message)> ActualizarDatosCliente(string codigoCuenta, ActualizarClienteDTO request)
        {
            // Buscar la cuenta en la base de datos
            var cuenta = await _context.Cuentas
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(c => c.CuentaCodigo == codigoCuenta);

            if (cuenta == null)
                return (false, "Cuenta no encontrada");

            // Obtener el cliente asociado
            var cliente = cuenta.Cliente;
            if (cliente == null)
                return (false, "Cliente no encontrado");

            // Actualizar solo los campos permitidos
            cliente.Ciudad = request.Ciudad;
            cliente.Direccion = request.Direccion;
            cliente.Telefono = request.Telefono;
            cliente.Email = request.Email;

            // Guardar cambios en la BD
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();

            return (true, "Datos del cliente actualizados correctamente");
        }

        public async Task<string> ObtenerSiguienteCodigoCuenta()
        {
            // Obtener el último código de cuenta registrado en la BD
            var ultimoCodigo = await _context.Cuentas
                .OrderByDescending(c => c.CuentaCodigo)
                .Select(c => c.CuentaCodigo)
                .FirstOrDefaultAsync();

            // Si no hay cuentas en la BD, comenzamos desde un valor inicial
            if (string.IsNullOrEmpty(ultimoCodigo))
            {
                return "00200001"; // Primer código si no hay registros previos
            }

            // Convertir el código actual a número y sumarle 1
            if (int.TryParse(ultimoCodigo, out int numero))
            {
                return (numero + 1).ToString("D8"); // Mantener formato con ceros a la izquierda
            }

            throw new Exception("Error al generar el nuevo código de cuenta.");
        }


    }
}
