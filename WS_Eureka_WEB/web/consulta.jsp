<%-- 
    Document   : consulta
    Created on : 12-ene-2025, 15:29:02
    Author     : ckan1
--%>

<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ page import="java.util.List" %>
<%@ page import="ec.edu.monster.modelo.Movimiento" %>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>EUREKABANK - Consulta de Movimientos</title>
    <link rel="stylesheet" href="css/styles.css">
</head>
<body>
    <div class="background">
        <div class="login-container">
            <h1>Consulta de Movimientos</h1>
            <form id="movementForm" action="MovimientosServlet" method="POST">
                <div class="account-controls">
                    <label for="cuenta">Cuenta:</label>
                    <span class="readonly-field"><%= session.getAttribute("cuentaCliente") %></span>
                    <input type="hidden" id="cuenta" name="cuenta" value="<%= session.getAttribute("cuentaCliente") %>">

                    <button type="submit" class="btn-primary">Consultar</button>
                    <button type="button" style="background-color: red;" onclick="goBack()">Regresar</button>
                </div>

                <% 
                    List<Movimiento> movimientos = (List<Movimiento>) request.getAttribute("movimientos");
                    if (movimientos != null && !movimientos.isEmpty()) {
                %>
                <div class="movement-table-wrapper">
                    <table class="movement-table">
                        <thead>
                            <tr>
                                <th>Cuenta</th>
                                <th>Número</th>
                                <th>Fecha</th>
                                <th>Tipo</th>
                                <th>Acción</th>
                                <th>Importe</th>
                            </tr>
                        </thead>
                        <tbody>
                            <% for (Movimiento movimiento : movimientos) { %>
                            <tr>
                                <td><%= movimiento.getCuenta() %></td>
                                <td><%= movimiento.getNromov() %></td>
                                <td><%= movimiento.getFecha() %></td>
                                <td><%= movimiento.getTipo() %></td>
                                <td><%= movimiento.getAccion() %></td>
                                <td><%= movimiento.getImporte() %></td>
                            </tr>
                            <% } %>
                        </tbody>
                    </table>
                </div>
                <% } else { %>
                <div class="no-movements">No se encontraron movimientos.</div>
                <% } %>
            </form>
        </div>
    </div>

    <script>
        function goBack() {
            // Redirigir al menú principal
            window.location.href = 'menu.jsp';
        }
    </script>
</body>
</html>