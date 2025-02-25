<%-- 
    Document   : menuGestion
    Created on : Feb 11, 2025, 2:46:01 PM
    Author     : Jimena
--%>

<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>Gestión - Menú Principal</title>
    <link rel="stylesheet" href="css/styles.css">
</head>
<body>
    <%
    // Obtener el nombre del empleado de la sesión
    String nombreCliente = (String) session.getAttribute("userName");
    String cuentaCliente = (String) session.getAttribute("cuentaCliente");
    %>
    <div class="background">
        <div class="menu-container">
            <h1>EUREKABANK GESTIÓN</h1>
            <p>Bienvenido, <%= nombreCliente%></p>
            <form action="AccionServlet" method="POST">
                <button type="submit" name="accion" value="clientes">Gestión de Clientes</button>
                <button type="submit" name="accion" value="cuentas">Gestión de Cuentas</button> 

                <button type="button" onclick="window.location.href='logout'" 
                    style="background-color: red; color: white; padding: 10px 15px; border: none; border-radius: 4px; cursor: pointer;">
                    Salir
                </button>
            </form>

        </div>
    </div>

<script src="app.js"></script>
</body>
</html>