namespace mcsv_cuenta.Models
{
    public class ActualizarEstadoCuentaDTO
    {
        public string Clave { get; set; }
        public bool Cancelar { get; set; } // true = ACTIVAR, false = CANCELAR
    }
}
