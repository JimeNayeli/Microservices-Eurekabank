using mcsv_cliente.Data;
using mcsv_cliente.Models;
using Microsoft.EntityFrameworkCore;

namespace mcsv_cliente.Services
{
    public class ClienteService
    {
        private readonly ApplicationDbContext _context;

        public ClienteService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Convertir Cliente a ClienteDTO (manual, sin AutoMapper)
        private ClienteDTO ConvertirClienteADTO(Cliente cliente)
        {
            return new ClienteDTO
            {
                id = cliente.ClienteCodigo,
                apellidoPaterno = cliente.Paterno,
                apellidoMaterno = cliente.Materno,
                Nombre = cliente.Nombre,
                DNI = cliente.DNI,
                Ciudad = cliente.Ciudad,
                Direccion = cliente.Direccion,
                Telefono = cliente.Telefono,
                Email = cliente.Email
            };
        }

        // Crear Cliente
        public async Task<ClienteDTO> CrearCliente(ClienteDTO clienteDTO)
        {
            var cliente = new Cliente
            {
                ClienteCodigo = clienteDTO.id,
                Paterno = clienteDTO.apellidoPaterno,
                Materno = clienteDTO.apellidoMaterno,
                Nombre = clienteDTO.Nombre,
                DNI = clienteDTO.DNI,
                Ciudad = clienteDTO.Ciudad,
                Direccion = clienteDTO.Direccion,
                Telefono = clienteDTO.Telefono,
                Email = clienteDTO.Email
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return ConvertirClienteADTO(cliente);
        }

        // Obtener Cliente por ID
        public async Task<ClienteDTO> ObtenerClientePorId(string id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) throw new KeyNotFoundException("Cliente no encontrado");

            return ConvertirClienteADTO(cliente);
        }

        // Obtener Cliente por DNI
        public async Task<ClienteDTO> ObtenerClientePorDni(string dni)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.DNI == dni);
            if (cliente == null) throw new KeyNotFoundException("Cliente no encontrado");

            return ConvertirClienteADTO(cliente);
        }

        // Listar todos los Clientes
        public async Task<List<ClienteDTO>> ListarClientes()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return clientes.Select(c => ConvertirClienteADTO(c)).ToList();
        }

        // Actualizar Cliente
        public async Task<ClienteDTO> ActualizarCliente(string id, ClienteDTO clienteDTO)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) throw new KeyNotFoundException("Cliente no encontrado");

            // Asignar manualmente los valores nuevos
            cliente.Paterno = clienteDTO.apellidoPaterno;
            cliente.Materno = clienteDTO.apellidoMaterno;
            cliente.Nombre = clienteDTO.Nombre;
            cliente.DNI = clienteDTO.DNI;
            cliente.Ciudad = clienteDTO.Ciudad;
            cliente.Direccion = clienteDTO.Direccion;
            cliente.Telefono = clienteDTO.Telefono;
            cliente.Email = clienteDTO.Email;

            await _context.SaveChangesAsync();
            return ConvertirClienteADTO(cliente);
        }

        // Eliminar Cliente
        public async Task EliminarCliente(string id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) throw new KeyNotFoundException("Cliente no encontrado");

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
