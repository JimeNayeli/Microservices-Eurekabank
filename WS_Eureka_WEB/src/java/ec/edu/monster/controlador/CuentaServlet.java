/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ec.edu.monster.controlador;

import com.google.gson.Gson;
import ec.edu.monster.modelo.Cuenta;
import ec.edu.monster.modelo.CuentaDetalleResponse;
import ec.edu.monster.modelo.CuentaRequest;
import ec.edu.monster.modelo.CuentaResponse;
import java.io.BufferedReader;
import java.io.IOException;
import java.util.ArrayList;
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
@WebServlet("/CuentaServlet")
public class CuentaServlet extends HttpServlet {
    private final CuentaControlador cuentaControlador = new CuentaControlador();
    private final Gson gson = new Gson();

@Override
protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
    String id = request.getParameter("id");
    String action = request.getParameter("action");

    if (action != null && action.equals("detalles")) {
        // 🔹 Obtener detalles de la cuenta en formato JSON para el modal
        if (id != null && !id.isEmpty()) {
            CuentaDetalleResponse detalle = cuentaControlador.obtenerCuentaPorId(id);
            if (detalle != null) {
                response.setContentType("application/json");
                response.setCharacterEncoding("UTF-8");
                response.getWriter().write(gson.toJson(detalle));
            } else {
                response.sendError(HttpServletResponse.SC_NOT_FOUND, "Cuenta no encontrada");
            }
        } else {
            response.sendError(HttpServletResponse.SC_BAD_REQUEST, "ID de cuenta requerido");
        }
    } else {
        // 🔹 Obtener todas las cuentas activas con `CuentaResponse`
        List<CuentaResponse> cuentas = cuentaControlador.listarCuentas();
        request.setAttribute("cuentas", cuentas); // ⚠️ Solo enviamos CuentaResponse
        request.getRequestDispatcher("cuentas.jsp").forward(request, response);
    }
}

@Override
protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
    String action = request.getParameter("action");

    if ("crear".equals(action)) {
        // Obtener los datos del formulario
        String monedaCodigo = request.getParameter("monedaCodigo");
        String sucursalCodigo = request.getParameter("sucursalCodigo");
        String empleadoCodigo = (String) request.getSession().getAttribute("userId"); // Código del empleado logueado
        String clienteCodigo = request.getParameter("clienteCodigo");
        String clave = request.getParameter("clave");

        // Crear el objeto Cuenta
        Cuenta cuenta = new Cuenta();
        cuenta.setMonedaCodigo(monedaCodigo);
        cuenta.setSucursalCodigo(sucursalCodigo);
        cuenta.setEmpleadoCodigo(empleadoCodigo);
        cuenta.setClienteCodigo(clienteCodigo);
        cuenta.setClave(clave);

        // Llamar al controlador para crear la cuenta
        Cuenta cuentaCreada = cuentaControlador.crearCuenta(cuenta);

        if (cuentaCreada != null) {
            response.sendRedirect("CuentaServlet"); // Redirigir a la lista de cuentas
        } else {
            request.setAttribute("errorMessage", "Error al crear la cuenta");
            request.getRequestDispatcher("cuentas.jsp").forward(request, response);
        }
    } else {
        // Lógica existente para otras acciones
    }
}
    
}

