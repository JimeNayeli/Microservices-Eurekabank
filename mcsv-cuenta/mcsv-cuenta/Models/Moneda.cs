using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mcsv_cuenta.Models
{
    [Table("Moneda")] // Indica el nombre exacto de la tabla en la base de datos
    public class Moneda
    {
        [Key] // Define la clave primaria
        [Column("chr_monecodigo")]
        public string MonedaCodigo { get; set; }
        [Column("vch_monedescripcion")]
        public string Descripcion { get; set; }
    }
}
