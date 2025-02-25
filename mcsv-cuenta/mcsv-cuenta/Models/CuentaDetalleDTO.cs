namespace mcsv_cuenta.Models
{
    public class CuentaDetalleDTO
    {
        public string CodigoCuenta { get; set; }
        public string Moneda { get; set; }
        public string Sucursal { get; set; }
        public string CiudadSucursal { get; set; }
        public string NombreEmpleado { get; set; }
        public string DniCliente { get; set; }
        public string NombreCliente { get; set; }
        public string CiudadCliente { get; set; }
        public string DireccionCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public string EmailCliente { get; set; }
        public string FechaCreacion { get; set; }
        public int NumeroMovimientos { get; set; }
        public decimal Saldo { get; set; }
    }
}
