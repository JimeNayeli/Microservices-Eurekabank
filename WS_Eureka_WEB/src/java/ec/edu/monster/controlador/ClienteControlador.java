/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ec.edu.monster.controlador;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import ec.edu.monster.modelo.Cliente;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.lang.reflect.Type;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.List;

/**
 *
 * @author ckan1
 */
public class ClienteControlador {
    private static final String BASE_URL = "http://localhost:5234/api/Cliente"; // Cambia el puerto si es diferente

    // Método para obtener un cliente por ID
    public Cliente obtenerClientePorId(String id) {
        try {
            URL url = new URL(BASE_URL + "/" + id);
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("GET");
            conn.setRequestProperty("Accept", "application/json");

            if (conn.getResponseCode() != 200) {
                throw new RuntimeException("Error: HTTP " + conn.getResponseCode());
            }

            BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
            Gson gson = new Gson();
            Cliente cliente = gson.fromJson(br, Cliente.class);
            conn.disconnect();
            return cliente;
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }

    // Método para obtener todos los clientes
    public List<Cliente> listarClientes() {
        try {
            URL url = new URL(BASE_URL);
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("GET");
            conn.setRequestProperty("Accept", "application/json");

            if (conn.getResponseCode() != 200) {
                throw new RuntimeException("Error: HTTP " + conn.getResponseCode());
            }

            BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
            Gson gson = new Gson();
            Type listType = new TypeToken<List<Cliente>>() {}.getType();
            List<Cliente> clientes = gson.fromJson(br, listType);
            conn.disconnect();
            return clientes;
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }

    // Método para crear un cliente
    public Cliente crearCliente(Cliente cliente) {
        try {
            URL url = new URL(BASE_URL);
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setDoOutput(true);
            conn.setRequestMethod("POST");
            conn.setRequestProperty("Content-Type", "application/json");

            Gson gson = new Gson();
            String input = gson.toJson(cliente);
            //System.out.println("📤 Enviando JSON al servidor: " + input);
            OutputStream os = conn.getOutputStream();
            os.write(input.getBytes());
            os.flush();

            if (conn.getResponseCode() != 201) {
                BufferedReader errorReader = new BufferedReader(new InputStreamReader(conn.getErrorStream()));
                StringBuilder errorResponse = new StringBuilder();
                String line;
                while ((line = errorReader.readLine()) != null) {
                    errorResponse.append(line);
                }
                errorReader.close();

                throw new RuntimeException("Error: HTTP " + conn.getResponseCode() + " - " + errorResponse.toString());
            }


            BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
            Cliente clienteCreado = gson.fromJson(br, Cliente.class);
            conn.disconnect();
            return clienteCreado;
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }

    // Método para actualizar un cliente
    public Cliente actualizarCliente(String id, Cliente cliente) {
        try {
            URL url = new URL(BASE_URL + "/" + id);
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setDoOutput(true);
            conn.setRequestMethod("PUT");
            conn.setRequestProperty("Content-Type", "application/json");

            Gson gson = new Gson();
            String input = gson.toJson(cliente);

            OutputStream os = conn.getOutputStream();
            os.write(input.getBytes());
            os.flush();

            if (conn.getResponseCode() != 200) {
                throw new RuntimeException("Error: HTTP " + conn.getResponseCode());
            }

            BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
            Cliente clienteActualizado = gson.fromJson(br, Cliente.class);
            conn.disconnect();
            return clienteActualizado;
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }

    // Método para eliminar un cliente
    public boolean eliminarCliente(String id) {
        try {
            URL url = new URL(BASE_URL + "/" + id);
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("DELETE");

            if (conn.getResponseCode() != 200) {
                return false;
            }

            conn.disconnect();
            return true;
        } catch (Exception e) {
            e.printStackTrace();
            return false;
        }
    }
}
