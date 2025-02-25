using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mcsv_cuenta.Models
{
    [Table("Empleado")] // Asegura que Entity Framework mapea a la tabla correcta
    public class Empleado
    {
        [Key]
        [Column("chr_emplcodigo")] // Nombre exacto de la columna en la base de datos
        public string EmpleadoCodigo { get; set; }

        [Column("vch_emplpaterno")]
        public string Paterno { get; set; }

        [Column("vch_emplmaterno")]
        public string Materno { get; set; }

        [Column("vch_emplnombre")]
        public string Nombre { get; set; }

        [Column("vch_emplciudad")]
        public string Ciudad { get; set; }

        [Column("vch_empldireccion")]
        public string Direccion { get; set; }

        [Column("vch_emplusuario")]
        public string Usuario { get; set; }

        [Column("vch_emplclave")]
        public string Clave { get; set; }
    }
}
