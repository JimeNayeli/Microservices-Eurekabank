/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ec.edu.monster.controlador;

import ec.edu.monster.modelo.LoginResponse;
import java.io.IOException;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

/**
 *
 * @author ckan1
 */
@WebServlet("/LoginServlet")
public class LoginServlet extends HttpServlet {
    private LoginControlador loginControlador;

    @Override
    public void init() {
        this.loginControlador = new LoginControlador();
    }

    @Override
   protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        
        String usuario = request.getParameter("usuario");
        String password = request.getParameter("password");
        String tipoUsuario = request.getParameter("tipoUsuario");
        
        LoginResponse loginResponse = loginControlador.login(usuario, password, tipoUsuario);
        
        if (loginResponse != null && loginResponse.getId() != null) {
            // Guardar información del usuario en la sesión
            HttpSession session = request.getSession();
            session.setAttribute("userId", loginResponse.getId());
            session.setAttribute("userName", loginResponse.getNombre());
            session.setAttribute("userType", tipoUsuario);
            session.setAttribute("cuentaCliente", usuario);
            System.out.println("Cuenta de cliente guardada en sesión: " + usuario);
            if ("cliente".equals(tipoUsuario)) {
                response.sendRedirect("menu.jsp");
            } else {
                // ✅ Redirigir al Servlet para que cargue los datos antes de mostrar la vista
                response.sendRedirect("menuGestion.jsp"); 
            }
        } else {
            request.setAttribute("errorMessage", loginResponse != null ? 
                loginResponse.getMensaje() : "Error en la autenticación");
            request.getRequestDispatcher("index.jsp").forward(request, response);
        }
    }
}
