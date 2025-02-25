using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mcsv_login.Models
{
    [Table("Usuario")] // Mapea a la tabla Usuario en SQL Server
    public class Usuario
    {
        [Key]
        [Column("chr_emplcodigo", TypeName = "char(4)")]
        public string EmpleadoCodigo { get; set; }

        [Column("vch_emplusuario", TypeName = "varchar(20)")]
        [MaxLength(20)]
        [Required]
        public string UsuarioNombre { get; set; }

        [Column("vch_emplclave", TypeName = "varchar(50)")]
        [MaxLength(100)]
        [Required]
        public string Clave { get; set; }

        [Column("vch_emplestado", TypeName = "varchar(15)")]
        [MaxLength(15)]
        [Required]
        public string Estado { get; set; }

        // Relación uno a uno con Empleado
        [ForeignKey("EmpleadoCodigo")]
        public Empleado Empleado { get; set; }
    }
}
