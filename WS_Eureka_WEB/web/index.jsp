<%-- 
    Document   : index
    Created on : 12-ene-2025, 15:18:16
    Author     : ckan1
--%>
<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>EUREKABANK - Login</title>
    <link rel="stylesheet" href="css/styles.css">
</head>
<body>
    <div class="background">
        <div class="login-container">
            <h1>Iniciar Sesión</h1>
            <form action="LoginServlet" method="POST">
                <label for="tipoUsuario">Tipo de Usuario</label>
                <select id="tipoUsuario" name="tipoUsuario" required>
                    <option value="cliente">Cliente</option>
                    <option value="empleado">Empleado</option>
                </select>
                
                <label for="usuario">Usuario</label>
                <input type="text" id="usuario" name="usuario" required>
                
                <label for="password">Contraseña</label>
                <input type="password" id="password" name="password" required>
                
                <button type="submit">Ingresar</button>
            </form>
            <% 
            String errorMessage = (String) request.getAttribute("errorMessage");
            if (errorMessage != null) {
            %>
                <p style="color: red;"><%= errorMessage %></p>
            <% } %>
        </div>
    </div>
</body>
</html>

