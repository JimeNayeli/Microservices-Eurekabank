/* 
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/ClientSide/javascript.js to edit this template
 */


// ✅ Definir la URL base al inicio para evitar repetirla en todo el código
// Definir la URL base
const BASE_URL = 'http://localhost:5057/api';
const CUENTAS_URL = `${BASE_URL}/Cuentas`;

function editarCuenta(id) {
    window.location.href = `CuentaServlet?id=${id}`;
}

function verInfoCuenta(codigoCuenta) {
    fetch(`${BASE_URL}/CuentaServlet?action=detalles&id=${codigoCuenta}`)
        .then(response => {
            if (!response.ok) {
                throw new Error("Cuenta no encontrada");
            }
            return response.json();
        })
        .then(data => {
            document.getElementById("detalleCodigoCuenta").innerText = data.codigoCuenta;
            document.getElementById("detalleMoneda").innerText = data.moneda;
            document.getElementById("detalleSucursal").innerText = data.sucursal;
            document.getElementById("detalleCiudadSucursal").innerText = data.ciudadSucursal;
            document.getElementById("detalleEmpleado").innerText = data.nombreEmpleado;
            document.getElementById("detalleDniCliente").innerText = data.dniCliente;
            document.getElementById("detalleNombreCliente").innerText = data.nombreCliente;
            document.getElementById("detalleCiudadCliente").innerText = data.ciudadCliente;
            document.getElementById("detalleTelefonoCliente").innerText = data.telefonoCliente;
            document.getElementById("detalleEmailCliente").innerText = data.emailCliente;
            document.getElementById("detalleFechaCreacion").innerText = data.fechaCreacion;
            document.getElementById("detalleNumeroMovimientos").innerText = data.numeroMovimientos;
            document.getElementById("detalleSaldo").innerText = `$${data.saldo.toFixed(2)}`;
            document.getElementById("detalleDireccionCliente").innerText = data.direccionCliente;
            document.getElementById("modalCuenta").style.display = "block";
        })
        .catch(error => {
            console.error("Error:", error);
            alert("No se pudo obtener los detalles.");
        });
}

document.addEventListener("DOMContentLoaded", function() {
    console.log("✅ app.js cargado correctamente");

    var userId = document.getElementById("userIdHidden")?.value;

    if (userId && userId !== "null") {
        sessionStorage.setItem("userId", userId);
        console.log("✅ UserId guardado en sessionStorage:", userId);
    } else {
        console.warn("⚠ No se encontró userId en la sesión.");
    }

    document.getElementById("formCrearCuenta").addEventListener("submit", crearCuenta);
});

function abrirModalCrearCuenta() {
    console.log("📌 Se ejecutó abrirModalCrearCuenta()");
    document.getElementById('crearCuentaModal').style.display = 'block';
}

function cerrarModalCrearCuenta() {
    document.getElementById('crearCuentaModal').style.display = 'none';
}

function crearCuenta(event) {
    event.preventDefault();

    var empleadoCodigo = sessionStorage.getItem("userId");

    if (!empleadoCodigo) {
        alert("Error: No se encontró el ID del empleado en sessionStorage.");
        return;
    }

    const formData = new FormData(event.target);
    const data = {
        monedaCodigo: formData.get('monedaCodigo'),
        sucursalCodigo: formData.get('sucursalCodigo'),
        empleadoCodigo: empleadoCodigo,
        clienteCodigo: formData.get('clienteCodigo'),
        clave: formData.get('clave')
    };

    console.log("Datos enviados:", JSON.stringify(data));

    fetch(`${CUENTAS_URL}/crear`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    })
    .then(response => {
        if (!response.ok) {
            throw new Error("Error al crear la cuenta");
        }
        return response.json();
    })
    .then(data => {
        alert('Cuenta creada con éxito: ' + data.id);
        cerrarModalCrearCuenta();
        window.location.reload();
    })
    .catch(error => {
        console.error('Error al crear la cuenta:', error);
    });
}

function cerrarModal() {
    document.getElementById('modalCuenta').style.display = 'none';
}

window.onclick = function(event) {
    const modal = document.getElementById('modalCuenta');
    if (event.target == modal) {
        modal.style.display = 'none';
    }
}

function abrirModalEditarCuenta(codigoCuenta) {
    fetch(`${BASE_URL}/CuentaServlet?action=detalles&id=${codigoCuenta}`)
        .then(response => {
            if (!response.ok) {
                throw new Error("Error al obtener los detalles de la cuenta");
            }
            return response.json();
        })
        .then(data => {
            document.getElementById("editarCodigoCuenta").value = data.codigoCuenta;
            document.getElementById("editarNombreCliente").value = data.nombreCliente;
            document.getElementById("editarCiudad").value = data.ciudadCliente;
            document.getElementById("editarDireccion").value = data.direccionCliente;
            document.getElementById("editarTelefono").value = data.telefonoCliente;
            document.getElementById("editarEmail").value = data.emailCliente;
            document.getElementById("editarCuentaModal").style.display = "block";
        })
        .catch(error => {
            console.error("Error al obtener los datos de la cuenta:", error);
            alert("No se pudo cargar los detalles de la cuenta.");
        });
}

function cerrarModalEditarCuenta() {
    document.getElementById("editarCuentaModal").style.display = "none";
}

function actualizarCuenta(event) {
    event.preventDefault();

    const codigoCuenta = document.getElementById("editarCodigoCuenta").value;
    const data = {
        ciudad: document.getElementById("editarCiudad").value,
        direccion: document.getElementById("editarDireccion").value,
        telefono: document.getElementById("editarTelefono").value,
        email: document.getElementById("editarEmail").value
    };

    fetch(`${CUENTAS_URL}/${codigoCuenta}/actualizar`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
    .then(response => {
        if (!response.ok) {
            throw new Error("Error al actualizar la cuenta");
        }
        return response.text();
    })
    .then(message => {
        alert(message);
        cerrarModalEditarCuenta();
        location.reload();
    })
    .catch(error => {
        console.error("Error al actualizar la cuenta:", error);
        alert("No se pudo actualizar la cuenta.");
    });
}

function togglePassword(inputId) {
    const input = document.getElementById(inputId);
    input.type = input.type === 'password' ? 'text' : 'password';
}

function abrirModalCancelarCuenta(codigoCuenta) {
    document.getElementById("cuentaIdCancelar").value = codigoCuenta;
    document.getElementById("cancelarCuentaModal").style.display = "block";
    document.getElementById("numeroCuentaCancelar").innerText = codigoCuenta;
}

function cerrarModalCancelarCuenta() {
    document.getElementById("cancelarCuentaModal").style.display = "none";
}

function cancelarCuenta(event) {
    event.preventDefault();

    let cuentaId = document.getElementById("cuentaIdCancelar").value;
    let clave = document.getElementById("claveCancelar").value;
    let confirmar = document.getElementById("confirmarCancelar").checked;

    if (!confirmar) {
        alert("Debes confirmar la cancelación.");
        return;
    }

    let data = {
        clave: clave,
        cancelar: true
    };

    fetch(`${CUENTAS_URL}/${cuentaId}/cancelar`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })
    .then(response => {
        if (!response.ok) {
            throw new Error("Error al cancelar la cuenta");
        }
        return response.json();
    })
    .then(responseData => {
        if (responseData) {
            alert("Cuenta cancelada con éxito.");
            cerrarModalCancelarCuenta();
            location.reload();
        } else {
            alert("No se pudo cancelar la cuenta.");
        }
    })
    .catch(error => {
        console.error("Error:", error);
        alert("Hubo un error al cancelar la cuenta.");
    });
}