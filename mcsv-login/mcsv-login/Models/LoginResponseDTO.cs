namespace mcsv_login.Models
{
    public class LoginResponseDTO
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Mensaje { get; set; }

        public LoginResponseDTO(string id, string nombre, string mensaje)
        {
            Id = id;
            Nombre = nombre;
            Mensaje = mensaje;
        }
    }
}
