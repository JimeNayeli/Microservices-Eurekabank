using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mcsv_cuenta.Models
{
    [Table("Sucursal")] // Indica el nombre exacto de la tabla en la base de datos
    public class Sucursal
    {
        [Key]
        [Column("chr_sucucodigo")] // Nombre real de la columna en SQL Server
        public string SucursalCodigo { get; set; }

        [Column("vch_sucunombre")]
        public string Nombre { get; set; }

        [Column("vch_sucuciudad")]
        public string Ciudad { get; set; }

        [Column("vch_sucudireccion")]
        public string Direccion { get; set; }

        [Column("int_sucucontcuenta")]
        public int ContadorCuenta { get; set; }
    }
}
