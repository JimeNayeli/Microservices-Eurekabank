<%-- 
    Document   : retiro
    Created on : 12-ene-2025, 15:30:22
    Author     : ckan1
--%>

<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>EUREKABANK - REtiro</title>
    <link rel="stylesheet" href="css/styles.css">
</head>
<body>
    <div class="background">
        <div class="menu-container">
            <h1>Realizar Retiro</h1>
            <form id="depositForm" action="RetiroServlet" method="POST">
                <label for="cuenta">Cuenta:</label>
                <span class="readonly-field"><%= session.getAttribute("cuentaCliente") %></span>
                <input type="hidden" id="cuenta" name="cuenta" value="<%= session.getAttribute("cuentaCliente") %>">

                <label for="importe">Importe:</label>
                <input type="number" id="importe" name="importe" step="0.01" required>

                <button type="submit" class="btn-primary">Procesar</button>
                <button type="button" style="background-color: red;" onclick="goBack()">Regresar</button>

                <% 
                String message = (String) request.getAttribute("message");
                if (message != null) {
                    String messageClass = message.startsWith("Error") ? "error-message" : "success-message";
            %>
                <div class="<%= messageClass %>"><%= message %></div>
            <% 
                }
            %>
            </form>
        </div>
    </div>

    <script>
        function goBack() {
            // Redirect to the main menu or previous page
            window.location.href = 'menu.jsp';
        }
    </script>
</body>
</html>
