namespace mcsv_cuenta.Models
{
    public class Movimiento
    {
        public string CuentaCodigo { get; set; }
        public int MovimientoNumero { get; set; }
        public DateTime Fecha { get; set; }
        public string EmpleadoCodigo { get; set; }
        public string TipoCodigo { get; set; }
        public decimal Importe { get; set; }
        public string Referencia { get; set; }

        public Cuenta Cuenta { get; set; }
        public Empleado Empleado { get; set; }
        public TipoMovimiento TipoMovimiento { get; set; }
    }
}
