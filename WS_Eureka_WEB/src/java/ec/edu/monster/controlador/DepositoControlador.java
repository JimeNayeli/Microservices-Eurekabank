/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ec.edu.monster.controlador;

import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
import ec.edu.monster.modelo.EurekabankService;
import javax.ws.rs.core.Response;

/**
 *
 * @author ckan1
 */
public class DepositoControlador {
    private EurekabankService eurekabankService; 

    public DepositoControlador() {
        this.eurekabankService = new EurekabankService(); 
    }

    public String registrarDeposito(String cuenta, double importe) {
        try {
            Response response = eurekabankService.registrarDeposito(cuenta, importe);
            String jsonResponse = response.readEntity(String.class);
            if (response.getStatus() == 200) {
                JsonElement jsonElement = JsonParser.parseString(jsonResponse);

            if (jsonElement.isJsonObject()) {
                JsonObject jsonObject = jsonElement.getAsJsonObject();
                return jsonObject.get("mensaje").getAsString();
            } else {
                // Si no es un JSON, asumir que es un mensaje de texto plano
                return jsonResponse;
            }
            } else {
                // Procesar JSON de error
                String errorResponse = response.readEntity(String.class);
                JsonObject jsonObject = JsonParser.parseString(errorResponse).getAsJsonObject();
                String errorMensaje = jsonObject.get("error").getAsString();
                return "Error al registrar el depósito: " + errorMensaje;
            }
        } catch (Exception e) {
            // Manejo de excepciones inesperadas
            return "Error inesperado: " + e.getMessage();
        }
    }
    
    public String registrarTransferencia(String cuentaOrigen, String cuentaDestino, double importe) {
    try {
        Response response = eurekabankService.registrarTransferencia(cuentaOrigen, cuentaDestino, importe);
        String jsonResponse = response.readEntity(String.class);

        if (response.getStatus() == 200) {
            JsonElement jsonElement = JsonParser.parseString(jsonResponse);

            if (jsonElement.isJsonObject()) {
                JsonObject jsonObject = jsonElement.getAsJsonObject();
                return jsonObject.get("mensaje").getAsString();
            } else {
                return jsonResponse;  // Si no es JSON, devolver el texto directamente
            }
        } else {
            JsonElement jsonElement = JsonParser.parseString(jsonResponse);
            if (jsonElement.isJsonObject()) {
                JsonObject jsonObject = jsonElement.getAsJsonObject();
                return "Error al registrar la transferencia: " + jsonObject.get("error").getAsString();
            } else {
                return "Error desconocido: " + jsonResponse;
            }
        }
    } catch (Exception e) {
        return "Error inesperado: " + e.getMessage();
    }
}

    
    public String registrarRetiro(String cuenta, double importe) {
    try {
        Response response = eurekabankService.registrarRetiro(cuenta, importe);
        String jsonResponse = response.readEntity(String.class);

        if (response.getStatus() == 200) {
            JsonElement jsonElement = JsonParser.parseString(jsonResponse);

            if (jsonElement.isJsonObject()) {
                JsonObject jsonObject = jsonElement.getAsJsonObject();
                return jsonObject.get("mensaje").getAsString();
            } else {
                return jsonResponse;  // Si no es JSON, devolver el texto directamente
            }
        } else {
            JsonElement jsonElement = JsonParser.parseString(jsonResponse);
            if (jsonElement.isJsonObject()) {
                JsonObject jsonObject = jsonElement.getAsJsonObject();
                return "Error al registrar el retiro: " + jsonObject.get("error").getAsString();
            } else {
                return "Error desconocido: " + jsonResponse;
            }
        }
    } catch (Exception e) {
        return "Error inesperado: " + e.getMessage();
    }
}

}
