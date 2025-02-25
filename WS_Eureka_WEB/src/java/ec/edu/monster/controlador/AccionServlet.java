/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ec.edu.monster.controlador;

import java.io.IOException;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

/**
 *
 * @author ckan1
 */
@WebServlet("/AccionServlet")
public class AccionServlet extends HttpServlet {
    protected void doPost(HttpServletRequest request, HttpServletResponse response) 
            throws ServletException, IOException {
        
        String accion = request.getParameter("accion");
        
        switch(accion) {
            case "consulta":
                request.getRequestDispatcher("consulta.jsp").forward(request, response);
                break;
            case "deposito":
                request.getRequestDispatcher("deposito.jsp").forward(request, response);
                break;
            case "retiro":
                request.getRequestDispatcher("retiro.jsp").forward(request, response);
                break;
            case "transferencia":
                request.getRequestDispatcher("transferencia.jsp").forward(request, response);
                break;
            case "clientes": // 🚀 Nueva opción para empleados
                response.sendRedirect("ClienteServlet");
                break;
            case "cuentas": // 🚀 Nueva opción para empleados
                response.sendRedirect("CuentaServlet");
                break;
            case "salir":
                // You might want to invalidate the session or redirect to a login page
                request.getSession().invalidate();
                response.sendRedirect("index.jsp"); // Assuming index.jsp is your login/entry page
                break;
        }
    }
}