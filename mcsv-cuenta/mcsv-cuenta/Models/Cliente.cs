using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mcsv_cuenta.Models
{
    [Table("Cliente")] // Asegura que Entity Framework mapea a la tabla correcta
    public class Cliente
    {
        [Key]
        [Column("chr_cliecodigo")] // Nombre exacto de la columna en la base de datos
        public string ClienteCodigo { get; set; }

        [Column("vch_cliepaterno")]
        public string Paterno { get; set; }

        [Column("vch_cliematerno")]
        public string Materno { get; set; }

        [Column("vch_clienombre")]
        public string Nombre { get; set; }

        [Column("chr_cliedni")]
        public string DNI { get; set; }

        [Column("vch_clieciudad")]
        public string Ciudad { get; set; }

        [Column("vch_cliedireccion")]
        public string Direccion { get; set; }

        [Column("vch_clietelefono")]
        public string Telefono { get; set; }

        [Column("vch_clieemail")]
        public string Email { get; set; }
    }
}
