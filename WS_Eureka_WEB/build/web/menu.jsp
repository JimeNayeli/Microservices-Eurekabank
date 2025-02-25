<%-- 
    Document   : menu
    Created on : 12-ene-2025, 15:29:56
    Author     : ckan1
--%>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>EUREKABANK - Menú Principal</title>
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
           <h1>EUREKABANK</h1>
            <p>Bienvenido, <%= nombreCliente %></p>

            <!-- Caja para mostrar la cuenta y el saldo -->
            <div class="cuenta-saldo-container">
                <span class="cuenta-info">Cuenta: <%= cuentaCliente %></span>
                <span class="saldo-info">Saldo: <span id="saldoCuenta">Cargando...</span></span>
            </div>

            <style>
                .cuenta-saldo-container {
                    display: flex;
                    justify-content: space-between; /* Distribuir elementos a los extremos */
                    align-items: center;
                    background-color: #d4edda; /* Verde claro */
                    color: #155724; /* Verde oscuro para contraste */
                    padding: 10px 20px;
                    border-radius: 10px; /* Bordes redondeados */
                    font-size: 16px;
                    font-weight: bold;
                    border: 1px solid #c3e6cb; /* Borde en un verde más oscuro */
                    max-width: 400px; /* Ancho máximo */
                    margin: 10px auto; /* Centrado */
                }

                .cuenta-info {
                    text-align: left; /* Alineación a la izquierda */
                }

                .saldo-info {
                    text-align: right; /* Alineación a la derecha */
                }
            </style>

            <form action="AccionServlet" method="POST">
                <button type="submit" name="accion" value="consulta">Consulta</button>
                <button type="submit" name="accion" value="deposito">Depósito</button>
                <button type="submit" name="accion" value="retiro">Retiro</button>
                <button type="submit" name="accion" value="transferencia">Transferencia</button>
                <button type="button" onclick="abrirModalCancelarCuenta('<%= cuentaCliente %>')" class="btn-secondary">
                    Cancelar mi cuenta
                </button>

                <button type="button" onclick="window.location.href='logout'" style="background-color: red; color: white; padding: 10px 15px; border: none; border-radius: 4px; cursor: pointer;">
                    Salir
                </button>
            </form>
        </div>
    </div>
    <div id="cancelarCuentaModal" class="modal">
    <div class="modal-content">
        <span class="close" onclick="cerrarModalCancelarCuenta()">&times;</span>
        <h2>Cancelar Cuenta, Cliente <%= nombreCliente%></h2>
        <div class="warning-box">
            <p>Esta acción no se puede deshacer. La cuenta será cancelada y no podrá realizar ninguna acción.</p>
        </div>
        <form id="formCancelarCuenta" onsubmit="cancelarCuenta(event)">
            <input type="hidden" id="cuentaIdCancelar">
            <table class="detalle-table">
                <tr>
                    <th>Cuenta a Cancelar</th>
                    <td><span id="numeroCuentaCancelar" class="cuenta-display"></span></td>
                </tr>

                <tr>
                    <th>Clave de Cuenta</th>
                    <td>
                        <div class="password-container">
                            <input type="password" 
                                   id="claveCancelar" 
                                   name="claveCancelar" 
                                   required
                                   placeholder="Ingrese su clave de acceso">
                            <span class="toggle-password" onclick="togglePassword('claveCancelar')">👁️</span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <th>Confirmación</th>
                    <td>
                        <label class="checkbox-container">
                            <input type="checkbox" id="confirmarCancelar" required>
                            <span class="checkmark"></span>
                            <span class="checkbox-text">Confirmo que deseo cancelar esta cuenta</span>
                        </label>
                    </td>
                </tr>
            </table>
            <div class="form-actions">
                <button type="submit" class="btn-danger">
                    <i class="icon-warning">⚠️</i> Confirmar Cancelación
                </button>
                <button type="button" class="btn-secondary" onclick="cerrarModalCancelarCuenta()">
                    Volver
                </button>
            </div>
        </form>
    </div>
</div>
        <script>
    // Variable global para almacenar el código de cuenta
    const codigoCuentaGlobal = '<%= cuentaCliente %>';
</script>
<script src="app2.js"></script>
</body>
</html>
