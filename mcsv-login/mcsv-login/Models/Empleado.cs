using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mcsv_login.Models
{
    [Table("Empleado")] // Mapea a la tabla Empleado en SQL Server
    public class Empleado
    {
        [Key]
        [Column("chr_emplcodigo", TypeName = "char(4)")]
        public string EmpleadoCodigo { get; set; }

        [Column("vch_emplpaterno", TypeName = "varchar(25)")]
        [MaxLength(25)]
        [Required]
        public string Paterno { get; set; }

        [Column("vch_emplmaterno", TypeName = "varchar(25)")]
        [MaxLength(25)]
        [Required]
        public string Materno { get; set; }

        [Column("vch_emplnombre", TypeName = "varchar(30)")]
        [MaxLength(30)]
        [Required]
        public string Nombre { get; set; }

        [Column("vch_emplciudad", TypeName = "varchar(30)")]
        [MaxLength(30)]
        public string Ciudad { get; set; }

        [Column("vch_empldireccion", TypeName = "varchar(50)")]
        [MaxLength(50)]
        public string Direccion { get; set; }

        [Column("vch_emplusuario", TypeName = "varchar(15)")]
        [MaxLength(15)]
        public string Usuario { get; set; }

        [Column("vch_emplclave", TypeName = "varchar(15)")]
        [MaxLength(15)]
        public string Clave { get; set; }

        // Relación uno a uno con Usuario
        public Usuario UsuarioDatos { get; set; }
    }
}
