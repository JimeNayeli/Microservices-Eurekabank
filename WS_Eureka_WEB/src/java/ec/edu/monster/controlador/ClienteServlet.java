/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ec.edu.monster.controlador;

import com.google.gson.Gson;
import ec.edu.monster.modelo.Cliente;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.List;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

/**
 *
 * @author ckan1
 */
@WebServlet("/ClienteServlet")
public class ClienteServlet extends HttpServlet {
    private final ClienteControlador clienteControlador = new ClienteControlador();

    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String action = request.getParameter("action");
        
        if ("delete".equals(action)) {
            String id = request.getParameter("id");
            clienteControlador.eliminarCliente(id);
            response.sendRedirect("clientes.jsp");
        } else if ("get".equals(action)) {
            String id = request.getParameter("id");
            Cliente cliente = clienteControlador.obtenerClientePorId(id);
            response.setContentType("application/json");
            response.setCharacterEncoding("UTF-8");
            PrintWriter out = response.getWriter();
            out.print(new Gson().toJson(cliente));
            out.flush();
        } else {
            List<Cliente> clientes = clienteControlador.listarClientes();
            request.setAttribute("clientes", clientes);
            request.getRequestDispatcher("clientes.jsp").forward(request, response);
        }
    }

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String id = request.getParameter("id");
        String nombre = request.getParameter("nombre");
        String apellidoPaterno = request.getParameter("apellidoPaterno");
        String apellidoMaterno = request.getParameter("apellidoMaterno");
        String dni = request.getParameter("dni");
        String ciudad = request.getParameter("ciudad");
        String direccion = request.getParameter("direccion");
        String telefono = request.getParameter("telefono");
        String email = request.getParameter("email");

        Cliente cliente = new Cliente(id, apellidoPaterno, apellidoMaterno, nombre, dni, ciudad, direccion, telefono, email);

        System.out.println("📩 ID Recibido: " + id);

        // 🔍 1. Verificar si el cliente existe con un GET antes de decidir
        Cliente clienteExistente = clienteControlador.obtenerClientePorId(id);

        Cliente resultado;
        if (clienteExistente == null) {
            System.out.println("🆕 Cliente no encontrado en la BD. Creando nuevo cliente...");
            resultado = clienteControlador.crearCliente(cliente);
        } else {
            System.out.println("🔄 Cliente encontrado. Actualizando cliente con ID: " + id);
            resultado = clienteControlador.actualizarCliente(id, cliente);
        }

        if (resultado != null) {
            System.out.println("✅ Cliente procesado correctamente: " + resultado.getId());
        } else {
            System.out.println("❌ Error al procesar el cliente.");
        }

        response.sendRedirect("clientes.jsp");
    }
}
