/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ec.edu.monster.controlador;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
import com.google.gson.reflect.TypeToken;
import ec.edu.monster.modelo.EurekabankService;
import ec.edu.monster.modelo.Movimiento;
import java.lang.reflect.Type;
import java.util.List;
import javax.ws.rs.core.Response;

/**
 *
 * @author ckan1
 */
public class MovimientosControlador {
    private EurekabankService eurekabankService;

    public MovimientosControlador() {
        this.eurekabankService = new EurekabankService();
    }

    public List<Movimiento> obtenerMovimientos(String cuenta) {
        try {
            Response response = eurekabankService.obtenerMovimientos(cuenta);

            if (response.getStatus() == 200) {
                String jsonResponse = response.readEntity(String.class);
                Type listType = new TypeToken<List<Movimiento>>() {}.getType();
                return new Gson().fromJson(jsonResponse, listType);
            } else {
                // Manejo de error genérico
                String errorResponse = response.readEntity(String.class);
                if (errorResponse != null && !errorResponse.isEmpty()) {
                    JsonObject jsonObject = JsonParser.parseString(errorResponse).getAsJsonObject();
                    String errorMensaje = jsonObject.has("error") 
                        ? jsonObject.get("error").getAsString() 
                        : "Error desconocido del servidor.";
                    throw new RuntimeException(errorMensaje);
                } else {
                    throw new RuntimeException("Respuesta de error vacía del servidor.");
                }
            }
        } catch (Exception e) {
            throw new RuntimeException("Error inesperado al obtener los movimientos: " + e.getMessage(), e);
        }
    }
}
