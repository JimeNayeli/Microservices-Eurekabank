/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ec.edu.monster.controlador;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import ec.edu.monster.modelo.CancelarCuentaRequest;
import ec.edu.monster.modelo.Cuenta;
import ec.edu.monster.modelo.CuentaDetalleResponse;
import ec.edu.monster.modelo.CuentaRequest;
import ec.edu.monster.modelo.CuentaResponse;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.lang.reflect.Type;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author ckan1
 */
public class CuentaControlador {
    private static final String BASE_URL = "http://localhost:5057/api/Cuentas";
    private final Gson gson = new Gson();

    // Método para obtener una cuenta por ID
public CuentaDetalleResponse obtenerCuentaPorId(String id) {
    try {
        URL url = new URL(BASE_URL + "/" + id + "/detalles");
        HttpURLConnection conn = (HttpURLConnection) url.openConnection();
        conn.setRequestMethod("GET");
        conn.setRequestProperty("Accept", "application/json");

        if (conn.getResponseCode() != 200) {
            throw new RuntimeException("Error: HTTP " + conn.getResponseCode());
        }

        BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
        CuentaDetalleResponse detalle = gson.fromJson(br, CuentaDetalleResponse.class);
        conn.disconnect();
        return detalle;
    } catch (Exception e) {
        e.printStackTrace();
        return null;
    }
}


    // Método para obtener todas las cuentas activas
    public List<CuentaResponse> listarCuentas() {
    try {
        URL url = new URL(BASE_URL + "/activas");
        HttpURLConnection conn = (HttpURLConnection) url.openConnection();
        conn.setRequestMethod("GET");
        conn.setRequestProperty("Accept", "application/json");

        if (conn.getResponseCode() != 200) {
            throw new RuntimeException("Error: HTTP " + conn.getResponseCode());
        }

        BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
        StringBuilder response = new StringBuilder();
        String line;
        
        while ((line = br.readLine()) != null) {
            response.append(line);
        }
        
        // Imprimir la respuesta JSON para depuración
        //System.out.println("JSON recibido: " + response.toString());

        Type listType = new TypeToken<List<CuentaResponse>>() {}.getType();
        List<CuentaResponse> cuentas = gson.fromJson(response.toString(), listType);
        
        // Verificar si la lista está vacía o es null
        //System.out.println("Número de cuentas recibidas: " + (cuentas != null ? cuentas.size() : "null"));
        
        conn.disconnect();
        return cuentas != null ? cuentas : new ArrayList<>();
    } catch (Exception e) {
        e.printStackTrace();
        System.out.println("Error al obtener las cuentas: " + e.getMessage());
        return new ArrayList<>();
    }
}

    // Método para crear una cuenta
    public Cuenta crearCuenta(Cuenta cuenta) {
    try {
        URL url = new URL(BASE_URL + "/crear");
        HttpURLConnection conn = (HttpURLConnection) url.openConnection();
        conn.setDoOutput(true);
        conn.setRequestMethod("POST");
        conn.setRequestProperty("Content-Type", "application/json");

        String input = gson.toJson(cuenta);
        OutputStream os = conn.getOutputStream();
        os.write(input.getBytes());
        os.flush();

        if (conn.getResponseCode() != 201) {
            throw new RuntimeException("Error: HTTP " + conn.getResponseCode());
        }

        BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
        Cuenta cuentaCreada = gson.fromJson(br, Cuenta.class);
        conn.disconnect();
        return cuentaCreada;
    } catch (Exception e) {
        e.printStackTrace();
        return null;
    }
}

    // Método para actualizar una cuenta
    public Cuenta actualizarCuenta(String id, Cuenta cuenta) {
        try {
            URL url = new URL(BASE_URL + "/" + id + "/actualizar");
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setDoOutput(true);
            conn.setRequestMethod("PUT");
            conn.setRequestProperty("Content-Type", "application/json");

            String input = gson.toJson(cuenta);
            OutputStream os = conn.getOutputStream();
            os.write(input.getBytes());
            os.flush();

            if (conn.getResponseCode() != 200) {
                throw new RuntimeException("Error: HTTP " + conn.getResponseCode());
            }

            BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
            Cuenta cuentaActualizada = gson.fromJson(br, Cuenta.class);
            conn.disconnect();
            return cuentaActualizada;
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }

    // Método para cancelar una cuenta
    public boolean cancelarCuenta(String id, String clave) {
        try {
            URL url = new URL(BASE_URL + "/" + id + "/cancelar");
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setDoOutput(true);
            conn.setRequestMethod("PUT");
            conn.setRequestProperty("Content-Type", "application/json");

            String input = gson.toJson(new CancelarCuentaRequest(clave, true));
            OutputStream os = conn.getOutputStream();
            os.write(input.getBytes());
            os.flush();

            if (conn.getResponseCode() != 200) {
                throw new RuntimeException("Error: HTTP " + conn.getResponseCode());
            }

            conn.disconnect();
            return true;
        } catch (Exception e) {
            e.printStackTrace();
            return false;
        }
    }
}
