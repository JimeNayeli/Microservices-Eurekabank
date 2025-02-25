using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mcsv_cliente.Models
{
    [Table("Cliente")] // Mapea a la tabla Cliente en la base de datos
    public class Cliente
    {
        [Key]
        [Column("chr_cliecodigo", TypeName = "char(5)")]
        public string ClienteCodigo { get; set; }

        [Column("vch_cliepaterno", TypeName = "varchar(25)")]
        [MaxLength(25)]
        [Required]
        public string Paterno { get; set; }

        [Column("vch_cliematerno", TypeName = "varchar(25)")]
        [MaxLength(25)]
        [Required]
        public string Materno { get; set; }

        [Column("vch_clienombre", TypeName = "varchar(30)")]
        [MaxLength(30)]
        [Required]
        public string Nombre { get; set; }

        [Column("chr_cliedni", TypeName = "char(8)")]
        [Required]
        public string DNI { get; set; }

        [Column("vch_clieciudad", TypeName = "varchar(30)")]
        [MaxLength(30)]
        public string Ciudad { get; set; }

        [Column("vch_cliedireccion", TypeName = "varchar(50)")]
        [MaxLength(50)]
        public string Direccion { get; set; }

        [Column("vch_clietelefono", TypeName = "varchar(20)")]
        [MaxLength(20)]
        public string Telefono { get; set; }

        [Column("vch_clieemail", TypeName = "varchar(50)")]
        [MaxLength(50)]
        public string Email { get; set; }
    }
}
