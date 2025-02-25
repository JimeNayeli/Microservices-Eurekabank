using System.ComponentModel.DataAnnotations;

namespace mcsv_cuenta.Models
{
    public class CrearCuentaDTO
    {
        
        [Required]
        public string MonedaCodigo { get; set; }

        [Required]
        public string SucursalCodigo { get; set; }

        [Required]
        public string EmpleadoCodigo { get; set; }

        [Required]
        public string ClienteCodigo { get; set; }

        [Required]
        public string Clave { get; set; }

    }

}
