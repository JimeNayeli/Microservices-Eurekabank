<%-- 
    Document   : clientes
    Created on : 09-feb-2025, 19:37:02
    Author     : ckan1
--%>

<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ page import="java.util.List" %>
<%@ page import="ec.edu.monster.modelo.Cliente" %>
<%@ page import="ec.edu.monster.controlador.ClienteControlador" %>
<%
    ClienteControlador clienteControlador = new ClienteControlador();
    List<Cliente> clientes = clienteControlador.listarClientes();
    request.setAttribute("clientes", clientes);
%>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>Gestión de Clientes</title>
    <link rel="stylesheet" href="css/styles.css">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">
</head>
<body class="background">
    <div class="cuenta-container">
        <h1>Gestión de Clientes</h1>
        <button class="btn-primary mb-3" data-bs-toggle="modal" data-bs-target="#modalCliente" onclick="limpiarFormulario()">Agregar Cliente</button>
        <button type="button" onclick="window.location.href='menuGestion.jsp'" 
            style="background-color: red; color: white; padding: 10px 16px; border: none; border-radius: 4px; cursor: pointer;">
            Regresar a Menú
        </button>

        <div class="movement-table-wrapper">
            <table class="movement-table">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Nombre</th>
                        <th>Apellido Paterno</th>
                        <th>Apellido Materno</th>
                        <th>DNI</th>
                        <th>Ciudad</th>
                        <th>Dirección</th>
                        <th>Teléfono</th>
                        <th>Email</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <% for (Cliente cliente : clientes) { %>
                    <tr>
                        <td><%= cliente.getId() %></td>
                        <td><%= cliente.getNombre() %></td>
                        <td><%= cliente.getApellidoPaterno() %></td>
                        <td><%= cliente.getApellidoMaterno() %></td>
                        <td><%= cliente.getDni() %></td>
                        <td><%= cliente.getCiudad() %></td>
                        <td><%= cliente.getDireccion()%></td>
                        <td><%= cliente.getTelefono() %></td>
                        <td><%= cliente.getEmail() %></td>
                        <td>
                            <button class="btn-secondary" onclick="editarCliente('<%= cliente.getId() %>')">Editar</button>
                            <a href="ClienteServlet?action=delete&id=<%= cliente.getId() %>" class="btn-primary" style="background-color: red;">Eliminar</a>
                        </td>
                    </tr>
                    <% } %>
                </tbody>
            </table>
        </div>
    </div>

    <!-- Modal para agregar/editar clientes -->
    <div class="modal" id="modalCliente">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Formulario Cliente</h5>
                    <button type="button" class="close" data-bs-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <form action="ClienteServlet" method="POST">
                        <div class="form-group">
                            <label>ID:</label>
                            <input type="text" class="form-control" id="clienteId" name="id" required>
                        </div>
                        <div class="form-group">
                            <label>Nombre:</label>
                            <input type="text" class="form-control" id="nombre" name="nombre" required>
                        </div>
                        <div class="form-group">
                            <label>Apellido Paterno:</label>
                            <input type="text" class="form-control" id="apellidoPaterno" name="apellidoPaterno" required>
                        </div>
                        <div class="form-group">
                            <label>Apellido Materno:</label>
                            <input type="text" class="form-control" id="apellidoMaterno" name="apellidoMaterno" required>
                        </div>
                        <div class="form-group">
                            <label>DNI:</label>
                            <input type="text" class="form-control" id="dni" name="dni" required>
                        </div>
                        <div class="form-group">
                            <label>Ciudad:</label>
                            <input type="text" class="form-control" id="ciudad" name="ciudad">
                        </div>
                        <div class="form-group">
                            <label>Dirección:</label>
                            <input type="text" class="form-control" id="direccion" name="direccion">
                        </div>
                        <div class="form-group">
                            <label>Teléfono:</label>
                            <input type="text" class="form-control" id="telefono" name="telefono">
                        </div>
                        <div class="form-group">
                            <label>Email:</label>
                            <input type="email" class="form-control" id="email" name="email">
                        </div>
                        <button type="submit" class="btn-primary">Guardar</button>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        function editarCliente(id) {
            fetch('ClienteServlet?action=get&id=' + id)
                .then(response => response.json())
                .then(data => {
                    document.getElementById('clienteId').value = data.id;
                    document.getElementById('nombre').value = data.nombre;
                    document.getElementById('apellidoPaterno').value = data.apellidoPaterno;
                    document.getElementById('apellidoMaterno').value = data.apellidoMaterno;
                    document.getElementById('dni').value = data.dni;
                    document.getElementById('ciudad').value = data.ciudad;
                    document.getElementById('ciudad').value = data.direccion;
                    document.getElementById('telefono').value = data.telefono;
                    document.getElementById('email').value = data.email;
                    var modal = new bootstrap.Modal(document.getElementById('modalCliente'));
                    modal.show();
                })
                .catch(error => console.error('Error:', error));
        }

        function limpiarFormulario() {
            document.getElementById('clienteId').value = "";
            document.getElementById('nombre').value = "";
            document.getElementById('apellidoPaterno').value = "";
            document.getElementById('apellidoMaterno').value = "";
            document.getElementById('dni').value = "";
            document.getElementById('ciudad').value = "";
            document.getElementById('direccion').value = "";
            document.getElementById('telefono').value = "";
            document.getElementById('email').value = "";
        }
    </script>
</body>
</html>
