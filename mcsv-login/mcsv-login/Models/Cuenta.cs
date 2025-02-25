using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mcsv_login.Models
{
    [Table("Cuenta")] // Indica el nombre exacto de la tabla en la base de datos
    public class Cuenta
    {
        [Key]
        [Column("chr_cuencodigo")]
        public string CuentaCodigo { get; set; }

        [Column("chr_monecodigo")]
        public string MonedaCodigo { get; set; }

        [Column("chr_sucucodigo")]
        public string SucursalCodigo { get; set; }

        [Column("chr_emplcreacuenta")]
        public string EmpleadoCodigo { get; set; }

        [Column("chr_cliecodigo")]
        public string ClienteCodigo { get; set; }

        [Column("dec_cuensaldo")]
        public decimal Saldo { get; set; }

        [Column("dtt_cuenfechacreacion")]
        public DateTime FechaCreacion { get; set; }

        [Column("vch_cuenestado")]
        public string Estado { get; set; }

        [Column("int_cuencontmov")]
        public int ContMovimientos { get; set; }

        [Column("chr_cuenclave")]
        public string Clave { get; set; }

        public Cliente Cliente { get; set; }
        public Empleado Empleado { get; set; }
    }
}
