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
@WebServlet("/RetiroServlet")
public class RetiroServlet extends HttpServlet {
    private DepositoControlador depositoControlador;

    @Override
    public void init() {
        this.depositoControlador = new DepositoControlador();
    }

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        String cuenta = request.getParameter("cuenta");
        double importe = Double.parseDouble(request.getParameter("importe"));

        String message = depositoControlador.registrarRetiro(cuenta, importe);

        // Set the message as a request attribute and forward to the deposit page
        request.setAttribute("message", message);
        request.getRequestDispatcher("retiro.jsp").forward(request, response);
    }
}
