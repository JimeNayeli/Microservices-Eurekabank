/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ec.edu.monster.modelo;

import javax.ws.rs.ClientErrorException;
import javax.ws.rs.client.Client;
import javax.ws.rs.client.WebTarget;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

/**
 *
 * @author ckan1
 */
public class EurekabankService {
    private WebTarget webTarget;
    private Client client;
    private static final String BASE_URI = "http://localhost:5069/";

    public EurekabankService() {
        client = javax.ws.rs.client.ClientBuilder.newClient();
        webTarget = client.target(BASE_URI).path("Eureka");
    }

    public Response obtenerMovimientos(String cuenta) throws ClientErrorException {
        WebTarget target = webTarget.path("movimientos").path(cuenta);
        
        return target.request(javax.ws.rs.core.MediaType.APPLICATION_JSON).get();
    }

    public Response registrarDeposito(String cuenta, double importe) throws ClientErrorException {
        // Construir la URL con los parámetros de consulta
        WebTarget target = webTarget.path("deposito")
                .queryParam("cuenta", cuenta)
                .queryParam("importe", importe);

        // Realizar la solicitud POST (sin cuerpo, ya que los datos están en la URL)
        return target.request(javax.ws.rs.core.MediaType.APPLICATION_JSON).post(null);
    }
    
    public Response registrarTransferencia(String cuentaOrigen, String cuentaDestino,  double importe) throws ClientErrorException {
        // Construir la URL con los parámetros de consulta
        WebTarget target = webTarget.path("transferencia")
                .queryParam("cuentaOrigen", cuentaOrigen)
                .queryParam("cuentaDestino", cuentaDestino)
                .queryParam("importe", importe);

        // Realizar la solicitud POST (sin cuerpo, ya que los datos están en la URL)
        return target.request(javax.ws.rs.core.MediaType.APPLICATION_JSON).post(null);
    }
    
    public Response registrarRetiro(String cuenta, double importe) throws ClientErrorException {
        // Construir la URL con los parámetros de consulta
        WebTarget target = webTarget.path("retiro")
                .queryParam("cuenta", cuenta)
                .queryParam("importe", importe);

        // Realizar la solicitud POST (sin cuerpo, ya que los datos están en la URL)
        return target.request(javax.ws.rs.core.MediaType.APPLICATION_JSON).post(null);
    }

    public Response login(String usuario, String password) throws ClientErrorException {
        WebTarget resource = webTarget.path("login")
                                      .queryParam("usuario", usuario)
                                      .queryParam("password", password);
        return resource.request(MediaType.APPLICATION_JSON).post(null, Response.class);
    }

    public void close() {
        client.close();
    }
}
