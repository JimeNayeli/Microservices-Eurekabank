<%-- 
    Document   : cuentas
    Created on : 09-feb-2025, 22:48:09
    Author     : ckan1
--%>

<%@page import="ec.edu.monster.modelo.CuentaResponse"%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ page import="java.util.List" %>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>Gestión de Cuentas</title>
    <link rel="stylesheet" href="css/styles.css">
   
</head>
<body>

    <%
    // Obtener el nombre del empleado de la sesión
    String nombreEmpleado = (String) session.getAttribute("userName");
    String empleadoCodigo = (String) request.getSession().getAttribute("userId");
    %>
    <div class="background">
        <div class="cuenta-container">
            <h1>Gestión de Cuentas</h1>
            <!-- Botón para abrir el modal -->
            <button onclick="abrirModalCrearCuenta()" class="btn-primary">Agregar Cuenta</button>
           <button type="button" onclick="window.location.href='menuGestion.jsp'" 
            style="background-color: red; color: white; padding: 12px 16px; border: none; border-radius: 4px; cursor: pointer;">
            Regresar a Menú
        </button>


            <input type="hidden" id="userIdHidden" value="<%= session.getAttribute("userId") %>">


            
            <%
            List<CuentaResponse> cuentas = (List<CuentaResponse>) request.getAttribute("cuentas");

            if (cuentas == null) {
                out.println("<p style='color: red;'>⚠ Error: La lista de cuentas es NULL.</p>");
            } else if (cuentas.isEmpty()) {
                out.println("<p style='color: orange;'>⚠ La lista de cuentas está vacía.</p>");
            } else {
                out.println("<p style='color: green;'>✅ Se encuentran " + cuentas.size() + " cuentas activas.</p>");
            }
        %>

        <div class="movement-table-wrapper">
            <table class="movement-table">
                <thead>
                    <tr>
                        <th>Código</th>
                        <th>Cliente</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <% if (cuentas != null && !cuentas.isEmpty()) { %>
                        <% for (CuentaResponse cuenta : cuentas) { %>
                        <tr>
                            <td><%= cuenta.getCodigoCuenta() %></td>
                            <td><%= cuenta.getNombreCliente() %></td>
                            <td>
                                <button onclick="verInfoCuenta('<%= cuenta.getCodigoCuenta() %>')" class="btn-primary">Ver Info.</button>
                                <button onclick="abrirModalEditarCuenta('<%= cuenta.getCodigoCuenta() %>')" class="btn-secondary">Editar</button>
                                <form id="formEliminar<%= cuenta.getCodigoCuenta() %>" action="CuentaServlet" method="post" style="display:inline;">
                                    <input type="hidden" name="id" value="<%= cuenta.getCodigoCuenta() %>">
                                    <input type="hidden" name="action" value="cancelar">
                                    <button type="button" onclick="abrirModalCancelarCuenta('<%= cuenta.getCodigoCuenta() %>')" class="btn-secondary">
                                        Cancelar
                                    </button>

                                </form>
                            </td>
                        </tr>
                        <% } %>
                    <% } else { %>
                        <tr><td colspan="3">No hay cuentas registradas.</td></tr>
                    <% } %>
                </tbody>
            </table>
        </div>
    </div>

    <!-- Modal para mostrar la información de la cuenta -->
    <div id="modalCuenta" class="modal">
        <div class="modal-content">
            <span class="close" onclick="cerrarModal()">&times;</span>
            <h2>Detalles de la Cuenta</h2>
            <table class="detalle-table">
                <tr><th>Código de Cuenta</th><td id="detalleCodigoCuenta"></td> <th>Moneda</th><td id="detalleMoneda"></td></tr>
                <tr><th>Sucursal</th><td id="detalleSucursal"></td> <th>Ciudad Sucursal</th><td id="detalleCiudadSucursal"></td></tr>
                <tr><th>Empleado</th><td id="detalleEmpleado"></td> <th>DNI Cliente</th><td id="detalleDniCliente"></td></tr>
                <tr><th>Cliente</th><td id="detalleNombreCliente"></td> <th>Ciudad Cliente</th><td id="detalleCiudadCliente"></td></tr>
                <tr><th>Dirección Cliente</th><td id="detalleDireccionCliente"></td> <th>Teléfono</th><td id="detalleTelefonoCliente"></td></tr>
                <tr><th>Email</th><td id="detalleEmailCliente"></td> <th>Fecha Creación</th><td id="detalleFechaCreacion"></td></tr>
                <tr><th>Nro. Movimientos</th><td id="detalleNumeroMovimientos"></td> <th>Saldo</th><td id="detalleSaldo"></td></tr>
            </table>
        </div>
    </div>
    
<!-- Modal para crear una cuenta -->
<div id="crearCuentaModal" class="modal">
    <div class="modal-content">
        <span class="close" onclick="cerrarModalCrearCuenta()">&times;</span>
        <h2>Crear Nueva Cuenta</h2>
        <form id="formCrearCuenta" onsubmit="crearCuenta(event)">
            <table class="detalle-table">
                <tr>
                    <th>Código de Moneda</th>
                    <td>
                        <input type="text" id="monedaCodigo" name="monedaCodigo" required 
                               placeholder="Ingresar codigo de moneda" maxlength="3">
                    </td>
                    <th>Código de Sucursal</th>
                    <td>
                        <input type="text" id="sucursalCodigo" name="sucursalCodigo" required
                               placeholder="Ingrese código de sucursal">
                    </td>
                </tr>
                <tr>
                    <th>Código de Cliente</th>
                    <td>
                        <input type="text" id="clienteCodigo" name="clienteCodigo" required
                               placeholder="Ingrese código de cliente">
                    </td>
                    <th>Clave</th>
                    <td>
                        <div class="password-container">
                            <input type="password" id="clave" name="clave" required
                                   placeholder="Ingrese clave" minlength="6">
                            <span class="toggle-password" onclick="togglePassword('clave')">👁️</span>
                        </div>
                    </td>
                </tr>
            </table>
            <div class="form-actions">
                <button type="submit" class="btn-primary">Crear Cuenta</button>
                <button type="button" class="btn-secondary" onclick="cerrarModalCrearCuenta()">Cancelar</button>
            </div>
        </form>
    </div>
</div>

<!-- Modal para actualizar cuenta -->
<div id="editarCuentaModal" class="modal">
    <div class="modal-content">
        <span class="close" onclick="cerrarModalEditarCuenta()">&times;</span>
        <h2>Actualizar Datos de la Cuenta</h2>
        <form id="formEditarCuenta" onsubmit="actualizarCuenta(event)">
            <table class="detalle-table">
                <tr>
                    <th>Código de Cuenta</th>
                    <td><input type="text" id="editarCodigoCuenta" name="codigoCuenta" readonly></td>
                    <th>Cliente</th>
                    <td><input type="text" id="editarNombreCliente" name="nombreCliente" readonly></td>
                </tr>
                <tr>
                    <th>Ciudad</th>
                    <td><input type="text" id="editarCiudad" name="ciudad" required></td>
                    <th>Dirección</th>
                    <td><input type="text" id="editarDireccion" name="direccion" required></td>
                </tr>
                <tr>
                    <th>Teléfono</th>
                    <td><input type="text" id="editarTelefono" name="telefono" required></td>
                    <th>Email</th>
                    <td><input type="email" id="editarEmail" name="email" required></td>
                </tr>
            </table>
            <div class="form-actions">
                <button type="submit" class="btn-primary">Actualizar Cuenta</button>
            </div>
        </form>
    </div>
</div>

<div id="cancelarCuentaModal" class="modal">
    <div class="modal-content">
        <span class="close" onclick="cerrarModalCancelarCuenta()">&times;</span>
        <h2>Cancelar Cuenta</h2>
        <div class="warning-box">
            <p>Esta acción no se puede deshacer. La cuenta será cancelada.</p>
        </div>
        <form id="formCancelarCuenta" onsubmit="cancelarCuenta(event)">
            <input type="hidden" id="cuentaIdCancelar">
            <table class="detalle-table">
                <tr>
                    <th>Cuenta a Cancelar</th>
                    <td><span id="numeroCuentaCancelar" class="cuenta-display"></span></td>
                </tr>

                <tr>
                    <th>Clave de Autorización</th>
                    <td>
                        <div class="password-container">
                            <input type="password" 
                                   id="claveCancelar" 
                                   name="claveCancelar" 
                                   required
                                   placeholder="Ingrese su clave de autorización">
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

     <script src="app2.js"></script>
</body>
</html>
