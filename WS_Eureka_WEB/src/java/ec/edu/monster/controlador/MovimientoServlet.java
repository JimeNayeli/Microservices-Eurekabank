/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ec.edu.monster.controlador;

import ec.edu.monster.modelo.Movimiento;
import java.io.IOException;
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
@WebServlet("/MovimientosServlet")
public class MovimientoServlet extends HttpServlet {
    private MovimientosControlador movimientosControlador;

    @Override
    public void init() {
        this.movimientosControlador = new MovimientosControlador();
    }

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        String cuenta = request.getParameter("cuenta");

        try {
            List<Movimiento> movimientos = movimientosControlador.obtenerMovimientos(cuenta);
            request.setAttribute("movimientos", movimientos);
            request.getRequestDispatcher("consulta.jsp").forward(request, response);
        } catch (RuntimeException e) {
            // Manejar el error y enviar un mensaje de error a la vista
            request.setAttribute("errorMessage", e.getMessage());
            request.getRequestDispatcher("consulta.jsp").forward(request, response);
        }
    }
}
