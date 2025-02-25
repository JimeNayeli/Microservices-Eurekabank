/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.modelo;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.google.gson.stream.JsonReader;
import java.io.StringReader;
import javax.ws.rs.ClientErrorException;
import javax.ws.rs.client.Client;
import javax.ws.rs.client.Entity;
import javax.ws.rs.client.WebTarget;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

/**
 *
 * @author Jimena
 */
public class LoginService {
    private WebTarget webTarget;
    private Client client;
    private static final String BASE_URI = "http://localhost:5017/api/";
    private Gson gson;

    public LoginService() {
        client = javax.ws.rs.client.ClientBuilder.newClient();
        webTarget = client.target(BASE_URI).path("Login");
        gson = new Gson();
    }
    
    
    public LoginResponse login(String usuario, String password, String tipoUsuario) throws ClientErrorException {
        WebTarget resourceTarget = webTarget.path(tipoUsuario); // /login/cliente o /login/empleado
        
        // Crear el objeto JSON para el body
        JsonObject jsonBody = new JsonObject();
        jsonBody.addProperty("usernameOrAccount", usuario);
        jsonBody.addProperty("password", password);
        
        Response response = resourceTarget
            .request(MediaType.APPLICATION_JSON)
            .post(Entity.entity(jsonBody.toString(), MediaType.APPLICATION_JSON));
        
        String jsonResponse = response.readEntity(String.class);
        return gson.fromJson(jsonResponse, LoginResponse.class);
    }

    public void close() {
        client.close();
    }
    
    
}
