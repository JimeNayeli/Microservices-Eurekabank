using System.ComponentModel.DataAnnotations;

namespace mcsv_cuenta.Models
{
    public class TipoMovimiento
    {
        [Key] // Define la clave primaria
        public string TipoCodigo { get; set; }
        public string Descripcion { get; set; }
        public string Accion { get; set; }
        public string Estado { get; set; }
    }
}
