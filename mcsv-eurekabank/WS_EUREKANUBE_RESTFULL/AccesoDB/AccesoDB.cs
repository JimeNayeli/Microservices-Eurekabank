using Microsoft.Data.SqlClient;

namespace WS_EUREKANUBE_RESTFULL.AccesoDB
{
    public class AccesoDB
    {
        public SqlConnection GetConnection()
        {
            try
            {
                //Configura la cadena de conexión con los datos de AWS
                string connectionString = "Server=tu-url,1433;" +
                                          "Database=EUREKABANK;" +
                                          "User Id=tu-username;" +
                                          "Password=tu-password;Encrypt=True;TrustServerCertificate=True;";



                // Devuelve una conexión lista para usar (sin abrir)
                return new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                // Lanza una excepción específica para manejo en niveles superiores
                throw new Exception("Error al obtener la conexión a la base de datos: " + ex.Message, ex);
            }
        }
    }
}
